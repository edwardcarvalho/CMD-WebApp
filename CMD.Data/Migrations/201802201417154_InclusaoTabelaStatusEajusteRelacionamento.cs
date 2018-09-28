namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InclusaoTabelaStatusEajusteRelacionamento : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medidas", "IdFuncionario", "dbo.Funcionario");
            DropIndex("dbo.Medidas", new[] { "IdFuncionario" });
            RenameColumn(table: "dbo.Medidas", name: "IdFuncionario", newName: "Funcionario_IdFuncionario1");
            CreateTable(
                "dbo.StatusMedida",
                c => new
                    {
                        IdStatus = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.IdStatus);
            
            AddColumn("dbo.Medidas", "Advertencia_IdAdvertencia", c => c.Int());
            AddColumn("dbo.Medidas", "Filial_IdFilial", c => c.Long());
            AddColumn("dbo.Medidas", "FuncAprovador_IdFuncionario", c => c.Long());
            AddColumn("dbo.Medidas", "Funcionario_IdFuncionario", c => c.Long());
            AddColumn("dbo.Medidas", "Motivo_IdMotivo", c => c.Int());
            AddColumn("dbo.Medidas", "Status_IdStatus", c => c.Long());
            AlterColumn("dbo.Medidas", "Funcionario_IdFuncionario1", c => c.Long());
            CreateIndex("dbo.Medidas", "Advertencia_IdAdvertencia");
            CreateIndex("dbo.Medidas", "Filial_IdFilial");
            CreateIndex("dbo.Medidas", "FuncAprovador_IdFuncionario");
            CreateIndex("dbo.Medidas", "Funcionario_IdFuncionario");
            CreateIndex("dbo.Medidas", "Motivo_IdMotivo");
            CreateIndex("dbo.Medidas", "Status_IdStatus");
            CreateIndex("dbo.Medidas", "Funcionario_IdFuncionario1");
            AddForeignKey("dbo.Medidas", "Advertencia_IdAdvertencia", "dbo.Advertencias", "IdAdvertencia");
            AddForeignKey("dbo.Medidas", "Filial_IdFilial", "dbo.Filial", "IdFilial");
            AddForeignKey("dbo.Medidas", "FuncAprovador_IdFuncionario", "dbo.Funcionario", "IdFuncionario");
            AddForeignKey("dbo.Medidas", "Funcionario_IdFuncionario", "dbo.Funcionario", "IdFuncionario");
            AddForeignKey("dbo.Medidas", "Motivo_IdMotivo", "dbo.Motivos", "IdMotivo");
            AddForeignKey("dbo.Medidas", "Status_IdStatus", "dbo.StatusMedida", "IdStatus");
            AddForeignKey("dbo.Medidas", "Funcionario_IdFuncionario1", "dbo.Funcionario", "IdFuncionario");
            DropColumn("dbo.Medidas", "IdTipoAdvertencia");
            DropColumn("dbo.Medidas", "IdMotivo");
            DropColumn("dbo.Medidas", "IdFilial");
            DropColumn("dbo.Medidas", "IdAProvador");
            DropColumn("dbo.Medidas", "IdStatusMedida");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medidas", "IdStatusMedida", c => c.Int(nullable: false));
            AddColumn("dbo.Medidas", "IdAProvador", c => c.Long(nullable: false));
            AddColumn("dbo.Medidas", "IdFilial", c => c.Long(nullable: false));
            AddColumn("dbo.Medidas", "IdMotivo", c => c.Int(nullable: false));
            AddColumn("dbo.Medidas", "IdTipoAdvertencia", c => c.Int(nullable: false));
            DropForeignKey("dbo.Medidas", "Funcionario_IdFuncionario1", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "Status_IdStatus", "dbo.StatusMedida");
            DropForeignKey("dbo.Medidas", "Motivo_IdMotivo", "dbo.Motivos");
            DropForeignKey("dbo.Medidas", "Funcionario_IdFuncionario", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "FuncAprovador_IdFuncionario", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "Filial_IdFilial", "dbo.Filial");
            DropForeignKey("dbo.Medidas", "Advertencia_IdAdvertencia", "dbo.Advertencias");
            DropIndex("dbo.Medidas", new[] { "Funcionario_IdFuncionario1" });
            DropIndex("dbo.Medidas", new[] { "Status_IdStatus" });
            DropIndex("dbo.Medidas", new[] { "Motivo_IdMotivo" });
            DropIndex("dbo.Medidas", new[] { "Funcionario_IdFuncionario" });
            DropIndex("dbo.Medidas", new[] { "FuncAprovador_IdFuncionario" });
            DropIndex("dbo.Medidas", new[] { "Filial_IdFilial" });
            DropIndex("dbo.Medidas", new[] { "Advertencia_IdAdvertencia" });
            AlterColumn("dbo.Medidas", "Funcionario_IdFuncionario1", c => c.Long(nullable: false));
            DropColumn("dbo.Medidas", "Status_IdStatus");
            DropColumn("dbo.Medidas", "Motivo_IdMotivo");
            DropColumn("dbo.Medidas", "Funcionario_IdFuncionario");
            DropColumn("dbo.Medidas", "FuncAprovador_IdFuncionario");
            DropColumn("dbo.Medidas", "Filial_IdFilial");
            DropColumn("dbo.Medidas", "Advertencia_IdAdvertencia");
            DropTable("dbo.StatusMedida");
            RenameColumn(table: "dbo.Medidas", name: "Funcionario_IdFuncionario1", newName: "IdFuncionario");
            CreateIndex("dbo.Medidas", "IdFuncionario");
            AddForeignKey("dbo.Medidas", "IdFuncionario", "dbo.Funcionario", "IdFuncionario", cascadeDelete: true);
        }
    }
}
