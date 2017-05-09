namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compra : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Compra", "QuantidadeDeProdutos");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compra", "QuantidadeDeProdutos", c => c.Int(nullable: false));
        }
    }
}
