using DataRepository;
using DataRepository.Interface;
using Models;
namespace ApiGraphQL.UtilGraphQL.Types
{
    public class GeneroType : ObjectType<Genero>
    {
        protected override void Configure(IObjectTypeDescriptor<Genero> descriptor)
        {
            descriptor.Description("Modelo referente aos gêneros que um jogo pode ter");
            descriptor.Field("jogos")
                .ResolveWith<Resolvers>(g =>
                g.GetJogos(default!));
        }
        private class Resolvers
        {
            private readonly IJogoGeneroRepository _jogoGeneroRepository;
            private readonly DbContext _context;

            public Resolvers(IJogoGeneroRepository _jogoGeneroRepository, DbContext _context)
            {
                this._jogoGeneroRepository = _jogoGeneroRepository;
                this._context = _context;
            }


            public IEnumerable<Jogo> GetJogos([Parent] Genero genero)
            {
                return _jogoGeneroRepository.SelectJogosPorGenero(_context, genero);
            }
        }

    }
}
