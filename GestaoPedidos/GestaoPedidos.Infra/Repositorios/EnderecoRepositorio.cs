using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infra.Repositorios;

public class EnderecoRepositorio : RepositorioBase<Endereco>, IEnderecoRepositorio
{
    public EnderecoRepositorio(AppDbContext context) : base(context)
    {
    }


    public async Task<List<Endereco>> GetEnderecoPorIdPessoaAsync(int idPessoa)
    {
        return await _context.Enderecos
                     .Where(e => e.PessoaId == idPessoa)
                     .ToListAsync();
    }
}
