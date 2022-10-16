using Contracts;
using DataRepository;
using DataRepository.Inputs;
using DataRepository.Interface;
using DataRepository.Payloads;
using MassTransit;
using Models;

namespace MyGraphQLAPI.Repository.UtilGraphQL
{
    public partial class Mutation
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IJogoGeneroRepository _jogoGeneroRepository;
        private readonly IEstudioRepository _estudioRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly DbContext _context;
        private readonly IPublishEndpoint _publish;

        public Mutation(IJogoRepository jogoRepository, 
                        IEstudioRepository estudioRepository, 
                        IGeneroRepository generoRepository, 
                        IJogoGeneroRepository jogoGeneroRepository, 
                        DbContext _dbContext,
                        IPublishEndpoint publish)
        {
            _jogoRepository = jogoRepository;
            _estudioRepository = estudioRepository;
            _generoRepository = generoRepository;
            _jogoGeneroRepository = jogoGeneroRepository;
            _context = _dbContext;
            _publish = publish;
        }

        public JogoPayload AddJogo(AddJogoInput input)
        {
            var id = _jogoRepository.InsertJogo(_context, input);
            if (id > 0)
            {
                var jogo = _jogoRepository.SelectJogoPorID(_context, id) ?? throw new GraphQLException(new Error("Falha ao Obter Jogo", "ERRO_CONEXÂO_BANCO"));
                JogoPayload payload = new JogoPayload(jogo);

                _publish.Publish(new GameCreated(jogo.Id, jogo.Nome));
                Console.WriteLine($"Game {jogo.Nome} created");
                return payload;
            }
            throw new GraphQLException(new Error("Falha ao Inserir Jogo", "ERRO_CONEXÂO_BANCO"));
        }

        public JogoPayload UpdateJogo(UpdateJogoInput input)
        {
            if(_jogoRepository.UpdateJogo(_context, input))
            {
                Jogo jogo = _jogoRepository.SelectJogoPorID(_context, input.Id) ?? throw new GraphQLException(new Error("Falha ao Obter Jogo", "ERRO_CONEXÂO_BANCO"));
                JogoPayload payload = new JogoPayload(jogo);

                _publish.Publish(new GameUpdated(jogo.Id, jogo.Nome));
                Console.WriteLine($"Game {jogo.Nome} updated");

                return payload;
                
            }
            throw new GraphQLException(new Error("Jogo não encontrado", "ID_JOGO_INEXISTENTE"));
        }

        public async Task<JogoPayload> UpdateAddJogoGenero(UpdateJogoGeneroInput input)
        {
            if(await _jogoGeneroRepository.UpdateAddGeneroToJogo(_context, input))
            {
                Jogo jogo = _jogoRepository.SelectJogoPorID(_context, input.JogoId) ?? throw new GraphQLException(new Error("Falha ao Obter Jogo", "ERRO_CONEXÂO_BANCO"));
                JogoPayload payload = new JogoPayload(jogo);
                return payload;
            }
            throw new GraphQLException(new Error("Jogo não encontrado", "ID_JOGO_INEXISTENTE"));
        }

        public JogoPayload UpdateJogoEstudio(UpdateJogoIdEstudioInput input)
        {
            if (_jogoRepository.UpdateEstudioJogo(_context, input))
            {
                Jogo jogo = _jogoRepository.SelectJogoPorID(_context, input.IdJogo) ?? throw new GraphQLException(new Error("Falha ao Obter Jogo", "ERRO_CONEXÂO_BANCO"));
                JogoPayload payload = new JogoPayload(jogo);
                return payload;
            }
            throw new GraphQLException(new Error("Jogo não encontrado", "ID_JOGO_INEXISTENTE"));
        }

        public async Task<JogoPayload> UpdateRemoveJogoGenero(UpdateJogoGeneroInput input)
        {

            if (await _jogoGeneroRepository.UpdateRemoveGeneroFromJogo(_context, input))
            {
                Jogo jogo = _jogoRepository.SelectJogoPorID(_context, input.JogoId) ?? throw new GraphQLException(new Error("Falha ao Obter Jogo", "ERRO_CONEXÂO_BANCO"));
                JogoPayload payload = new JogoPayload(jogo);
                return payload;
            }
            throw new GraphQLException(new Error("Jogo não encontrado", "ID_JOGO_INEXISTENTE"));
        }

        public bool DeleteJogo(DeleteJogoInput input)
        {
            if(_jogoRepository.DeleteJogo(_context, input))
            {
                _publish.Publish(new GameDeleted(input.Id));
                Console.WriteLine($"Game deleted");
                return true;
            }
            throw new GraphQLException(new Error("Jogo não encontrado", "ID_JOGO_INEXISTENTE"));
        }
    }
}
