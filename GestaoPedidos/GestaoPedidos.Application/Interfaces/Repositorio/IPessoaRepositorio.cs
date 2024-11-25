using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Interfaces.Repositorio
{
    public interface IPessoaRepositorio : IRepositorio<Pessoa>
    {
        Task<Pessoa?> GetByCPFAsync(string cpf);
        Task<Pessoa?> GetByEmailAsync(string email);
        Task<Pessoa?> GetByClientIdAsync(int clientId);
    }
}
