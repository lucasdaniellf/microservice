using System.ComponentModel.DataAnnotations;

namespace PlatformApp.Models.GameLibrary
{
    public class PlatformGame
    {
        public int GameId { get; init; }
        public int PlatformId { get; init; }
        public int GamePrice { get; set; }
        public int GameQtySold { get; set; }
        public virtual Game Game { get; set; } = null!;
        public virtual Platform Platform { get; set; } = null!;
    }
}
