using Contracts;
using MassTransit;
using PlatformApp.Models.GameLibrary;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Consumers.GameConsumer
{
    public class GameCreatedConsumer : IConsumer<GameCreated>
    {
        private readonly IGameRepository _repository;

        public GameCreatedConsumer(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GameCreated> context)
        {

            var message = context.Message;
            var list_game = await _repository.GetGamesByJogoIdAsync(message.gameId);

            if (list_game.Any())
            {
                return;
            }

            Game game = new Game()
            {
                JogoId = message.gameId,
                Name = message.name
            };

            await _repository.InsertGameAsync(game);

            return;
        }
    }
}
