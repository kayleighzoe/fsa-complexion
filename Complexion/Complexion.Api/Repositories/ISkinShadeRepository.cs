using Complexion.Api.Models;

namespace Complexion.Api.Repositories
{
    public interface ISkinShadeRepository
    {
        Task<IEnumerable<SkinShade>> GetAllSkinShades();
    }
}
