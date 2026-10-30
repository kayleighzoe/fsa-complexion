using Complexion.Models.Products;

namespace Complexion.Repository.Lookups
{
    public interface ILookupRepository
    {
        Task<ShareLookupIds> GetShareLookupIdsAsync(ShareRecommendationDto dto);
    }
}