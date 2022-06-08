using PlatformApp.Models.DTO.GameDTO;
using PlatformApp.Models.DTO.PlatformDTO;

namespace PlatformApp.Models.DTO.PlatformGameDTO
{
    public class PlatformGameReadDTO
    {
        public int GamePrice { get; set; }
        public int GameQtySold { get; set; }
        public GameReadDTO Game { get; set; } = null!;
        public Platform_PlatformGame_ReadDTO Platform { get; set; } = null!;
    }
}
