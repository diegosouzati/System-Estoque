namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VendedoresApelido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendedor", "Apelido", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendedor", "Apelido");
        }
    }
}
