using Complexion.Models.Skin;

namespace Complexion.Services.Skin
{
    public interface ISkinUndertoneService
    {
        Task<IEnumerable<SkinUndertone>> GetAllAsync();
    }
}