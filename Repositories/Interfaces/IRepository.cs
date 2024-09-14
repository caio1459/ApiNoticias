using ApiNoticias.Models;

namespace ApiNoticias.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public ApiResponse<IEnumerable<T>> SelectAll();
        public ApiResponse<T?> SelectOne(int id);
        public ApiResponse<int> Insert(T entity);
        public ApiResponse<int> Update(T entity);
        public ApiResponse<int> Delete(int id);
    }
}
