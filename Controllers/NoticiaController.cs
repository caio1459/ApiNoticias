using ApiNoticias.Controllers.Interfaces;
using ApiNoticias.Models;
using ApiNoticias.Repositories;
using ApiNoticias.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase, IController<Noticia>
    {
        private readonly IRepository<Noticia> _repository;

        public NoticiaController(IRepository<Noticia> repository)
        {
            _repository = repository;
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
        public IActionResult Post(Noticia noticia)
        {
            var response = _repository.Insert(noticia);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }

        [HttpPut]
        public IActionResult Put(Noticia noticia)
        {
            var response = _repository.Update(noticia);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _repository.Delete(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }
    }
}