using Microsoft.AspNetCore.Mvc;

namespace ApiNoticias.Controllers.Interfaces
{
    public interface IController<T> where T : class
    {
        public IActionResult GetAll();
        public IActionResult GetOne(int id);
        public IActionResult Post(T entity);
        public IActionResult Put(T entity);
        public IActionResult Delete(int id);
    }
}
