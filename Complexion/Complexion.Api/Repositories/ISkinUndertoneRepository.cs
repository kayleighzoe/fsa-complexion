using Complexion.Api.Models;

namespace Complexion.Api.Repositories
{
    public interface ISkinUndertoneRepository
    {
        Task<IEnumerable<SkinUndertone>> GetAllSkinUndertones();
    }
}