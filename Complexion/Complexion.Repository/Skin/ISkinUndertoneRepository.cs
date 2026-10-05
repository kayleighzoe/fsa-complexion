using Complexion.Models.Skin;

namespace Complexion.Repository.Skin
{
    public interface ISkinUndertoneRepository
    {
        Task<IEnumerable<SkinUndertone>> GetAllUndertonesAsync();
    }
}