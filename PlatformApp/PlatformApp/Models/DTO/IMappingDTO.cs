using PlatformApp.Models;
using PlatformApp.Models.DTO.GameDTO;
using PlatformApp.Models.DTO.PlatformDTO;
using PlatformApp.Models.DTO.PlatformGameDTO;
using PlatformApp.Models.DTO.PlatformTypeDTO;
using PlatformApp.Models.GameLibrary;

namespace PlatformApp.Models.DTO
{
    public interface IMappingDTO
    {
        public PlatformReadDTO MapPlatformReadDTO(Platform platform);
        public GameWithPlatformsReadDto MapGameWithPlatformsDTO(Game game);
        public Platform MapPlatformInsertDTO(PlatformInsertDTO dto);
        public void MapPlatformUpdateDTO(PlatformUpdateDTO dto, Platform platform);
        public Platform_PlatformType_ReadDTO MapPlatform_PlatformType_ReadDTO(PlatformType platformType);
        public Platform_PlatformGame_ReadDTO MapPlatform_PlatformGame_ReadDTO(Platform platform);
        public PlatformTypeReadDTO MapPlatformTypeReadDTO(PlatformType platformType);
        public PlatformType MapPlatformTypeInsertDTO(PlatformTypeInsertDTO dto);
        public void MapPlatformTypeUpdateDTO(PlatformTypeUpdateDTO dto, PlatformType platformType);
        public GameReadDTO MapGameDTO(Game game);
        public PlatformGameReadDTO MapPlatformGameReadDTO(PlatformGame platformGame);
        public PlatformGame MapPlatformGameInsertDTO(int PlatformId, int GameId, PlatformGameInsertDTO dto);
        public void MapPlatformGameUpdateDTO(PlatformGameUpdateDTO dto, PlatformGame platformGame);
    }
}
