using DataRepository;
using DataRepository.Interface;
using Models;

namespace ApiGraphQL.UtilGraphQL.Types
{
    public class JogoType : ObjectType<Jogo>
    {
        protected override void Configure(IObjectTypeDescriptor<Jogo> descriptor)
        {
            descriptor.Description("Modelo referente ao jogo do usuário");
            descriptor.Field("estudio")
                .ResolveWith<Resolvers>(e =>
                e.GetEstudio(default!));

            descriptor.Field("generos")
                .ResolveWith<Resolvers>(g =>
                g.GetGenero(default!));

            descriptor.Field(j => j.IdEstudio).Ignore();
        }

        private class Resolvers
        {
            private readonly IEstudioRepository _estudioRepository;
            private readonly IJogoGeneroRepository _jogoGeneroRepository;
            private readonly DbContext _context;
            public Resolvers(IEstudioRepository _estudioRepository, IJogoGeneroRepository _jogoGeneroRepository, DbContext _context)
            {
                this._estudioRepository = _estudioRepository;
                this._jogoGeneroRepository = _jogoGeneroRepository;
                this._context = _context;
            }

            public Estudio? GetEstudio([Parent] Jogo jogo)
            {
                if (jogo.IdEstudio.HasValue)
                {
                    return _estudioRepository.SelectEstudioPorID(_context, (int)jogo.IdEstudio);
                }
                return null;
            }

            public IEnumerable<Genero> GetGenero([Parent] Jogo jogo)
            {
                return _jogoGeneroRepository.SelectGenerosPorJogo(_context, jogo);
            }
        }
    }
}
