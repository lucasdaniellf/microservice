using PlatformApp.Models;

namespace PlatformApp.Repository.Service
{
    public interface IPlatformTypeRepository
    {
        public Task<IEnumerable<PlatformType>> GetPlatformTypesAsync();
        public Task<IEnumerable<PlatformType>> GetPlatformTypesByNameAsync(string name);
        public Task<IEnumerable<PlatformType>> GetPlatformTypeByIdAsync(int id);
        public Task<bool> InsertPlataformTypeAsync(PlatformType platformType);
        public Task<bool> UpdatePlataformTypeAsync(PlatformType platformType);
        public Task<bool> DeletePlataformTypeAsync(int id);
        public Task<bool> ExistsPlatformTypeAsync(int id);
        public Task<bool> SaveChangesAsync();
    }
}
