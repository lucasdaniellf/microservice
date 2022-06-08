using DataRepository.Interface;
using Models;
using Dapper;
using Npgsql;
using DataRepository.Inputs;

namespace DataRepository
{
    public class JogoRepository : IJogoRepository
    {

        public bool DeleteJogo(DbContext _context, DeleteJogoInput jogo)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"DELETE FROM jogo
                               WHERE id = @ID
";
                int result = conn.Execute(sql, new { ID = jogo.Id });

                return result > 0;
            }

        }

        public int InsertJogo(DbContext _context, AddJogoInput jogo)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"INSERT INTO jogo(
	                            nome, 
                                descricao,
                                classificacaoesbr,  
                                idestudio)
	                            VALUES (
                                @Nome, 
                                @Descricao, 
                                @ClassificacaoESBR,
                                @IdEstudio)
                                RETURNING Id
";
                int result = (int) conn.ExecuteScalar(sql, jogo);

                return result;
            }
        }

        public Jogo? SelectJogoPorID(DbContext _context, int id)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"SELECT * FROM Jogo
                               WHERE jogo.Id = @ID                                
";
                var jogo = conn.QueryFirstOrDefault<Jogo?>(sql, new { ID = id } );

                return jogo ;
            }
        }

        public IEnumerable<Jogo> SelectTodosJogos(DbContext _context)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"SELECT * FROM Jogo";
                IEnumerable<Jogo> jogos = new List<Jogo>();
                
                jogos = conn.Query<Jogo>(sql);
                
                return jogos;
            }
        }


        public bool UpdateJogo(DbContext _context, UpdateJogoInput jogo)
        {
            DynamicParameters parameters = new DynamicParameters();

            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"UPDATE jogo SET ";
                int x = 0;
                if(jogo.Nome != null)
                {
                    x += 1;
                    sql +=  "Nome = @Nome ";
                    parameters.Add("Nome", jogo.Nome);
                }
                if (jogo.Descricao != null)
                {
                    x += 1;
                    sql += "Descricao = @Descricao ";
                    parameters.Add("Descricao", jogo.Descricao);
                }
                if (jogo.ClassificacaoESBR != null)
                {
                    x += 1;
                    sql += "ClassificacaoESBR = @ClassificacaoESBR ";
                    parameters.Add("ClassicificacaoESBR", jogo.ClassificacaoESBR);
                }

                if(x > 0)
                {
                    sql += "Where Id = @Id";
                    parameters.Add("Id", jogo.Id);

                    int result = conn.Execute(sql, parameters);
                    return result > 0;

                }

                return x > 0;
            }
        }

        public bool UpdateEstudioJogo(DbContext _context, UpdateJogoIdEstudioInput jogo)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {

                string sql = @"UPDATE jogo SET IdEstudio = @IdEstudio";

                int result = conn.Execute(sql, new { IdEstudio = jogo.IdEstudio });

                return result > 0;
            }
        }


        public IEnumerable<Jogo> SelectJogosPorEstudio(DbContext _context, Estudio estudio)
        {
            using (var conn = new NpgsqlConnection(_context.ConnString))
            {
                string sql = @"SELECT * FROM Jogo
                               WHERE Jogo.IdEstudio = @IdEstudio
";
                IEnumerable<Jogo> jogos = new List<Jogo>();

                jogos = conn.Query<Jogo>(sql, new { IdEstudio = estudio.Id });

                return jogos;
            }
        }
    }
}
