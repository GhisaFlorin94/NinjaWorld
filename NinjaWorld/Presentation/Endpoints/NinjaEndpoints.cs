using Microsoft.AspNetCore.Mvc;
using NinjaWorld.Application.Interfaces;
using NinjaWorld.Application.Models;
using NinjaWorld.Application.Models.Dtos;
using NinjaWorld.Application.Models.Orders;
using NinjaWorld.Domain.Entities;
using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Presentation.Endpoints
{
    public static class NinjaEndpoints
    {
        public static void MapNinjaEndpoints(this WebApplication app)
        {
            app.MapGet("/ninjas", GetAllNinjas);
            app.MapGet("/ninjas/{id}", GetNinjaById);
            app.MapPost("/ninjas", CreateNinja);
            app.MapPut("/ninjas/{id}", UpdateNinja);
            app.MapDelete("/ninjas/{id}", DeleteNinja);
            app.MapPost("world/reset", ResetWorld);
            app.MapPost("world/war", StartWar);
        }

        private static void StartWar([FromServices] INinjaService ninjaService, WarDeclarationRequestDto warDeclarationRequest)
        {
            ninjaService.DeclareWar(warDeclarationRequest);
        }

        private static async Task ResetWorld([FromServices] INinjaService ninjaService, [FromBody] int ninjaNumber)
        {
            await ninjaService.ResetWorldAsync(ninjaNumber);
        }

        private static async Task<Ninja> GetNinjaById([FromServices] INinjaService ninjaService, Guid id)
        {
            return await ninjaService.GetByIdAsync(id);
        }

        private static async Task<IEnumerable<Ninja>> GetAllNinjas([FromServices] INinjaService ninjaService, [FromQuery] string? name, NinjaRank? rank, string? orderBy, OrderDirection? orderDirection)
        {
            return await ninjaService.SearchNinjaAsync(name, rank, orderBy, orderDirection);
        }

        private static async Task<Ninja> CreateNinja([FromServices] INinjaService ninjaService, [FromBody] NinjaDto ninja)
        {
            return await ninjaService.CreateNinjaAsync(ninja);
        }

        private static async Task UpdateNinja([FromServices] INinjaService ninjaService, Guid id, [FromBody] NinjaDto ninja)
        {
            await ninjaService.UppdateNinjaAsync(id, ninja);
            return;
        }

        private static async Task<bool> DeleteNinja([FromServices] INinjaService ninjaService, Guid id)
        {
            return await ninjaService.DeleteNinjaAsync(id);
        }
    }
}