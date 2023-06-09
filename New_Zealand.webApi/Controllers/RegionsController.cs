using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using New_Zealand.webApi.CustomeActionFilters;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;
using New_Zealand.webApi.Repositories;

namespace New_Zealand.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {


        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        //injection de IRegionRepository dans le constructeur
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //get data from database - Domain models
            var regionsDomain = await regionRepository.GetAllRegionsAsync();
            //Map domain Models to DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            /*            var regionsDto = new List<RegionDto>();
                        foreach (var regionDomain in regionsDomain)
                        {
                            regionsDto.Add(new RegionDto()
                            {
                                Id= regionDomain.Id,
                                Code= regionDomain.Code,
                                Name= regionDomain.Name,
                                RegionImageUrl= regionDomain.RegionImageUrl
                            });
                        }*/
            //return dtos

        }


        //get region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetOneByIdRegions([FromRoute] Guid id)
        {

            //with link relation
            //get region domain model from database
            var regionsDomainModel = await regionRepository.GetOneByIdRegionsAsync(id);

            if (regionsDomainModel == null)
            {
                return NotFound();
            }
            //return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionsDomainModel));

            /* var OneRegion = _dbContext.Regionss.Find(id);
                if (OneRegion == null)
                {
                    return NotFound();
                }

                return Ok(OneRegion);
            */
        }


        //creation a region with a post metod
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {

            //map or Convert DTO to Domain Model
            var regionsDomainModel = mapper.Map<Regions>(addRegionRequestDto);

            regionsDomainModel = await regionRepository.CreateRegionAsync(regionsDomainModel);
            //Map Domain model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionsDomainModel);
            return CreatedAtAction(nameof(GetOneByIdRegions), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //map DTO to domain model
            var regionDomainModel = mapper.Map<Regions>(updateRegionRequestDto);
            //check if region exist
            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);
            
            //var regionDomainModel = await _dbContext.Regionss.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //convert domain model to DTO
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }


        //region delete method
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //return deleted Region back
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
