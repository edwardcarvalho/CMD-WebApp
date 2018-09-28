namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataOcorrencia_solicitacao_medidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medidas", "DataOcorrencia", c => c.DateTime(nullable: false));
            AddColumn("dbo.Medidas", "DataSolicitacao", c => c.DateTime(nullable: false));
            DropColumn("dbo.Medidas", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medidas", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.Medidas", "DataSolicitacao");
            DropColumn("dbo.Medidas", "DataOcorrencia");
        }
    }
}
