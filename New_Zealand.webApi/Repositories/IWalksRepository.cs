using Microsoft.AspNetCore.Mvc;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;

namespace New_Zealand.webApi.Repositories
{
    public interface IWalksRepository
    {
        Task<Walk> CreateWalksAsync(Walk walk);
        Task<List<Walk>>GetWalksAsync(string? filterOn=null, string? filterQuery=null,
            string? sortBy=null, bool isAscending=true,int pageNumber=1, int pageSize=1000);
        Task<Walk?> GetOneWalksAsync(Guid id);
        Task<Walk?> UpdateWalksAsync(Guid id, Walk walk);
    }
}
