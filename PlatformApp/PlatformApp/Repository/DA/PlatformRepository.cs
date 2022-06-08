using Microsoft.EntityFrameworkCore;
using PlatformApp.Models;
using PlatformApp.Models.DTO;
using PlatformApp.Models.DTO.PlatformDTO;
using PlatformApp.Repository.Service;

namespace PlatformApp.Repository.DA
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly PlatformDbContext _dbContext;

        public PlatformRepository(PlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeletePlatformAsync(int id)
        {

            var platform = await (from x in _dbContext.platforms
                                        .Include(x => x.platformType)
                                        .Include(x => x.Games)
                                  where x.Id == id
                                  select x).ToListAsync();

            if (platform.Any())
            {
                _dbContext.Remove(platform.First());
            }

            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Platform>> GetPlatformByIdAsync(int id)
        {
            return await (from platform in _dbContext.platforms
                          .Include(x => x.platformType)
                    where platform.Id == id
                    select platform).ToListAsync();
        }

        public async Task<IEnumerable<Platform>> GetPlatformByNameAsync(string name)
        {
            return await (from platform in _dbContext.platforms.Include(x => x.platformType)
                   where platform.Name.ToLower().Contains(name.ToLower())
                   select platform).ToListAsync();
        }

        public async Task<IEnumerable<Platform>> GetPlatformsAsync()
        {
            return await (from platform in _dbContext.platforms.Include(x => x.platformType)
                          select platform).ToListAsync();
        }

        public async Task<bool> InsertPlatformAsync(Platform platform)
        {
            int ptId = platform.PlatformTypeId ?? 0;

            if (ptId > 0)
            {
                platform.platformType = (from x in _dbContext.platformsType
                                         where x.Id == platform.PlatformTypeId
                                         select x).ToList().FirstOrDefault();
            }

            _dbContext.platforms.Add(platform);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePlatformAsync(Platform platform)
        {
            int ptId = platform.PlatformTypeId ?? 0;

            if (ptId > 0)
            {
                platform.platformType = (from x in _dbContext.platformsType
                                         where x.Id == platform.PlatformTypeId
                                         select x).ToList().FirstOrDefault();
            }
            return await SaveChangesAsync();
        }

        public async Task<bool> ExistsPlatformAsync(int id)
        {
            return await _dbContext.platforms.AnyAsync(x => x.Id == id);
        }
    }
}
