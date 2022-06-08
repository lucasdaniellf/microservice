using System.ComponentModel.DataAnnotations;

namespace PlatformApp.Models.GameLibrary
{
    public class Game
    {
        public int Id { get; init; }
        public int JogoId { get; init; }
        public string Name { get; set; } = null!;
        public virtual IEnumerable<PlatformGame> Platforms { get; set; } = new List<PlatformGame>();

    }
}
