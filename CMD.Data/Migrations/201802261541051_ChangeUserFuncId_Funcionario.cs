namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserFuncId_Funcionario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Funcionario_IdFuncionario", c => c.Long());
            CreateIndex("dbo.Usuarios", "Funcionario_IdFuncionario");
            AddForeignKey("dbo.Usuarios", "Funcionario_IdFuncionario", "dbo.Funcionario", "IdFuncionario");
            DropColumn("dbo.Usuarios", "IdFuncionario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "IdFuncionario", c => c.Long(nullable: false));
            DropForeignKey("dbo.Usuarios", "Funcionario_IdFuncionario", "dbo.Funcionario");
            DropIndex("dbo.Usuarios", new[] { "Funcionario_IdFuncionario" });
            DropColumn("dbo.Usuarios", "Funcionario_IdFuncionario");
        }
    }
}
