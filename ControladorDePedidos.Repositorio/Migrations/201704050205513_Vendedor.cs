namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vendedor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendedor",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CPF = c.String(),
                        RG = c.String(),
                        Endereco = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        CEP = c.String(),
                        Estado = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                        Comissao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            AddColumn("dbo.Venda", "Vendedor_Codigo", c => c.Int());
            CreateIndex("dbo.Venda", "Vendedor_Codigo");
            AddForeignKey("dbo.Venda", "Vendedor_Codigo", "dbo.Vendedor", "Codigo");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venda", "Vendedor_Codigo", "dbo.Vendedor");
            DropIndex("dbo.Venda", new[] { "Vendedor_Codigo" });
            DropColumn("dbo.Venda", "Vendedor_Codigo");
            DropTable("dbo.Vendedor");
        }
    }
}
