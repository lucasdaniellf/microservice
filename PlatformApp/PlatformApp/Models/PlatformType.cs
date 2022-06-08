namespace PlatformApp.Models
{
    public class PlatformType
    {
        public int Id { get; init; }
        public string Description { get; set; } = null!;
        public virtual IEnumerable<Platform> Platforms { get; set; } = new List<Platform>();
    }
}
