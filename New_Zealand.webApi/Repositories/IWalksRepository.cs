using Microsoft.AspNetCore.Mvc;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;

namespace New_Zealand.webApi.Repositories
{
    public interface IWalksRepository
    {
        Task<Walk> CreateWalksAsync(Walk walk);
        Task<List<Walk>>GetWalksAsync();
        Task<Walk?> GetOneWalksAsync(Guid id);
    }
}
