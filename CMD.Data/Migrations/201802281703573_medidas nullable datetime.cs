namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medidasnullabledatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medidas", "DataInicioSuspensao", c => c.DateTime());
            AlterColumn("dbo.Medidas", "DataFinalSuspensao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medidas", "DataFinalSuspensao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Medidas", "DataInicioSuspensao", c => c.DateTime(nullable: false));
        }
    }
}
