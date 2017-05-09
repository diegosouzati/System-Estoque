namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailcliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Email");
        }
    }
}
