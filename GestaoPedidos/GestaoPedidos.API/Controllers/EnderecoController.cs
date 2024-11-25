using GestaoPedidos.Application.Servicos;
using GestaoPedidos.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : Controller
    {
        private readonly EnderecosPorPessoUseCase _enderecosPorPessoUseCase;

        public EnderecoController(EnderecosPorPessoUseCase enderecosPorPessoUseCase)
        {
            _enderecosPorPessoUseCase = enderecosPorPessoUseCase;
        }

        /// <summary>
        /// Obtém a lista de endereços associados a uma pessoa específica.
        /// </summary>
        /// <param name="idPessoa">ID da pessoa para buscar os endereços.</param>
        /// <returns>Retorna a lista de endereços da pessoa especificada.</returns>
        /// <response code="200">Lista de endereços obtida com sucesso.</response>
        /// <response code="204">Nenhum endereço encontrado para a pessoa especificada.</response>
        /// <response code="400">ID da pessoa inválido.</response>
        /// <response code="500">Erro interno ao buscar os endereços.</response>
        [HttpGet("{idPessoa}")]
        public async Task<IActionResult> ObterEnderecosPorPessoa(int idPessoa)
        {
            if (idPessoa <= 0)
            {
                return BadRequest(new { Erro = "ID da pessoa inválido." });
            }

            try
            {
                var listaEnderecos = await _enderecosPorPessoUseCase.Execute(idPessoa);
                if (!listaEnderecos.Any())
                {
                    return NoContent();
                }
                return Ok(listaEnderecos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro interno ao buscar os endereços.", Detalhes = ex.Message });
            }
        }

    }
}
