using GestaoPedidos.Application.DTO.Response;
using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPedidos.Application.UseCases
{
    public class EnderecosPorPessoUseCase
    {
        private readonly IEnderecoRepositorio _enderecoRepositorio;

        public EnderecosPorPessoUseCase(IEnderecoRepositorio enderecoRepositorio)
        {
            _enderecoRepositorio = enderecoRepositorio;
        }

        public async Task<List<EnderecoResponseDTO>> Execute(int idPessoa)
        {
            var enderecos = await _enderecoRepositorio.GetEnderecoPorIdPessoaAsync(idPessoa);

            if (enderecos == null || !enderecos.Any())
            {
                throw new Exception("Nenhum endereço encontrado para esta pessoa.");
            }

            return enderecos.Select(e => new EnderecoResponseDTO
            {
                Id = e.Id,
                Bairro = e.Bairro,
                Cidade = e.Cidade,
                Estado = e.Estado,
                CEP = e.CEP,
                TipoEndereco = e.TipoEndereco,
                Logradouro = e.Logradouro,
                Numero = e.Numero,
                Complemento = e.Complemento
            }).ToList();
        }
    } 
}
