using DataRepository;
using DataRepository.Interface;
using Models;

namespace ApiGraphQL.UtilGraphQL.Types
{
    public class EstudioType : ObjectType<Estudio>
    {
        protected override void Configure(IObjectTypeDescriptor<Estudio> descriptor)
        {
            descriptor.Description("Estúdios que publicaram os jogos");
            descriptor.Field("jogos")
                .ResolveWith<Resolvers>(x => x.GetJogos(default!));

        }

        private class Resolvers
        {
            private readonly IJogoRepository _repository;
            private readonly DbContext _context;


            public Resolvers(IJogoRepository repository, DbContext _context)
            {
                this._context = _context; 
                _repository = repository;
            }

            public IEnumerable<Jogo> GetJogos([Parent] Estudio estudio)
            {
                return _repository.SelectJogosPorEstudio(_context, estudio);
            }
        }
    }
}
