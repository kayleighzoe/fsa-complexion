using Complexion.Models.Skin;
using Complexion.Repository.Skin;

namespace Complexion.Api.Services
{
    public class SkinUndertoneService : ISkinUndertoneService
    {
        private readonly ISkinUndertoneRepository _skinUndertoneRepository;

        public SkinUndertoneService(ISkinUndertoneRepository skinUndertoneRepository)
        {
            _skinUndertoneRepository = skinUndertoneRepository;
        }

        public async Task<IEnumerable<SkinUndertone>> GetAllSkinUndertones()
        {
            return await _skinUndertoneRepository.GetAllSkinUndertones();
        }
    }
}
