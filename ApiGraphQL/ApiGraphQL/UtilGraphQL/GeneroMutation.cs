using DataRepository;
using DataRepository.Inputs;
using DataRepository.Payloads;
using Models;

namespace MyGraphQLAPI.Repository.UtilGraphQL
{
    public partial class Mutation
    {
        public GeneroPayload AddGenero(AddGeneroInput input)
        {
            int id = _generoRepository.InsertGenero(_context, input);
            if (id > 0)
            {
                Genero? genero = _generoRepository.SelectGeneroPorID(_context, id) ?? throw new GraphQLException(new Error("Falha ao Obter Gênero", "ERRO_CONEXÂO_BANCO"));
                GeneroPayload payload = new GeneroPayload(genero);
                return payload;
            }
            throw new GraphQLException(new Error("Falha ao Inserir Gênero", "ERRO_CONEXÂO_BANCO"));
        }

        public GeneroPayload UpdateGenero(UpdateGeneroInput input)
        {
            if (_generoRepository.UpdateGenero(_context, input))
            {
                Genero genero = _generoRepository.SelectGeneroPorID(_context, input.id) ?? throw new GraphQLException(new Error("Falha ao Atualizar Gênero", "ERRO_CONEXÂO_BANCO"));
                GeneroPayload payload = new GeneroPayload(genero); 
                return payload;
            }
            throw new GraphQLException(new Error("Genero não encontrado", "ID_GENERO_INEXISTENTE"));
        }

        public bool DeleteGenero(DeleteGeneroInput input)
        {
            if(_generoRepository.DeleteGenero(_context, input))
            {
                return true;
            }
            throw new GraphQLException(new Error("Genero não encontrado", "ID_GENERO_INEXISTENTE"));
        }
    }
}
