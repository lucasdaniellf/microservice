using Microsoft.EntityFrameworkCore;
using PlatformApp.Models.DTO.GameDTO;
using PlatformApp.Models.DTO.PlatformDTO;
using PlatformApp.Models.DTO.PlatformGameDTO;
using PlatformApp.Models.DTO.PlatformTypeDTO;
using PlatformApp.Models.GameLibrary;


namespace PlatformApp.Models.DTO
{
    public class MappingDTO : IMappingDTO
    {

        public GameReadDTO MapGameDTO(Game game)
        {
            return new GameReadDTO(
                game.Id,
                game.Name
            );         
        }

        public GameWithPlatformsReadDto MapGameWithPlatformsDTO(Game game)
        {
            return new GameWithPlatformsReadDto(
                game.Id,
                game.Name,
                (from x in game.Platforms
                select MapPlatform_PlatformGame_ReadDTO(x.Platform)).ToList()
            );
        }

        public PlatformGame MapPlatformGameInsertDTO(int PlatformId, int GameId, PlatformGameInsertDTO dto)
        {
            return new PlatformGame()
            {
                GameId = GameId,
                PlatformId = PlatformId,
                GamePrice = dto.GamePrice,
                GameQtySold = dto.GameQtySold
            };
        }

        public PlatformGameReadDTO MapPlatformGameReadDTO(PlatformGame platformGame)
        {
            return new PlatformGameReadDTO()
            {
                GamePrice = platformGame.GamePrice,
                GameQtySold = platformGame.GameQtySold,
                Game = MapGameDTO(platformGame.Game),
                Platform = MapPlatform_PlatformGame_ReadDTO(platformGame.Platform)
            };
        }

        public void MapPlatformGameUpdateDTO(PlatformGameUpdateDTO dto, PlatformGame platformGame) 
        {
            platformGame.GameQtySold = dto.GameQtySold;
            platformGame.GamePrice = dto.GamePrice;
        }

        //-------------------------------------------------------------------------------------------//
        public Platform MapPlatformInsertDTO(PlatformInsertDTO dto)
        {
            Platform p = new Platform()
            {
                Name = dto.Name,
                Company = dto.Company,
                PlatformTypeId = dto.PlatformTypeId
            };

            return p;
        }

        public PlatformReadDTO MapPlatformReadDTO(Platform platform)
        {
            PlatformReadDTO dto = new PlatformReadDTO()
            {
                Id = platform.Id,
                Name = platform.Name,
                Company = platform.Company,
                PlatformType = platform.platformType != null ? MapPlatform_PlatformType_ReadDTO(platform.platformType) : null
            };

            return dto;
        }

        public void MapPlatformUpdateDTO(PlatformUpdateDTO dto, Platform platform)
        {

            platform.Name = dto.Name;
            platform.Company = dto.Company;
            
            int ptId = dto.PlatformTypeId ?? 0;
            if (ptId > 0)
            {
                platform.PlatformTypeId = ptId;
            }
        }

        public Platform_PlatformGame_ReadDTO MapPlatform_PlatformGame_ReadDTO(Platform platform)
        {
            return new Platform_PlatformGame_ReadDTO()
            {
                Id = platform.Id,
                Name = platform.Name
            };
        }

        public Platform_PlatformType_ReadDTO MapPlatform_PlatformType_ReadDTO(PlatformType platformType)
        {
            Platform_PlatformType_ReadDTO dto = new Platform_PlatformType_ReadDTO()
            {
                Description = platformType.Description
            };
            return dto;
        }

        //------------------------------------------------------------------------------------------------------//

        public PlatformType MapPlatformTypeInsertDTO(PlatformTypeInsertDTO dto)
        {
            PlatformType platformType = new PlatformType()
            {
                Description = dto.Description
            };

            return platformType;
        }

        public PlatformTypeReadDTO MapPlatformTypeReadDTO(PlatformType platformType)
        {
            PlatformTypeReadDTO dto = new PlatformTypeReadDTO()
            {
                Id = platformType.Id,
                Description = platformType.Description
            };

            return dto;
        }

        public void MapPlatformTypeUpdateDTO(PlatformTypeUpdateDTO dto, PlatformType platformType)
        {

            platformType.Description = dto.Description;
        }
    }
}
