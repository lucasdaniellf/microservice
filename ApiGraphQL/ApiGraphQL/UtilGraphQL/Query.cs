using DataRepository.Interface;
using Models;
using DataRepository;

namespace ApiGraphQL.UtilGraphQL
{
    public class Query
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IEstudioRepository _estudioRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly DbContext _context;
        
        public Query(IJogoRepository _jogoRepository, IEstudioRepository _estudioRepository, IGeneroRepository _generoRepository, DbContext context)
        {
            this._jogoRepository = _jogoRepository;
            this._generoRepository = _generoRepository;
            this._estudioRepository = _estudioRepository;
            this._context = context;

        }

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Jogo> GetJogos()
        {
            return _jogoRepository.SelectTodosJogos(_context);
        }

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Genero> GetGeneros()
        {
            return _generoRepository.SelectTodosGeneros(_context);
        }

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Estudio> GetEstudios()
        {
            return _estudioRepository.SelectTodosEstudios(_context);
        }
    }
}
