using Dapper;
using DataRepository.Inputs;
using DataRepository.Interface;
using Models;
using Npgsql;

namespace DataRepository
{
    public class JogoGeneroRepository : IJogoGeneroRepository
    {
        public IEnumerable<Genero> SelectGenerosPorJogo(DbContext _context, Jogo jogo)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"SELECT genero.* FROM genero
                               INNER JOIN jogogenero ON jogogenero.IdGenero = genero.Id
                               INNER JOIN jogo ON jogo.Id = jogogenero.IdJogo
                               WHERE jogo.id = @ID
";
                IEnumerable<Genero> generos = new List<Genero>();

                generos = conn.Query<Genero>(sql, new { ID = jogo.Id });

                return generos;
            }

        }

        public IEnumerable<Jogo> SelectJogosPorGenero(DbContext _context, Genero genero)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"SELECT jogo.* FROM jogo
                               INNER JOIN jogogenero ON jogo.Id = jogogenero.IdJogo
                               INNER JOIN genero ON jogogenero.IdGenero = genero.Id
                               WHERE genero.id = @ID
";
                IEnumerable<Jogo> jogos = new List<Jogo>();

                jogos = conn.Query<Jogo>(sql, new { ID = genero.Id });

                return jogos;
            }
        }

        public async Task<bool> UpdateAddGeneroToJogo(DbContext _context, UpdateJogoGeneroInput jogo )
        {
            DynamicParameters parameters = new DynamicParameters();

            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"INSERT INTO jogogenero(idJogo, idGenero)
                                VALUES (@idJogo, @idGenero)
";
                List<Task<int>> tasks = new List<Task<int>>();

                foreach (var item in jogo.GeneroIds)
                {
                    tasks.Add(conn.ExecuteAsync(sql, new { idJogo = jogo.JogoId, idGenero = item }));
                }
                int[] count = await Task.WhenAll(tasks);

                return count.Sum() > 0;
            }
        }

        public async Task<bool> UpdateRemoveGeneroFromJogo(DbContext _context, UpdateJogoGeneroInput jogo)
        {
            DynamicParameters parameters = new DynamicParameters();

            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"DELETE FROM jogogenero
                               WHERE idJogo = @idJogo and idGenero = @idGenero
";
                List<Task<int>> tasks = new List<Task<int>>();

                foreach (var item in jogo.GeneroIds)
                {
                    tasks.Add(conn.ExecuteAsync(sql, new { idJogo = jogo.JogoId, idGenero = item }));
                }
                int[] count = await Task.WhenAll(tasks);

                return count.Sum() > 0;
            }
        }
    }
}
