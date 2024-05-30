using NinjaWorld.Application.Models;
using NinjaWorld.Application.Models.Dtos;
using NinjaWorld.Application.Models.Orders;
using NinjaWorld.Domain.Entities;
using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Application.Interfaces
{
    public interface INinjaService
    {
        Task<IEnumerable<Ninja>> SearchNinjaAsync(string? name, NinjaRank? rank, string? orderBy, OrderDirection? orderDirection);

        Task<Ninja> CreateNinjaAsync(NinjaDto ninja);

        Task UppdateNinjaAsync(Guid id, NinjaDto ninja);

        Task<bool> DeleteNinjaAsync(Guid id);

        Task<Ninja> GetByIdAsync(Guid id);

        Task ResetWorldAsync(int ninjaNumber);

        void DeclareWar(WarDeclarationRequestDto warDeclarationRequest);

        Task HandleWarAftermatch(WarResult warResult);
    }
}