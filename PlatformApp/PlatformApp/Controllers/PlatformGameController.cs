using Microsoft.AspNetCore.Mvc;
using PlatformApp.Models.DTO;
using PlatformApp.Models.DTO.PlatformGameDTO;
using PlatformApp.Models.GameLibrary;
using PlatformApp.Repository.Service;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Controllers
{
    [Route("api/Platform/{PlatformId}/Game")]
    [ApiController]
    public class PlatformGameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlatformRepository _platformRepository;
        private readonly IPlatformGameRepository _gamePlatRepo;
        private readonly IMappingDTO _mapper;

        public PlatformGameController(IGameRepository gameRepo, 
                                        IPlatformRepository platformRepo,
                                        IPlatformGameRepository gamePlatRepo,
                                        IMappingDTO mapper)
        {
            _gameRepository = gameRepo;
            _platformRepository = platformRepo;
            _gamePlatRepo = gamePlatRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformGameReadDTO>>> GetGamesInPlatformAsync(int PlatformId)
        {
            List<PlatformGameReadDTO> list = (from x in await _gamePlatRepo.GetGamesInPlatformAsync(PlatformId)
                                      select _mapper.MapPlatformGameReadDTO(x)).ToList();

            return Ok(list);
        }

        [HttpGet("{GameId}", Name = "GetGameByIdInPlatformByIdAsync")]
        public async Task<ActionResult<PlatformGameReadDTO>> GetGamesByIdInPlatformAsync(int PlatformId, int GameId)
        {
            List<PlatformGame> list = (await _gamePlatRepo.GetGamesByIdInPlatformAsync(GameId, PlatformId)).ToList();

            if (list.Count > 0)
            {
                return _mapper.MapPlatformGameReadDTO(list.First());
            }

            return NotFound();
        }

        [HttpPost("{GameId}")]
        public async Task<ActionResult<PlatformGameReadDTO>> AddGameToPlatformAsync(int PlatformId, int GameId, PlatformGameInsertDTO dto)
        {
            if (ModelState.IsValid)
            {
                if ((await _platformRepository.ExistsPlatformAsync(PlatformId)) && (await _gameRepository.ExistsGameAsync(GameId)))
                {
                    List<PlatformGame> list = (await _gamePlatRepo.GetGamesByIdInPlatformAsync(GameId, PlatformId)).ToList();

                    if (list.Count > 0)
                    {
                        return BadRequest("Jogo já cadastrado na plataforma");
                    }
                    
                    PlatformGame pg = _mapper.MapPlatformGameInsertDTO(PlatformId, GameId, dto);

                    if (await _gamePlatRepo.InsertGameInPlatformAsync(pg))
                    {
                        return CreatedAtAction(nameof(GetGamesByIdInPlatformAsync), new { pg.PlatformId, pg.GameId }, _mapper.MapPlatformGameReadDTO(pg)); ;
                    }
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveGameFromPlatformAsync(int PlatformId, int GameId)
        {

            if (await _gamePlatRepo.DeleteGameFromPlatformAsync(GameId, PlatformId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut("{GameId}")]
        public async Task<ActionResult> UpdateGameInfoPlatformAsync(int PlatformId, int GameId, PlatformGameUpdateDTO dto)
        {
            IEnumerable<PlatformGame> list = await _gamePlatRepo.GetGamesByIdInPlatformAsync(GameId, PlatformId);
            if (list.Any())
            {       
                PlatformGame pg = list.First();
                
                _mapper.MapPlatformGameUpdateDTO(dto, pg);
                await _gamePlatRepo.UpdateGameInfoInPlatformAsync(pg);

                return NoContent();
            }
            return NotFound();
        }
    }
}
