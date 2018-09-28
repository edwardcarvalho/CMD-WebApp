namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFuncionarioCollection : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.Medidas", new[] { "Funcionario_IdFuncionario" });
            //DropIndex("dbo.Medidas", new[] { "Funcionario_IdFuncionario1" });
            //DropColumn("dbo.Medidas", "Funcionario_IdFuncionario");
            //RenameColumn(table: "dbo.Medidas", name: "Funcionario_IdFuncionario1", newName: "Funcionario_IdFuncionario");
            //AlterColumn("dbo.Medidas", "Funcionario_IdFuncionario", c => c.Long(nullable: false));
            //AlterColumn("dbo.Medidas", "Funcionario_IdFuncionario", c => c.Long(nullable: false));
            //CreateIndex("dbo.Medidas", "Funcionario_IdFuncionario");
        }
        
        public override void Down()
        {
            //DropIndex("dbo.Medidas", new[] { "Funcionario_IdFuncionario" });
            //AlterColumn("dbo.Medidas", "Funcionario_IdFuncionario", c => c.Long());
            //AlterColumn("dbo.Medidas", "Funcionario_IdFuncionario", c => c.Long());
            //RenameColumn(table: "dbo.Medidas", name: "Funcionario_IdFuncionario", newName: "Funcionario_IdFuncionario1");
            //AddColumn("dbo.Medidas", "Funcionario_IdFuncionario", c => c.Long());
            //CreateIndex("dbo.Medidas", "Funcionario_IdFuncionario1");
            //CreateIndex("dbo.Medidas", "Funcionario_IdFuncionario");
        }
    }
}
