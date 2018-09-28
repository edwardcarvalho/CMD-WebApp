namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuarios", "Funcionario_IdFuncionario", "dbo.Funcionario");
            DropIndex("dbo.Usuarios", new[] { "Funcionario_IdFuncionario" });
            AddColumn("dbo.Usuarios", "IdFuncionario", c => c.Long(nullable: false));
            DropColumn("dbo.Usuarios", "Funcionario_IdFuncionario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "Funcionario_IdFuncionario", c => c.Long());
            DropColumn("dbo.Usuarios", "IdFuncionario");
            CreateIndex("dbo.Usuarios", "Funcionario_IdFuncionario");
            AddForeignKey("dbo.Usuarios", "Funcionario_IdFuncionario", "dbo.Funcionario", "IdFuncionario");
        }
    }
}
