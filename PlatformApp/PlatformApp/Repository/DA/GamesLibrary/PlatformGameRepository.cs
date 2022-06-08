using Microsoft.EntityFrameworkCore;
using PlatformApp.Models.GameLibrary;
using PlatformApp.Repository.Service;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Repository.DA.GamesLibrary
{
    public class PlatformGameRepository : IPlatformGameRepository
    {
        private readonly PlatformDbContext _context;
        private readonly IPlatformRepository _platRepo;
        private readonly IGameRepository _gameRepo;

        public PlatformGameRepository(PlatformDbContext context, IPlatformRepository platRepo, IGameRepository gameRepo)
        {
            _context = context;
            _platRepo = platRepo;
            _gameRepo = gameRepo;
        }

        public async Task<bool> DeleteGameFromPlatformAsync(int Gameid, int PlatformId)
        {
            var p = await (from x in _context.platformGame
                               .Include(x => x.Game)
                               .Include(x => x.Platform)
                               where x.GameId == Gameid && x.PlatformId == PlatformId
                               select x).ToListAsync();

            if(p.Any())
            {
                _context.platformGame.Remove(p.First());
            }

            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<PlatformGame>> GetGameByNameInPlatformAsync(string name, int platformId)
        {
            return await (from x in _context.platformGame
                                            .Include(x => x.Game)
                                            .Include(x => x.Platform)
                          where x.Game.Name.ToLower() == name.ToLower() && x.PlatformId == platformId
                          select x).ToListAsync();
        }

        public async Task<IEnumerable<PlatformGame>> GetGamesByIdInPlatformAsync(int gameId, int platformId)
        {
            return await (from x in _context.platformGame
                               .Include(x => x.Game)
                               .Include(x => x.Platform)
                         where x.PlatformId == platformId && x.GameId == gameId
                         select x).ToListAsync();
        }

        public async Task<IEnumerable<PlatformGame>> GetGamesInPlatformAsync(int platformId)
        {
            return await (from x in _context.platformGame
                               .Include(x => x.Game)
                               .Include(x => x.Platform)
                          where x.PlatformId == platformId
                          select x).ToListAsync();
        }

        public async Task<bool> InsertGameInPlatformAsync(PlatformGame platformGame)
        {
            platformGame.Platform = (await _platRepo.GetPlatformByIdAsync(platformGame.PlatformId)).First();
            platformGame.Game = (await _gameRepo.GetGamesByIdAsync(platformGame.GameId)).First();

            _context.platformGame.Add(platformGame);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGameInfoInPlatformAsync(PlatformGame platformGame)
        {
            return await SaveChangesAsync();
        }
    }
}
