using PlatformApp.Models;
using PlatformApp.Models.DTO.PlatformDTO;

namespace PlatformApp.Repository.Service
{
    public interface IPlatformRepository
    {
        public Task<IEnumerable<Platform>> GetPlatformsAsync();
        public Task<IEnumerable<Platform>> GetPlatformByNameAsync(string name);
        public Task<IEnumerable<Platform>> GetPlatformByIdAsync(int id);
        public Task<bool> DeletePlatformAsync(int id);
        public Task<bool> UpdatePlatformAsync(Platform platform);
        public Task<bool> InsertPlatformAsync(Platform platform);
        public Task<bool> ExistsPlatformAsync(int id);
        public Task<bool> SaveChangesAsync();
    }
}
