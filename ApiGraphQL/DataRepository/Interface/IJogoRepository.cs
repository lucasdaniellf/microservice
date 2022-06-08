using DataRepository.Inputs;
using Models;
namespace DataRepository.Interface
{
    public interface IJogoRepository
    {
        public IEnumerable<Jogo> SelectTodosJogos(DbContext _context);
        public Jogo? SelectJogoPorID(DbContext _context, int id);
        public bool DeleteJogo(DbContext _context, DeleteJogoInput jogo);
        public int InsertJogo(DbContext _context, AddJogoInput jogo);
        public bool UpdateJogo(DbContext _context, UpdateJogoInput jogo);
        public bool UpdateEstudioJogo(DbContext _context, UpdateJogoIdEstudioInput jogo);
        public IEnumerable<Jogo> SelectJogosPorEstudio(DbContext _context, Estudio estudio);
    }
}
