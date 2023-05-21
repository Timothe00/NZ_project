using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using New_Zealand.webApi.CustomeActionFilters;
using New_Zealand.webApi.Data;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;
using New_Zealand.webApi.Repositories;

namespace New_Zealand.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;
        //injection de IWalksRepository dans le constructeur
        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkDomainModel = await walksRepository.GetWalksAsync();
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetOneWalks([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRepository.GetOneWalksAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            //map or Convert DTO to Domain Model
            var walksDomainModel = mapper.Map<Walk>(addWalksRequestDto);
            await walksRepository.CreateWalksAsync(walksDomainModel);
            //Map Domain model to Dto
            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult>UpdateWalks([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateWalksRequestDto)
        {
            var walksDomainModel = mapper.Map<Walk>(updateWalksRequestDto);
            walksDomainModel= await walksRepository.UpdateWalksAsync(id, walksDomainModel);

            if(walksDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

    }
}
