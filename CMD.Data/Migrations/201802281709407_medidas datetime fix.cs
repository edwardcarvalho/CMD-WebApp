namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medidasdatetimefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medidas", "DataOcorrencia", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Medidas", "DataSolicitacao", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Medidas", "DataInicioSuspensao", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Medidas", "DataFinalSuspensao", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medidas", "DataFinalSuspensao", c => c.DateTime());
            AlterColumn("dbo.Medidas", "DataInicioSuspensao", c => c.DateTime());
            AlterColumn("dbo.Medidas", "DataSolicitacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Medidas", "DataOcorrencia", c => c.DateTime(nullable: false));
        }
    }
}
