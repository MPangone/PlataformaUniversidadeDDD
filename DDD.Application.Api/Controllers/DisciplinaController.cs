using DDD.Domain;
using DDD.Infra.MemoryDb.Interfaces;
using DDD.Infra.MemoryDb.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {

        readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaController(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        // GET: api/<AlunosController>
        [HttpGet]
        public ActionResult<List<Disciplina>> Get()
        {
            return Ok(_disciplinaRepository.GetDisciplinas());
        }

        [HttpGet("{id}")]
        public ActionResult<Disciplina> GetById(int id)
        {
            return Ok(_disciplinaRepository.GetDisciplinaById(id));
        }

        [HttpPost]
        public ActionResult<Disciplina> CreateDisciplina(Disciplina disciplina)
        {
            if (disciplina.Nome.Length < 3 || disciplina.Nome.Length > 30)
            {
                return BadRequest("Nome não pode ser menor que 3 ou maior que 30 caracteres");
            }

            _disciplinaRepository.InsertDisciplina(disciplina);
            return CreatedAtAction(nameof(GetById), new { Id = disciplina});
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Disciplina disciplina)
        {
            try
            {
                if (disciplina == null)
                {
                    return BadRequest();
                }
                _disciplinaRepository.DeleteDisciplina(disciplina);
                return Ok("Disciplina deletada com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
