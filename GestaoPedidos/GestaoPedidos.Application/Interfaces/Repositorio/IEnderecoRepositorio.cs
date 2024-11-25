using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Interfaces.Repositorio
{
    public interface IEnderecoRepositorio
    {
        Task<List<Endereco>> GetEnderecoPorIdPessoaAsync(int idPessoa);
    }
}