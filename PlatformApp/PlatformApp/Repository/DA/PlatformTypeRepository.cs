using Microsoft.EntityFrameworkCore;
using PlatformApp.Models;
using PlatformApp.Models.DTO;
using PlatformApp.Repository.Service;

namespace PlatformApp.Repository.DA
{
    public class PlatformTypeRepository : IPlatformTypeRepository
    {

        private readonly PlatformDbContext _context;

        public PlatformTypeRepository(PlatformDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePlataformTypeAsync(int id)
        {
            var platformType = await (from x in _context.platformsType
                                                .Include(x => x.Platforms)
                                          where x.Id == id
                                          select x).ToListAsync();

            if (platformType.Any())
            {
                _context.platformsType.Remove(platformType.First());
            }

            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<PlatformType>> GetPlatformTypeByIdAsync(int id)
        {
            return await (from x in _context.platformsType
                          where x.Id == id
                            select x).ToListAsync();
        }

        public async Task<IEnumerable<PlatformType>> GetPlatformTypesAsync()
        {
            return await (from x in _context.platformsType
                          select x).ToListAsync();
        }

        public async Task<IEnumerable<PlatformType>> GetPlatformTypesByNameAsync(string name)
        {
            return await (from x in _context.platformsType
                          where x.Description.ToLower().Contains(name.ToLower())
                          select x).ToListAsync();
        }
        
        public async Task<bool> InsertPlataformTypeAsync(PlatformType platformType)
        {
            _context.platformsType.Add(platformType);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> ExistsPlatformTypeAsync(int id)
        {
            return await _context.platformsType.AnyAsync(x => x.Id == id);
        }
        public async Task<bool> UpdatePlataformTypeAsync(PlatformType platformType)
        {
            platformType.Platforms = (from x in _context.platforms
                                      where x.PlatformTypeId == platformType.Id
                                      select x).ToList();

            return await SaveChangesAsync();
        }
    }
}
