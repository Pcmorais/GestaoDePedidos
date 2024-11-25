using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infra.Repositorios
{
    public class PessoaRepositorio : RepositorioBase<Pessoa>, IPessoaRepositorio
    {
        public PessoaRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<Pessoa?> GetByCPFAsync(string cpf) =>
           await _context.Pessoas.FirstOrDefaultAsync(p => p.CPF == cpf);

        public async Task<Pessoa?> GetByEmailAsync(string email) =>
            await _context.Pessoas.FirstOrDefaultAsync(p => p.Email == email);

        public async Task<Pessoa?> GetByClientIdAsync(int clienteId)
        {
            return await _context.Pessoas
                         .Include(p => p.Enderecos) // Inclui os endereços associados
                         .FirstOrDefaultAsync(p => p.Id == clienteId);
        }
    }
}
