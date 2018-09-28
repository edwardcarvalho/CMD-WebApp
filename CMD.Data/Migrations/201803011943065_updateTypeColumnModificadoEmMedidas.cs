namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTypeColumnModificadoEmMedidas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medidas", "Modificado", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medidas", "Modificado", c => c.DateTime(nullable: false));
        }
    }
}
