using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NinjaWorld.Application.Extensions;
using NinjaWorld.Application.Helpers;
using NinjaWorld.Application.Interfaces;
using NinjaWorld.Application.Models;
using NinjaWorld.Application.Models.Dtos;
using NinjaWorld.Application.Models.Orders;
using NinjaWorld.Domain.Entities;
using NinjaWorld.Domain.Enums;
using RabbitMQ.Client;
using System.Text;

namespace NinjaWorld.Application.Services
{
    public class NinjaService : INinjaService
    {
        private readonly INinjaDbContext _db;

        //private readonly IMessagePublisher _messagePublisher;
        private static readonly Random _random = new Random();

        public NinjaService(INinjaDbContext ninjaDbContext)
        {
            _db = ninjaDbContext;
        }

        public async Task<Ninja> CreateNinjaAsync(NinjaDto ninjaDto)
        {
            var ninja = ninjaDto.ToNinja();
            await _db.Ninja.AddAsync(ninja);
            _db.SaveChanges();
            return ninja;
        }

        public async Task<bool> DeleteNinjaAsync(Guid id)
        {
            var deletedRow = await _db.Ninja.Where(n => n.Id == id).ExecuteDeleteAsync();
            if (deletedRow != 1)
                return false;
            else return true;
        }

        public async Task<Ninja> GetByIdAsync(Guid id)
        {
            var ninja = await _db.Ninja.Include(n => n.Tools).SingleAsync(n => n.Id == id);
            if (ninja == null)
                throw new KeyNotFoundException("Ninja not found");
            return ninja;
        }

        public async Task<IEnumerable<Ninja>> SearchNinjaAsync(string? name, NinjaRank? rank, string? orderBy, OrderDirection? orderDirection)
        {
            var query = _db.Ninja.Include(n => n.Tools).AsQueryable().AsNoTracking();
            if (rank != null)
                query = query.Where(n => n.Rank == rank);
            if (name != null)
                query = query.Where(n => n.Name == name);

            if (orderBy != null)
                query = query.OrderBy(orderBy, orderDirection ?? OrderDirection.Ascending);
            return await query.ToListAsync();
        }

        public async Task UppdateNinjaAsync(Guid id, NinjaDto ninja)
        {
            var existingNinja = await GetByIdAsync(id);
            if (existingNinja == null)
            {
                throw new KeyNotFoundException("Ninja not found");
            }
            existingNinja.Name = ninja.Name;
            existingNinja.Rank = ninja.Rank;
            existingNinja.Tools = new List<Tool>(ninja.Tools.Select(t => t.ToTool()));
            existingNinja.Power = ninja.Power;
            existingNinja.Village = ninja.Village;
            _db.Ninja.Update(existingNinja);
            _db.SaveChanges();
        }

        public async Task ResetWorldAsync(int n)
        {
            await ClearWorldAsync();

            var ninjas = new List<Ninja>();

            for (int i = 0; i < n; i++)
            {
                var ninja = new Ninja
                {
                    Id = Guid.NewGuid(),
                    Name = "NinjaWarrior" + i,
                    Rank = (NinjaRank)_random.Next(Enum.GetValues(typeof(NinjaRank)).Length),
                    Village = (Village)_random.Next(Enum.GetValues(typeof(Village)).Length),
                    Power = _random.Next(20, 301),
                    Tools = GenerateRandomTools()
                };

                ninjas.Add(ninja);
            }
            await _db.Ninja.AddRangeAsync(ninjas);
            _db.SaveChanges();
        }

        public void DeclareWar(WarDeclarationRequestDto warDeclarationRequest)
        {
            if (warDeclarationRequest.DefendingVillage == warDeclarationRequest.AttackingVillage)
                throw new ArgumentException("Attacking Village can't be the same as the defending Village");

            //Improvement: Use MassTransit && extract loginc inside Intfrasturcutre layer
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(Constants.RequestQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var warDeclaration = new WarDeclarationRequest()
            {
                AttackingVillage = warDeclarationRequest.AttackingVillage,
                DefendingVillage = warDeclarationRequest.DefendingVillage,
                AttackingArmy = _db.Ninja.Include(n => n.Tools).Where(n => n.Village == warDeclarationRequest.AttackingVillage).ToList(),
                DefendingArmy = _db.Ninja.Include(n => n.Tools).Where(n => n.Village == warDeclarationRequest.DefendingVillage).ToList()
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(warDeclaration, SerializerHelper.GetJsonSerializerSettings()));

            channel.BasicPublish("", Constants.RequestQueueName, null, body);
        }

        private async Task ClearWorldAsync()
        {
            await _db.Tool.ExecuteDeleteAsync();
            await _db.Ninja.ExecuteDeleteAsync();
        }

        private static List<Tool> GenerateRandomTools()
        {
            string[] ToolNames = { "Kunai", "Shuriken", "Explosive Tag", "Smoke Bomb", "Ninja Sword" };

            int numberOfTools = _random.Next(1, 6);
            var tools = new List<Tool>();

            for (int i = 0; i < numberOfTools; i++)
            {
                var tool = new Tool
                {
                    Id = Guid.NewGuid(),
                    Name = ToolNames[_random.Next(ToolNames.Length)],
                    Power = _random.Next(1, 30)
                };

                tools.Add(tool);
            }

            return tools;
        }

        public async Task HandleWarAftermatch(WarResult warResult)
        {
            await _db.Tool.Where(x => warResult.AttackingArmyCasualties.Select(ac => ac.Id).Contains(x.NinjaId)).ExecuteDeleteAsync();
            await _db.Ninja.Where(x => warResult.AttackingArmyCasualties.Select(ac => ac.Id).Contains(x.Id)).ExecuteDeleteAsync();
            await _db.Tool.Where(x => warResult.DefendingArmyCasualties.Select(ac => ac.Id).Contains(x.NinjaId)).ExecuteDeleteAsync();
            await _db.Ninja.Where(x => warResult.DefendingArmyCasualties.Select(ac => ac.Id).Contains(x.Id)).ExecuteDeleteAsync();
        }
    }
}