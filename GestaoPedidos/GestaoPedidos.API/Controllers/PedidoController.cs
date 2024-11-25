using GestaoPedidos.Application.DTO.Request;
using GestaoPedidos.Application.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ServicoPedido _servicoPedido;

        public PedidoController(ServicoPedido servicoPedido)
        {
            _servicoPedido = servicoPedido;
        }

        /// <summary>
        /// Cria um novo pedido com os dados fornecidos.
        /// </summary>
        /// <param name="request">Objeto contendo as informações necessárias para criar o pedido.</param>
        /// <returns>Retorna o ID do pedido criado se a operação for bem-sucedida, ou uma mensagem de erro caso contrário.</returns>
        /// <response code="200">Pedido criado com sucesso. Retorna o ID do pedido criado.</response>
        /// <response code="400">Se os dados fornecidos estiverem inválidos, retorna um erro de requisição (Bad Request).</response>
        /// <response code="500">Erro inesperado no servidor. Retorna uma mensagem de erro genérica.</response>
        [HttpPost("Criar")]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequestDTO request)
        {
            try
            {
                var resultado = await _servicoPedido.GerarPedido(request.ClienteId, request.EnderecoEntregaId, request.EnderecoCobrancaId, request.Itens);

                if (resultado.Success)
                {
                    return Ok(resultado.Value);
                }

                return BadRequest(resultado.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Ocorreu um erro inesperado. Por favor, tente novamente." });
            }
        }


        /// <summary>
        /// Obtém um pedido específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do pedido a ser recuperado.</param>
        /// <returns>Retorna o pedido encontrado ou um erro 404 se o pedido não for encontrado.</returns>
        /// <response code="200">Pedido encontrado e retornado com sucesso.</response>
        /// <response code="404">Pedido não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedido(int id)
        {
            var pedido = await _servicoPedido.ObtenhaPedidoDetalhado(id);
            if (pedido == null) return NotFound(new { Mensagem = "Pedido não encontrado." });

            return Ok(pedido);
        }

        /// <summary>
        /// Obtém uma lista de pedidos com base nos filtros fornecidos, incluindo nome da pessoa, ID do pedido e intervalo de datas.
        /// </summary>
        /// <param name="nomePessoa">Nome da pessoa associado ao pedido (opcional). Pode ser usado para filtrar os pedidos por nome.</param>
        /// <param name="pedidoId">ID do pedido (opcional). Filtra pedidos específicos com base no ID.</param>
        /// <param name="dataPedidoInicio">Data de início do intervalo de busca (opcional). Filtra pedidos realizados a partir dessa data.</param>
        /// <param name="dataPedidoFim">Data de término do intervalo de busca (opcional). Filtra pedidos realizados até essa data.</param>
        /// <returns>Retorna uma lista de pedidos filtrados conforme os critérios fornecidos.</returns>
        /// <response code="200">Pedidos encontrados com sucesso, conforme os filtros aplicados.</response>
        /// <response code="500">Erro interno ao tentar buscar os pedidos. Detalhes adicionais são fornecidos.</response>
        [HttpGet("Pedidos")]
        public async Task<IActionResult> ObterPedidos(
            [FromQuery] string? nomePessoa,
            [FromQuery] int? pedidoId,
            [FromQuery] DateTime? dataPedidoInicio,
            [FromQuery] DateTime? dataPedidoFim)
        {
            try
            {
                var pedidos = await _servicoPedido.ObtenhaPedidos(nomePessoa, pedidoId, dataPedidoInicio, dataPedidoFim);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro interno ao tentar obter os pedidos.", Detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Gera um relatório de pedidos em formato Excel com base nos filtros fornecidos, incluindo nome da pessoa, ID do pedido e intervalo de datas.
        /// </summary>
        /// <param name="nomePessoa">Nome da pessoa associado ao pedido (opcional). Filtra pedidos realizados por uma pessoa específica.</param>
        /// <param name="pedidoId">ID do pedido (opcional). Filtra um pedido específico baseado no ID.</param>
        /// <param name="dataPedidoInicio">Data de início do intervalo de busca (opcional). Filtra pedidos realizados a partir dessa data.</param>
        /// <param name="dataPedidoFim">Data de término do intervalo de busca (opcional). Filtra pedidos realizados até essa data.</param>
        /// <returns>Retorna um arquivo Excel contendo os pedidos filtrados conforme os critérios fornecidos.</returns>
        /// <response code="200">Relatório gerado e retornado com sucesso. O arquivo Excel contém os pedidos filtrados.</response>
        /// <response code="500">Erro interno ao gerar o relatório de pedidos. Detalhes adicionais são fornecidos.</response>
        [HttpGet("relatorio/excel")]
        public async Task<IActionResult> GerarRelatorioExcel(
            [FromQuery] string? nomePessoa,
            [FromQuery] int? pedidoId,
            [FromQuery] DateTime? dataPedidoInicio,
            [FromQuery] DateTime? dataPedidoFim)
        {
            try
            {
                var excelFile = await _servicoPedido.GerarRelatorioPedidosExcel(nomePessoa, pedidoId, dataPedidoInicio, dataPedidoFim);
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Relatorio_Pedidos.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = "Erro interno ao gerar o relatório de pedidos.", Detalhes = ex.Message });
            }
        }

    }
}
