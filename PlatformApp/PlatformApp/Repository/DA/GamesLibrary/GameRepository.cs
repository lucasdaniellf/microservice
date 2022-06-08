using Microsoft.EntityFrameworkCore;
using PlatformApp.Models.GameLibrary;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Repository.DA.GamesLibrary
{
    public class GameRepository : IGameRepository
    {
        private readonly PlatformDbContext _context;

        public GameRepository(PlatformDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var game = await (from x in _context.games.Include(x => x.Platforms)
                                where x.Id == id
                                select x).ToListAsync();

            if(game.Any())
            {
                _context.games.Remove(game.First());
            }
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteGameByJogoAsync(int id)
        {
            var game = await (from x in _context.games.Include(x => x.Platforms)
                              where x.JogoId == id
                              select x).ToListAsync();

            if (game.Any())
            {
                _context.games.Remove(game.First());
            }
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _context.games
                .Include(x => x.Platforms)
                .ThenInclude(x => x.Platform).ToListAsync();
        }
        public async Task<IEnumerable<Game>> GetGamesByJogoIdAsync(int id)
        {
            return await (from x in _context.games
                          where x.JogoId == id
                          select x).ToListAsync();
        }
        public async Task<IEnumerable<Game>> GetGamesByIdAsync(int id)
        {
            return await (from x in _context.games
                          where x.Id == id
                          select x).ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByNameAsync(string name)
        {
            return await (from x in _context.games
                          where x.Name.ToLower().Contains(name.ToLower())
                          select x).ToListAsync();
        }

        public Task<bool> InsertGameAsync(Game game)
        {
            _context.games.Add(game);
            return SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsGameAsync(int id)
        {
            return await _context.games.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateGameAsync(Game game)
        {
            return await SaveChangesAsync();
        }
    }
}
