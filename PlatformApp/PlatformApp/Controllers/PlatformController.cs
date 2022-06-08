using Microsoft.AspNetCore.Mvc;
using PlatformApp.Models;
using PlatformApp.Models.DTO;
using PlatformApp.Models.DTO.PlatformDTO;
using PlatformApp.Repository.Service;

namespace PlatformApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IMappingDTO _mapper;
        private readonly IPlatformRepository _repository;

        public PlatformController(IPlatformRepository platformRepository, IMappingDTO mapper )
        {
            _repository = platformRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDTO>>> GetPlatformsAsync(string? name)
        {
            IEnumerable<PlatformReadDTO> dtos;

            if (!string.IsNullOrEmpty(name))
            {
                dtos = from platform in await _repository.GetPlatformByNameAsync(name)
                                                    select _mapper.MapPlatformReadDTO(platform);
                
            } 
            else
            {
                dtos = from platform in await _repository.GetPlatformsAsync()
                       select _mapper.MapPlatformReadDTO(platform);
            }

            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetPlatformByIdAsync")]
        public async Task<ActionResult<PlatformReadDTO>> GetPlatformByIdAsync(int id)
        {
            IEnumerable<Platform> list = await _repository.GetPlatformByIdAsync(id);

            if (!list.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.MapPlatformReadDTO(list.First()));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> InsertPlatformAsync(PlatformInsertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            } else
            {
                Platform platform = _mapper.MapPlatformInsertDTO(dto);
                if (await _repository.InsertPlatformAsync(platform))
                {
                    PlatformReadDTO readDto = _mapper.MapPlatformReadDTO(platform);
                    return CreatedAtAction(nameof(GetPlatformByIdAsync), new { id = platform.Id }, readDto);
                }
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlatformAsync(int id)
        {
            if (await _repository.DeletePlatformAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlatformAsync(int id, PlatformUpdateDTO dto)
        {
            IEnumerable<Platform> list = await _repository.GetPlatformByIdAsync(id);


            if (list.Any())
            {
                Platform p = list.First();

                _mapper.MapPlatformUpdateDTO(dto, p);
                await _repository.UpdatePlatformAsync(p);
                return NoContent();
            }
            return NotFound();
        }
    }
}
