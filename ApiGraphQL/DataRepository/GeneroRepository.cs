using DataRepository.Interface;
using Dapper;
using Models;
using Npgsql;
using DataRepository.Inputs;

namespace DataRepository
{
    public class GeneroRepository : IGeneroRepository
    {
        public bool DeleteGenero(DbContext _context, DeleteGeneroInput genero)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"DELETE FROM genero
                               WHERE id = @ID
";
                int result = conn.Execute(sql, new { ID = genero.id });

                return result > 0;
            }

        }

        public int InsertGenero(DbContext _context, AddGeneroInput genero)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"INSERT INTO genero(
	                            nome)
	                            VALUES (@nome)
                                RETURNING Id
";
                int result = (int) conn.ExecuteScalar(sql, genero);
                return result;
            }

        }
        public Genero? SelectGeneroPorID(DbContext _context, int id)
        {
            using (var conn = new Npgsql.NpgsqlConnection(_context.ConnString))
            {
                var sql = @"SELECT * FROM genero
                            WHERE genero.id = @Id
";

                
                var genero = conn.QueryFirstOrDefault<Genero?>(sql, new { ID = id });
                return genero;
            }
        }
        public IEnumerable<Genero> SelectTodosGeneros(DbContext _context)
        {
            using (var conn = new Npgsql.NpgsqlConnection(_context.ConnString))
            {
                var sql = @"SELECT * FROM GENERO";

                IEnumerable<Genero> generos = new List<Genero>();
                generos = conn.Query<Genero>(sql);
                return generos;
            }
        }
        public bool UpdateGenero(DbContext _context, UpdateGeneroInput genero)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {


                string sql = @"UPDATE genero 
                                SET nome = @nome
                                WHERE id = @id
";

                int result = conn.Execute(sql, genero);

                return result > 0;
            }
        }
    }
}
