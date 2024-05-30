using WarResolverClient.Models;

namespace WarResolverClient.Services.Interfaces
{
    internal interface IWarResolverService
    {
        WarResult ResolveWar(WarDeclarationRequest warDeclarationRequest);
    }
}