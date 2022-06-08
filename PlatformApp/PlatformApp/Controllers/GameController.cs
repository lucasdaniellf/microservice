using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformApp.Models.DTO;
using PlatformApp.Models.DTO.GameDTO;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMappingDTO _mapper;
        private readonly IGameRepository _repository;

        public GameController(IGameRepository repository, IMappingDTO mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameWithPlatformsReadDto>>> GetGamesAsync()
        {
            List<GameWithPlatformsReadDto> list = (from x in await _repository.GetGamesAsync()
                                              select _mapper.MapGameWithPlatformsDTO(x)).ToList();

            return Ok(list);
        }
    }
}
