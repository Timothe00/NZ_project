using New_Zealand.webApi.Models.Domain;

namespace New_Zealand.webApi.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Regions>> GetAllRegionsAsync();
        Task<Regions?> GetOneByIdRegionsAsync(Guid id);
        Task<Regions> CreateRegionAsync(Regions regions);
        Task<Regions?> UpdateRegionAsync(Guid id, Regions regions);
        Task<Regions?> DeleteRegionAsync(Guid id);
    }
}
