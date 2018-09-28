namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAtivoTbMedidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medidas", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medidas", "Ativo");
        }
    }
}
