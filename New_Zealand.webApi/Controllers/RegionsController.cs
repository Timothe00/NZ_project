using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using New_Zealand.webApi.Data;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;

namespace New_Zealand.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly New_ZealandDbContext _dbContext;
        public RegionsController(New_ZealandDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllRegions()
        {
            //get data from database - Domain models
            var regionsDomain = _dbContext.Regionss.ToList();

            //Map domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id= regionDomain.Id,
                    Code= regionDomain.Code,
                    Name= regionDomain.Name,
                    RegionImageUrl= regionDomain.RegionImageUrl
                });
            }
            //return dtos
            return Ok(regionsDto);
        }


        //get region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetOneByIdRegions([FromRoute] Guid id)
        {

            //with link relation
            //get region domain model from database
            var regionsDomain = _dbContext.Regionss.FirstOrDefault(x => x.Id == id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            var regionsDto = new RegionDto
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl
            };

            return Ok(regionsDomain);

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
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            //map or Convert DTO to Domain Model
            var regionDomainModel = new Regions
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to create region

            _dbContext.Regionss.Add(regionDomainModel);
            _dbContext.SaveChanges();

            //Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetOneByIdRegions), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //check if region exist
            var regionDomainModel = _dbContext.Regionss.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map DTO to domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            _dbContext.SaveChanges();

            //convert domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        //region delete method
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = _dbContext.Regionss.FirstOrDefault(x=>x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //delete region
            _dbContext.Regionss.Remove(regionDomainModel);
            _dbContext.SaveChanges();



            //return deleted Region back
            //map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
