using MediatR;
using MeuCorre.Application.UseCases.Subcategorias.Commands;
using MeuCorre.Application.UseCases.Subcategorias.Dtos;
using MeuCorre.Application.UseCases.Subcategorias.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SubcategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubcategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Cria uma nova subcategoria para o usuário
        /// </summary>
        /// <param name="command">Os dados da nova subcategoria</param>
        /// <returns>Retorna uma nova subcategoria criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SubcategoriaDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CriarSubcategoria([FromBody] CriarSubcategoriaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return Conflict(mensagem);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarSubcategoria([FromBody] AtualizarSubcategoriaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarSubcategoria([FromBody] DeletarSubcategoriaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpPatch("ativar/{id}")]
        public async Task<IActionResult> AtivarSubcategoria(Guid id)
        {
            var command = new AtivarSubcategoriaCommand { SubcategoriaId = id };
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(mensagem);
            }
        }


        [HttpPatch("inativar/{id}")]
        public async Task<IActionResult> InativarSubcategoria(Guid id)
        {
            var command = new InativarSubcategoriaCommand { SubcategoriaId = id };
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(mensagem);
            }
        }


        [HttpGet]
        public async Task<IActionResult> ObterSubcategoriasPorUsuario([FromQuery] ListarTodasSubcategoriasQuery query)
        {
            var subcategorias = await _mediator.Send(query);
            return Ok(subcategorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterSubcategoriaPorId(Guid id)
        {
            var query = new ObterSubcategoriaQuery() { SubcategoriaId = id };
            var subcategoria = await _mediator.Send(query);
            if (subcategoria == null)
            {
                return NotFound("Subcategoria não encontrada");
            }
            return Ok(subcategoria);
        }
    }

}
