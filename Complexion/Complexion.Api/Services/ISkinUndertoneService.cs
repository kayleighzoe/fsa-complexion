using Complexion.Api.Models;

namespace Complexion.Api.Services
{
    public interface ISkinUndertoneService
    {
        Task<IEnumerable<SkinUndertone>> GetAllSkinUndertones();
    }
}