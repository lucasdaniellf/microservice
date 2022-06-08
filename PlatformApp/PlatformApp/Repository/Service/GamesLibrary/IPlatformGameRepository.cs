using PlatformApp.Models.GameLibrary;

namespace PlatformApp.Repository.Service.GamesLibrary
{
    public interface IPlatformGameRepository
    {
        public Task<IEnumerable<PlatformGame>> GetGamesInPlatformAsync(int platformId);
        public Task<IEnumerable<PlatformGame>> GetGameByNameInPlatformAsync(string name, int platformId);
        public Task<IEnumerable<PlatformGame>> GetGamesByIdInPlatformAsync(int gameId, int platformId);
        public Task<bool> DeleteGameFromPlatformAsync(int gameId, int platformId);
        public Task<bool> UpdateGameInfoInPlatformAsync(PlatformGame platformGame);
        public Task<bool> InsertGameInPlatformAsync(PlatformGame platformGame);
        public Task<bool> SaveChangesAsync();
    }
}
