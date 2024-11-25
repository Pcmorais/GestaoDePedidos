using GestaoPedidos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.CPF).IsUnique();

            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.Email).IsUnique();

            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.SKU).IsUnique();

            modelBuilder.Entity<Endereco>()
                .Property(e => e.TipoEndereco)
                .IsRequired();

            modelBuilder.Entity<PedidoItem>()
                .Property(pi => pi.Quantidade)
                .IsRequired();
        }
    }

}
