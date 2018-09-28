namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TesteSeed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Funcionario", "IdCargo", "dbo.Cargo");
            DropForeignKey("dbo.Funcionario", "IdOperacao", "dbo.Operacao");
            DropIndex("dbo.Funcionario", new[] { "IdOperacao" });
            DropIndex("dbo.Funcionario", new[] { "IdCargo" });
            RenameColumn(table: "dbo.Funcionario", name: "IdFilial", newName: "Filial_IdFilial");
            RenameColumn(table: "dbo.Funcionario", name: "IdCargo", newName: "Cargo_IdCargo");
            RenameColumn(table: "dbo.Funcionario", name: "IdOperacao", newName: "Operacao_IdOperacao1");
            RenameIndex(table: "dbo.Funcionario", name: "IX_IdFilial", newName: "IX_Filial_IdFilial");
            AlterColumn("dbo.Funcionario", "Operacao_IdOperacao1", c => c.Long());
            AlterColumn("dbo.Funcionario", "Cargo_IdCargo", c => c.Long());
            CreateIndex("dbo.Funcionario", "Cargo_IdCargo");
            CreateIndex("dbo.Funcionario", "Operacao_IdOperacao1");
            AddForeignKey("dbo.Funcionario", "Cargo_IdCargo", "dbo.Cargo", "IdCargo");
            AddForeignKey("dbo.Funcionario", "Operacao_IdOperacao1", "dbo.Operacao", "IdOperacao");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionario", "Operacao_IdOperacao1", "dbo.Operacao");
            DropForeignKey("dbo.Funcionario", "Cargo_IdCargo", "dbo.Cargo");
            DropIndex("dbo.Funcionario", new[] { "Operacao_IdOperacao1" });
            DropIndex("dbo.Funcionario", new[] { "Cargo_IdCargo" });
            AlterColumn("dbo.Funcionario", "Cargo_IdCargo", c => c.Long(nullable: false));
            AlterColumn("dbo.Funcionario", "Operacao_IdOperacao1", c => c.Long(nullable: false));
            RenameIndex(table: "dbo.Funcionario", name: "IX_Filial_IdFilial", newName: "IX_IdFilial");
            RenameColumn(table: "dbo.Funcionario", name: "Operacao_IdOperacao1", newName: "IdOperacao");
            RenameColumn(table: "dbo.Funcionario", name: "Cargo_IdCargo", newName: "IdCargo");
            RenameColumn(table: "dbo.Funcionario", name: "Filial_IdFilial", newName: "IdFilial");
            CreateIndex("dbo.Funcionario", "IdCargo");
            CreateIndex("dbo.Funcionario", "IdOperacao");
            AddForeignKey("dbo.Funcionario", "IdOperacao", "dbo.Operacao", "IdOperacao", cascadeDelete: true);
            AddForeignKey("dbo.Funcionario", "IdCargo", "dbo.Cargo", "IdCargo", cascadeDelete: true);
        }
    }
}
