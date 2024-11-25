using GestaoPedidos.Application.Abstracoes;
using GestaoPedidos.Application.DTO.Request;
using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;

namespace GestaoPedidos.Application.Servicos
{
    public class ServicoPessoa
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public ServicoPessoa(IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }

        public async Task<Result> CriarPessoa(CriarPessoaRequestDTO dados)
        {

            var pessoa = new Pessoa()
            {
                NomeCompleto = dados.NomeCompleto,
                CPF = dados.CPF,
                Email = dados.Email,
                Telefone = dados.Telefone
            };

            var enderecos = dados.Enderecos.Select(x => new Endereco
            {
                Bairro = x.Bairro,
                Cidade = x.Cidade,
                CEP = x.CEP,
                Complemento = x.Complemento,
                Estado = x.Estado,
                Numero = x.Numero,
                Logradouro = x.Logradouro,
                TipoEndereco = "Ambos"
            });



            if (await _pessoaRepositorio.GetByCPFAsync(pessoa.CPF) != null)
                return Result.Fail("CPF já cadastrado.");

            if (await _pessoaRepositorio.GetByEmailAsync(pessoa.Email) != null)
                return Result.Fail("E-mail já cadastrado.");

            if (!enderecos.Any())
            {
                return Result.Fail("Deve ser inserido pelo menos um endereço");
            }


            foreach (var item in enderecos)
            {
                pessoa.Enderecos.Add(item);
            }

            await _pessoaRepositorio.AddAsync(pessoa);

            return Result.Ok("Pessoa cadastrada com sucesso.");
        }

        public async Task<Pessoa?> ObtenhaPeloId(int id) => await _pessoaRepositorio.GetByIdAsync(id);

        public async Task<IEnumerable<Pessoa>> ObtenhaTodos() => await _pessoaRepositorio.GetAllAsync();
    }
}
