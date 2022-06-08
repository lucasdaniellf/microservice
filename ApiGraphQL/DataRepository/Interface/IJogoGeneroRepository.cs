using DataRepository.Inputs;
using Models;

namespace DataRepository.Interface
{
    public interface IJogoGeneroRepository
    {
        public IEnumerable<Jogo> SelectJogosPorGenero(DbContext _context, Genero genero);
        public IEnumerable<Genero> SelectGenerosPorJogo(DbContext _context, Jogo jogo);
        public Task<bool> UpdateAddGeneroToJogo(DbContext _context, UpdateJogoGeneroInput jogo);
        public Task<bool> UpdateRemoveGeneroFromJogo(DbContext _context, UpdateJogoGeneroInput jogo);
    }
}
