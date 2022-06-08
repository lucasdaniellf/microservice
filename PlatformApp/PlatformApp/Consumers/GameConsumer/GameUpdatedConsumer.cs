using Contracts;
using MassTransit;
using PlatformApp.Models.GameLibrary;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Consumers.GameConsumer
{
    public class GameUpdatedConsumer : IConsumer<GameUpdated>
    {
        private readonly IGameRepository _repository;

        public GameUpdatedConsumer(IGameRepository repo)
        {
            _repository = repo;
        }

        public async Task Consume(ConsumeContext<GameUpdated> context)
        {
            var message = context.Message;
            var list_game = await _repository.GetGamesByJogoIdAsync(message.gameId);

            if(list_game.Any())
            {
                Game g = list_game.First();

                g.Name = message.name;
                await _repository.UpdateGameAsync(g);
                Console.WriteLine($"Game {g.Name} updated");

                return;
            }

            Game game = new Game{
                JogoId = message.gameId,
                Name = message.name
            };
            await _repository.InsertGameAsync(game);

            return; 

        }
    }
}
