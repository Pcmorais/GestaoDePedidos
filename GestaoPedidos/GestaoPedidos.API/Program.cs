using GestaoPedidos.Application.Interfaces.Repositorio;
using GestaoPedidos.Application.Interfaces.Servicos;
using GestaoPedidos.Application.Servicos;
using GestaoPedidos.Application.UseCases;
using GestaoPedidos.Domain.Entidades;
using GestaoPedidos.Infra;
using GestaoPedidos.Infra.Repositorios;
using GestaoPedidos.Infra.Servicos;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IRepositorio<Pessoa>, RepositorioBase<Pessoa>>();
            builder.Services.AddScoped<IRepositorio<Produto>, RepositorioBase<Produto>>();
            builder.Services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            builder.Services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
            builder.Services.AddScoped<IServicoExcelPedidos, ServicoPedidosExcel>();
            builder.Services.AddScoped<ServicoPessoa>();
            builder.Services.AddScoped<EnderecosPorPessoUseCase>();
            builder.Services.AddScoped<ServicoProduto>();
            builder.Services.AddScoped<ServicoPedido>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Obtém o caminho para o arquivo XML
                var xmlFile = Path.Combine(AppContext.BaseDirectory, "GestaoPedidos.Api.xml");
                options.IncludeXmlComments(xmlFile);
            });


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
