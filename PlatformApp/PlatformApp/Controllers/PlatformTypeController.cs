using Microsoft.AspNetCore.Mvc;
using PlatformApp.Models;
using PlatformApp.Models.DTO;
using PlatformApp.Models.DTO.PlatformTypeDTO;
using PlatformApp.Repository.Service;

namespace PlatformApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformTypeController : ControllerBase
    {
        private readonly IMappingDTO _mapper;
        private readonly IPlatformTypeRepository _repository;

        public PlatformTypeController(IPlatformTypeRepository platformTypeRepository, IMappingDTO mapper)
        {
            _repository = platformTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformTypeReadDTO>>> GetPlatformTypesAsync(string? name)
        {
            IEnumerable<PlatformTypeReadDTO> dtos;
            if (!string.IsNullOrEmpty(name))
            {
                dtos = from x in await _repository.GetPlatformTypesByNameAsync(name)
                       select _mapper.MapPlatformTypeReadDTO(x);
            } else
            {
                dtos = from x in await _repository.GetPlatformTypesAsync()
                       select _mapper.MapPlatformTypeReadDTO(x);
            }

            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetPlatformTypeByIdAsync")]
        public async Task<ActionResult<PlatformTypeReadDTO>> GetPlatformTypeByIdAsync(int id)
        {
            var platformType = await _repository.GetPlatformTypeByIdAsync(id);
            if (!platformType.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.MapPlatformTypeReadDTO(platformType.First()));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformTypeReadDTO>> InsertPlatformAsync(PlatformTypeInsertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                PlatformType platformType = _mapper.MapPlatformTypeInsertDTO(dto);
                if (await _repository.InsertPlataformTypeAsync(platformType))
                {
                    PlatformTypeReadDTO readDTO = _mapper.MapPlatformTypeReadDTO(platformType);
                    return CreatedAtAction(nameof(GetPlatformTypeByIdAsync), new { id = platformType.Id }, readDTO);
                }
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlatformTypeAsync(int id)
        {
            if (await _repository.DeletePlataformTypeAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlatformAsync(int id, PlatformTypeUpdateDTO dto)
        {
            var platformType = await _repository.GetPlatformTypeByIdAsync(id);

            if (platformType.Any())
            {
                var pt = platformType.First();
                _mapper.MapPlatformTypeUpdateDTO(dto, pt);
                await _repository.UpdatePlataformTypeAsync(pt);
                return NoContent();
            }
            return NotFound();
        }
    }
}
