using GestaoPedidos.Application.Abstracoes;
using GestaoPedidos.Application.DTO.Request;
using GestaoPedidos.Application.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ServicoPessoa _servicoPessoa;

        public PessoaController(ServicoPessoa servicoPessoa)
        {
            _servicoPessoa = servicoPessoa;
        }

        /// <summary>
        /// Cria uma nova pessoa no sistema.
        /// </summary>
        /// <param name="dados">Os dados necessários para criação de uma pessoa.</param>
        /// <returns>Retorna uma mensagem de sucesso com os dados da pessoa criada.</returns>
        /// <response code="200">Pessoa criada com sucesso.</response>
        /// <response code="400">Se houver erro nos dados de entrada.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [HttpPost("")]
        public async Task<IActionResult> CriarPessoa([FromBody] CriarPessoaRequestDTO dados)
        {
            try
            {
                var resultado = await _servicoPessoa.CriarPessoa(dados);
                if (resultado.Success)
                {
                    return Ok(resultado.Value);
                }

                return BadRequest(new { Erro = resultado.Error });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Ocorreu um erro inesperado. Por favor, tente novamente." });
            }
        }


        /// <summary>
        /// Obtém os dados de uma pessoa pelo ID.
        /// </summary>
        /// <param name="id">O ID da pessoa a ser recuperada.</param>
        /// <returns>Retorna os dados da pessoa se encontrada, ou um erro 404 se não encontrada.</returns>
        /// <response code="200">Pessoa encontrada e retornada com sucesso.</response>
        /// <response code="404">Pessoa não encontrada.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPessoa(int id)
        {
            var pessoa = await _servicoPessoa.ObtenhaPeloId(id);
            if (pessoa == null)
            {
                return NotFound(new { Mensagem = "Pessoa não encontrada." });
            }

            return Ok(pessoa);
        }

        /// <summary>
        /// Obtém uma lista de todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Retorna uma lista de pessoas, ou um código 204 caso não haja pessoas cadastradas.</returns>
        /// <response code="200">Lista de pessoas retornada com sucesso.</response>
        /// <response code="204">Nenhuma pessoa encontrada.</response>
        [HttpGet]
        public async Task<IActionResult> ObterPessoas()
        {
            var listaPessoas = await _servicoPessoa.ObtenhaTodos();
            if (!listaPessoas.Any())
            {
                return NoContent(); 
            }

            return Ok(listaPessoas);
        }

    }

}
