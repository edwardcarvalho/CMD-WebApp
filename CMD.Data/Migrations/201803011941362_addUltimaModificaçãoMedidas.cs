namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUltimaModificaçãoMedidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medidas", "Modificado", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medidas", "Modificado");
        }
    }
}
