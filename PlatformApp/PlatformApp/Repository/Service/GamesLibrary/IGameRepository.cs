using PlatformApp.Models.GameLibrary;

namespace PlatformApp.Repository.Service.GamesLibrary
{
    public interface IGameRepository
    {
        public Task<IEnumerable<Game>> GetGamesAsync();
        public Task<IEnumerable<Game>> GetGamesByNameAsync(string name);
        public Task<IEnumerable<Game>> GetGamesByIdAsync(int id);
        public Task<IEnumerable<Game>> GetGamesByJogoIdAsync(int id);
        public Task<bool> DeleteGameAsync(int id);
        public Task<bool> DeleteGameByJogoAsync(int id);
        public Task<bool> UpdateGameAsync(Game game);
        public Task<bool> InsertGameAsync(Game game);
        public Task<bool> ExistsGameAsync(int id);
        public Task<bool> SaveChangesAsync();
    }
}
