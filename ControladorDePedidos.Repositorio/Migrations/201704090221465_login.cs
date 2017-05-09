namespace ControladorDePedidos.Repositorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Senha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Senha");
        }
    }
}
