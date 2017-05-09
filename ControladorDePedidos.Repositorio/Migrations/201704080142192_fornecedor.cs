namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fornecedor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fornecedor",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Endereco = c.String(),
                        Complemento = c.String(),
                        Cidade = c.String(),
                        Bairro = c.String(),
                        Cep = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        CNPJ = c.String(),
                        InscricaoEstadual = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fornecedor");
        }
    }
}
