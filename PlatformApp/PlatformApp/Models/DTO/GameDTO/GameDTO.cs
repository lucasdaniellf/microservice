using PlatformApp.Models.DTO.PlatformDTO;

namespace PlatformApp.Models.DTO.GameDTO
{
    public record GameReadDTO(int Id, string Name );
    public record GameWithPlatformsReadDto(int Id, string Name, IEnumerable<Platform_PlatformGame_ReadDTO> Platforms);
}
