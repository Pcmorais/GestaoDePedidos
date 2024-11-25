using GestaoPedidos.Application.DTO.Request;
using GestaoPedidos.Application.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ServicoProduto _servicoProduto;

        public ProdutoController(ServicoProduto servicoProduto)
        {
            _servicoProduto = servicoProduto;
        }


        /// <summary>
        /// Cria um novo produto no sistema.
        /// </summary>
        /// <param name="request">Os dados necessários para criação de um produto.</param>
        /// <returns>Retorna uma mensagem de sucesso  caso bem sucessido, ou um erro caso contrário.</returns>
        /// <response code="200">Produto criado com sucesso.</response>
        /// <response code="400">Se houver erro nos dados de entrada.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] CriarProdutoRequestDTO request)
        {
            try
            {
                var result = await _servicoProduto.CriarProduto(request);

                if (result.Success)
                {
                    return Ok(result.Value);
                }

                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
        }

        /// <summary>
        /// Obtém a lista completa de produtos cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna a lista de produtos disponíveis.</returns>
        /// <response code="200">Lista de produtos retornada com sucesso.</response>
        /// <response code="204">Nenhum produto encontrado.</response>
        /// <response code="500">Erro interno ao tentar buscar os produtos.</response>
        [HttpGet]
        public async Task<IActionResult> ObtenhaProdutos()
        {
            var produtos = await _servicoProduto.ObtenhaProdutosAsync();
            if (!produtos.Any())
            {
                return NoContent();
            }

            return Ok(produtos);
        }

    }

}
