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
                TipoEndereco = x.TipoEndereco
            });

            if (!ValidarCPF(pessoa.CPF))
            {
                return Result.Fail("CPF inválido.");
            }

            if (await _pessoaRepositorio.GetByCPFAsync(pessoa.CPF) != null)
                return Result.Fail("CPF já cadastrado.");

            if (await _pessoaRepositorio.GetByEmailAsync(pessoa.Email) != null)
                return Result.Fail("E-mail já cadastrado.");

            if (!enderecos.Any())
            {
                return Result.Fail("Deve ser inserido pelo menos um endereço");
            }

            foreach (var endereco in enderecos)
            {
                if (!ValidarCEP(endereco.CEP))
                {
                    return Result.Fail($"CEP inválido: {endereco.CEP}");
                }

                if (!ValidarTipoEndereco(endereco.TipoEndereco))
                {
                    return Result.Fail($"Tipo de endereço invalido: {endereco.TipoEndereco}. Deve ser 'Entrega', 'Cobrança' ou 'Ambos'.");
                }

                if (endereco.TipoEndereco.Equals("Cobrança", StringComparison.OrdinalIgnoreCase))
                {
                    endereco.TipoEndereco = "cobranca";
                }
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

        #region Utils
        private bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray()); // Remove não dígitos
            if (cpf.Length != 11) return false;

            // Evita CPFs com todos os dígitos iguais
            if (new string(cpf[0], 11) == cpf) return false;

            // Calcula os dígitos verificadores
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            string tempCpf, digito;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();

            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        // Método para validar CEP
        private bool ValidarCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return false;

            cep = new string(cep.Where(char.IsDigit).ToArray()); // Remove não dígitos
            return cep.Length == 8;
        }

        private bool ValidarTipoEndereco(string tipoEndereco)
        {
            string[] tiposValidos = { "Entrega", "Cobrança", "Ambos" };
            return tiposValidos.Contains(tipoEndereco, StringComparer.OrdinalIgnoreCase);
        }
        #endregion
    }
}
