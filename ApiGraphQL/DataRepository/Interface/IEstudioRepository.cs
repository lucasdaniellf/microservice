using DataRepository.Inputs;
using Models;

namespace DataRepository.Interface
{
    public interface IEstudioRepository
    {
        public IEnumerable<Estudio> SelectTodosEstudios(DbContext _context);
        public Estudio? SelectEstudioPorID(DbContext _context, int id);
        public bool DeleteEstudio(DbContext _context, DeleteEstudioInput id);
        public int InsertEstudio(DbContext _context, AddEstudioInput estudio);
        public bool UpdateEstudio(DbContext _context, UpdateEstudioInput estudio);
    }
}
