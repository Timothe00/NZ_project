using AutoMapper;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;

namespace New_Zealand.webApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Regions, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Regions>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Regions>().ReverseMap();

            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalksRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

        }
    }
}
