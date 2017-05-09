namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class venda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemDaVenda",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto_Codigo = c.Int(),
                        Venda_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Produto", t => t.Produto_Codigo)
                .ForeignKey("dbo.Venda", t => t.Venda_Codigo)
                .Index(t => t.Produto_Codigo)
                .Index(t => t.Venda_Codigo);
            
            CreateTable(
                "dbo.Venda",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        DataDeCadastro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DataDeEfetivacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StatusDaVenda = c.Int(nullable: false),
                        Cliente_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Cliente", t => t.Cliente_Codigo)
                .Index(t => t.Cliente_Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemDaVenda", "Venda_Codigo", "dbo.Venda");
            DropForeignKey("dbo.Venda", "Cliente_Codigo", "dbo.Cliente");
            DropForeignKey("dbo.ItemDaVenda", "Produto_Codigo", "dbo.Produto");
            DropIndex("dbo.Venda", new[] { "Cliente_Codigo" });
            DropIndex("dbo.ItemDaVenda", new[] { "Venda_Codigo" });
            DropIndex("dbo.ItemDaVenda", new[] { "Produto_Codigo" });
            DropTable("dbo.Venda");
            DropTable("dbo.ItemDaVenda");
        }
    }
}
