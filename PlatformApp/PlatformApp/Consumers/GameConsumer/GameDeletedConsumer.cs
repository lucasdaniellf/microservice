using Contracts;
using MassTransit;
using PlatformApp.Models.GameLibrary;
using PlatformApp.Repository.Service.GamesLibrary;

namespace PlatformApp.Consumers.GameConsumer
{
    public class GameDeletedConsumer : IConsumer<GameDeleted>
    {
        private readonly IGameRepository _repoository;

        public GameDeletedConsumer(IGameRepository repo)
        {
            _repoository = repo;
        }
        public async Task Consume(ConsumeContext<GameDeleted> context)
        {
            var message = context.Message;
            await _repoository.DeleteGameByJogoAsync(message.gameId);

            return;
        }
    }
}
