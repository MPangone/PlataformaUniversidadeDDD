using DDD.Domain;
using DDD.Infra.MemoryDb.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        // GET: api/<AlunosController>
        [HttpGet]
        public ActionResult<List<Aluno>> Get()
        {
            return Ok(_alunoRepository.GetAlunos());
        }

        [HttpGet("{id}")]
        public ActionResult<Aluno> GetById(int id)
        {
            return Ok(_alunoRepository.GetAlunoById(id));
        }

        [HttpPost]
        public ActionResult<Aluno> CreateAluno(Aluno aluno)
        {
            if (aluno.Nome.Length < 3 || aluno.Nome.Length > 30)
            {
                return BadRequest("Nome nao pode ser menor que 3 ou maior que 30 caracteres");
            }

            _alunoRepository.InsertAluno(aluno);
            return CreatedAtAction(nameof(GetById), new { Id = aluno.Id }, aluno);
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Aluno aluno)
        {
            try
            {
                if (aluno == null)
                {
                    return BadRequest();
                }
                _alunoRepository.DeleteAluno(aluno);
                return Ok("Aluno deletado com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
