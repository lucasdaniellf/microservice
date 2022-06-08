namespace PlatformApp.Models.DTO.PlatformDTO
{
    public class PlatformReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public Platform_PlatformType_ReadDTO? PlatformType { get; set; }
    }
}
