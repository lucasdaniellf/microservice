using DataRepository;
using DataRepository.Inputs;
using DataRepository.Payloads;
using Models;

namespace MyGraphQLAPI.Repository.UtilGraphQL
{
    public partial class Mutation
    {
        public EstudioPayload AddEstudio(AddEstudioInput input)
        {
            var id = _estudioRepository.InsertEstudio(_context, input);
            if (id > 0)
            {
                var estudio = _estudioRepository.SelectEstudioPorID(_context, id) ?? throw new GraphQLException(new Error("Falha ao Obter Estudio", "ERRO_CONEXÂO_BANCO"));
                EstudioPayload payload = new EstudioPayload(estudio);
                return payload;
            }
            throw new GraphQLException(new Error("Falha ao Inserir Estúdio", "ERRO_CONEXÂO_BANCO"));
        }

        public EstudioPayload UpdateEstudio(UpdateEstudioInput input)
        {
            if(_estudioRepository.UpdateEstudio(_context, input))
            {
                Estudio estudio = _estudioRepository.SelectEstudioPorID(_context, input.id) ?? throw new GraphQLException(new Error("Falha ao Obter Estudio", "ERRO_CONEXÂO_BANCO"));
                EstudioPayload payload = new EstudioPayload(estudio);
                return payload;
            }
            throw new GraphQLException(new Error("Estúdio não encontrado", "ID_ESTUDIO_INEXISTENTE"));
        }

        public bool DeleteEstudio(DeleteEstudioInput input)
        {
            if(_estudioRepository.DeleteEstudio(_context, input))
            {
                return true;
            }
            throw new GraphQLException(new Error("Estúdio não encontrado", "ID_ESTUDIO_INEXISTENTE"));

        }
    }
}
