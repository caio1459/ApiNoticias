using ApiNoticias.Models;
using ApiNoticias.Repositories.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace ApiNoticias.Repositories
{
    public class AutorRepository : IRepository<Autor>
    {
        private readonly SqlConnection _connection;

        public AutorRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("connection");
            _connection = new SqlConnection(connectionString);
        }

        public ApiResponse<int> Delete(int id)
        {
            var sql = "DELETE FROM TbAutor WHERE AutorId = @id";
            var parameters = new { id };
            try
            {
                var result = _connection.Execute(sql, parameters);
                return new ApiResponse<int>(result > 0, result > 0 ? "Autor deletado com sucesso." : "Nenhum autor encontrado.", result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, $"Erro ao deletar o autor: {ex.Message}");
            }
        }

        public ApiResponse<int> Insert(Autor autor)
        {
            var sql = "INSERT INTO TbAutor (Nome, Email) VALUES (@Nome, @Email)";
            var parameters = new { autor.Nome, autor.Email };
            try
            {
                var result = _connection.Execute(sql, parameters);
                return new ApiResponse<int>(result > 0, result > 0 ? "Autor inserido com sucesso." : "Erro ao inserir autor.", result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, $"Erro ao inserir autor: {ex.Message}");
            }
        }

        public ApiResponse<IEnumerable<Autor>> SelectAll()
        {
            try
            {
                var autores = _connection.Query<Autor>("SELECT * FROM TbAutor");
                return new ApiResponse<IEnumerable<Autor>>(true, "Autores recuperados com sucesso.", autores);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<Autor>>(false, $"Erro ao recuperar autores: {ex.Message}");
            }
        }

        public ApiResponse<Autor?> SelectOne(int id)
        {
            var sql = "SELECT * FROM TbAutor WHERE AutorId = @id";
            var parameters = new { id };
            try
            {
                var autor = _connection.QueryFirstOrDefault<Autor>(sql, parameters);
                if (autor != null)
                {
                    return new ApiResponse<Autor?>(true, "Autor encontrado.", autor);
                }
                return new ApiResponse<Autor?>(false, "Autor não encontrado.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<Autor?>(false, $"Erro ao buscar autor: {ex.Message}");
            }
        }

        public ApiResponse<int> Update(Autor autor)
        {
            var sql = "UPDATE TbAutor SET Nome = @Nome, Email = @Email WHERE AutorId = @AutorID";
            var parameters = new { autor.Nome, autor.Email, autor.AutorID };
            try
            {
                var result = _connection.Execute(sql, parameters);
                return new ApiResponse<int>(result > 0, result > 0 ? "Autor atualizado com sucesso." : "Erro ao atualizar autor.", result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, $"Erro ao atualizar autor: {ex.Message}");
            }
        }
    }
}
