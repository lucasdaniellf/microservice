
using DataRepository.Inputs;
using Models;

namespace DataRepository.Interface
{
    public interface IGeneroRepository
    {
        public IEnumerable<Genero> SelectTodosGeneros(DbContext _context);
        public Genero? SelectGeneroPorID(DbContext _context, int id);
        public bool DeleteGenero(DbContext _context, DeleteGeneroInput genero);
        public int InsertGenero(DbContext _context, AddGeneroInput genero);
        public bool UpdateGenero(DbContext _context, UpdateGeneroInput genero);
    }
}
