using Dapper;
using DataRepository.Inputs;
using DataRepository.Interface;
using Models;
using Npgsql;

namespace DataRepository
{
    public class EstudioRepository : IEstudioRepository
    {
        public bool DeleteEstudio(DbContext _context, DeleteEstudioInput estudio)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"DELETE FROM estudio
                               WHERE id = @ID
";
                int result = conn.Execute(sql, new { ID = estudio.id });

                return result > 0;
            }
        }

        public int InsertEstudio(DbContext _context, AddEstudioInput estudio)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"INSERT INTO estudio(
	                            nome)
	                            VALUES (@nome)
                                RETURNING Id
";
                int result = (int) conn.ExecuteScalar(sql, estudio);
                return result;
            }
        }


        public Estudio? SelectEstudioPorID(DbContext _context, int id)
        {
            using (var conn = new Npgsql.NpgsqlConnection(_context.ConnString))
            {
                var sql = @"SELECT * FROM ESTUDIO 
                            WHERE ID = @ID
";

                Estudio? estudio = conn.QueryFirstOrDefault<Estudio?>(sql, new { ID = id });
                return estudio;
            }
        }

        public IEnumerable<Estudio> SelectTodosEstudios(DbContext _context)
        {
            using (var conn = new Npgsql.NpgsqlConnection(_context.ConnString))
            {
                var sql = @"SELECT * FROM ESTUDIO";

                IEnumerable<Estudio> estudios = new List<Estudio>();
                estudios =  conn.Query<Estudio>(sql);
                return estudios;
            }
        }

        public bool UpdateEstudio(DbContext _context, UpdateEstudioInput estudio)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {


                string sql = @"UPDATE estudio 
                                SET estudio.nome = @nome
                                WHERE id = @id
";

                int result = conn.Execute(sql, estudio);

                return result > 0;
            }
        }
    }
}
