using ApiNoticias.Models;
using ApiNoticias.Repositories.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace ApiNoticias.Repositories
{
    public class NoticiaRepository : IRepository<Noticia>
    {
        private readonly SqlConnection _connection;

        public NoticiaRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("connection");
            _connection = new SqlConnection(connectionString);
        }

        public ApiResponse<int> Delete(int id)
        {
            var sql = "DELETE FROM TbNoticia WHERE NotId = @id";
            var parameters = new { id };
            try
            {
                var result = _connection.Execute(sql, parameters);
                return new ApiResponse<int>(result > 0, result > 0 ? "Notícia deletada com sucesso." : "Nenhuma notícia encontrada.", result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, $"Erro ao deletar notícia: {ex.Message}");
            }
        }

        public ApiResponse<int> Insert(Noticia noticia)
        {
            var sql = @"
                INSERT INTO TbNoticia (Titulo, Texto, Data, AutorId)
                VALUES (@Titulo, @Texto, @Data, @AutorId)";
            var parameters = new
            {
                noticia.Titulo,
                noticia.Texto,
                noticia.Data,
                noticia.AutorId
            };
            try
            {
                var result = _connection.Execute(sql, parameters);
                return new ApiResponse<int>(result > 0, result > 0 ? "Notícia inserida com sucesso." : "Erro ao inserir notícia.", result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, $"Erro ao inserir notícia: {ex.Message}");
            }
        }

        public ApiResponse<IEnumerable<Noticia>> SelectAll()
        {
            var sql = @"
                SELECT n.NotId, n.Titulo, n.Texto, n.Data, n.AutorId, a.AutorId, a.Nome, a.Email
                FROM TbNoticia n
                INNER JOIN TbAutor a ON n.AutorId = a.AutorId";
            try
            {
                var noticias = _connection.Query<Noticia, Autor, Noticia>(
                    sql,
                    (noticia, autor) =>
                    {
                        noticia.Autor = autor;
                        return noticia;
                    },
                    splitOn: "AutorId"
                );
                return new ApiResponse<IEnumerable<Noticia>>(true, "Notícias recuperadas com sucesso.", noticias);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<Noticia>>(false, $"Erro ao recuperar notícias: {ex.Message}");
            }
        }

        public ApiResponse<Noticia?> SelectOne(int id)
        {
            var sql = @"
                SELECT n.NotId, n.Titulo, n.Texto, n.Data, n.AutorId, a.AutorId, a.Nome, a.Email
                FROM TbNoticia n
                INNER JOIN TbAutor a ON n.AutorId = a.AutorId
                WHERE n.NotId = @id";
            var parameters = new { id };
            try
            {
                var noticia = _connection.Query<Noticia, Autor, Noticia>(
                    sql,
                    (noticia, autor) =>
                    {
                        noticia.Autor = autor;
                        return noticia;
                    },
                    parameters,
                    splitOn: "AutorId"
                ).FirstOrDefault();

                if (noticia != null)
                {
                    return new ApiResponse<Noticia?>(true, "Notícia encontrada.", noticia);
                }
                return new ApiResponse<Noticia?>(false, "Notícia não encontrada.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<Noticia?>(false, $"Erro ao buscar notícia: {ex.Message}");
            }
        }

        public ApiResponse<int> Update(Noticia noticia)
        {
            var sql = @"
                UPDATE TbNoticia 
                SET Titulo = @Titulo, Texto = @Texto, Data = @Data, AutorId = @AutorId
                WHERE NotId = @NotId";
            var parameters = new
            {
                noticia.Titulo,
                noticia.Texto,
                noticia.Data,
                noticia.AutorId,
                noticia.NotId
            };
            try
            {
                var result = _connection.Execute(sql, parameters);
                return new ApiResponse<int>(result > 0, result > 0 ? "Notícia atualizada com sucesso." : "Erro ao atualizar notícia.", result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, $"Erro ao atualizar notícia: {ex.Message}");
            }
        }
    }
}
