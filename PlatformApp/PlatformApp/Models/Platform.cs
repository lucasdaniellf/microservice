using PlatformApp.Models.GameLibrary;

namespace PlatformApp.Models
{
    public class Platform
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public int? PlatformTypeId { get; set; } = null;
        public virtual PlatformType? platformType { get; set; } = null;
        public virtual IEnumerable<PlatformGame> Games { get; set; } = new List<PlatformGame>();
    }
}
