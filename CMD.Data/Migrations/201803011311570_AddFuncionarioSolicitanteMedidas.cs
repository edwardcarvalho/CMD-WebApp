namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFuncionarioSolicitanteMedidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medidas", "FuncSolicitante_IdFuncionario", c => c.Long());
            CreateIndex("dbo.Medidas", "FuncSolicitante_IdFuncionario");
            AddForeignKey("dbo.Medidas", "FuncSolicitante_IdFuncionario", "dbo.Funcionario", "IdFuncionario");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medidas", "FuncSolicitante_IdFuncionario", "dbo.Funcionario");
            DropIndex("dbo.Medidas", new[] { "FuncSolicitante_IdFuncionario" });
            DropColumn("dbo.Medidas", "FuncSolicitante_IdFuncionario");
        }
    }
}
