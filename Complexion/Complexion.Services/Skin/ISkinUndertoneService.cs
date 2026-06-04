using Complexion.Models.Skin;

namespace Complexion.Api.Services
{
    public interface ISkinUndertoneService
    {
        Task<IEnumerable<SkinUndertone>> GetAllSkinUndertones();
    }
}