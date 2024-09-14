using ApiNoticias.Controllers.Interfaces;
using ApiNoticias.Models;
using ApiNoticias.Repositories;
using ApiNoticias.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase, IController<Autor>
    {
        private readonly IRepository<Autor> _repository;

        public AutorController(IRepository<Autor> repository)
        {
            _repository = repository;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _repository.Delete(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _repository.SelectAll();
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var response = _repository.SelectOne(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }

        [HttpPost]
        public IActionResult Post(Autor autor)
        {
            var response = _repository.Insert(autor);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }

        [HttpPut]
        public IActionResult Put(Autor autor)
        {
            var response = _repository.Update(autor);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }
    }
}
