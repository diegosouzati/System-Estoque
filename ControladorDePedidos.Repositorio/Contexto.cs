using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.Repositorio
{
    public class Contexto : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }

        public Contexto() : base("DefaultConnection") { } //Método criado para conexão do banco de dados com o programa
        
        public DbSet<Marca> Marca  { get; set; } //Criação da Tabela Marca no Banco de Dados
      
        public DbSet<Produto> Produto { get; set; } //Tabela no banco de dados para produtos

        public DbSet<Usuario> Usuario { get; set; } //Tabela no banco de dados para usuarios

        public DbSet<Cliente> Cliente { get; set; } //Tabela no banco de dados para cliente

        public DbSet<Compra> Compra { get; set; } //Tabela no banco para compras

        public DbSet<ItemDaCompra> ItemDaCompra { get; set; } //Tabela no banco para compras

        public DbSet<Venda> Venda { get; set; } //Tabela no banco para venda

        public DbSet<ItemDaVenda> ItemDaVenda { get; set; } //Tabela no banco para venda

        public DbSet<Vendedor> Vendedor { get; set; } //Tabela no banco para venda

        public DbSet<Fornecedor> Fornecedor { get; set; } //Tabela no banco para Fornecedor


    }
}
