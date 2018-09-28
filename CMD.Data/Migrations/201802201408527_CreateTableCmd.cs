namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCmd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertencias",
                c => new
                    {
                        IdAdvertencia = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdAdvertencia);
            
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        IdCargo = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.IdCargo);
            
            CreateTable(
                "dbo.Filial",
                c => new
                    {
                        IdFilial = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdFilial);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        IdFuncionario = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Cpf = c.String(),
                        Rg = c.String(),
                        Matricula = c.String(),
                        IdOperacao = c.Long(nullable: false),
                        IdCargo = c.Long(nullable: false),
                        IdFilial = c.Long(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Operacao_IdOperacao = c.Long(),
                    })
                .PrimaryKey(t => t.IdFuncionario)
                .ForeignKey("dbo.Cargo", t => t.IdCargo, cascadeDelete: true)
                .ForeignKey("dbo.Operacao", t => t.Operacao_IdOperacao)
                .ForeignKey("dbo.Operacao", t => t.IdOperacao, cascadeDelete: true)
                .ForeignKey("dbo.Filial", t => t.IdFilial)
                .Index(t => t.IdOperacao)
                .Index(t => t.IdCargo)
                .Index(t => t.IdFilial)
                .Index(t => t.Operacao_IdOperacao);
            
            CreateTable(
                "dbo.Medidas",
                c => new
                    {
                        IdMedida = c.Long(nullable: false, identity: true),
                        IdTipoAdvertencia = c.Int(nullable: false),
                        IdMotivo = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        IdTipoOcorrencia = c.Int(nullable: false),
                        Telefone = c.String(),
                        Descricao = c.String(),
                        IdFilial = c.Long(nullable: false),
                        IdAProvador = c.Long(nullable: false),
                        IdFuncionario = c.Long(nullable: false),
                        IdStatusMedida = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMedida)
                .ForeignKey("dbo.Funcionario", t => t.IdFuncionario, cascadeDelete: true)
                .Index(t => t.IdFuncionario);
            
            CreateTable(
                "dbo.Operacao",
                c => new
                    {
                        IdOperacao = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Gerente_IdFuncionario = c.Long(),
                        Supervisor_IdFuncionario = c.Long(),
                    })
                .PrimaryKey(t => t.IdOperacao)
                .ForeignKey("dbo.Funcionario", t => t.Gerente_IdFuncionario)
                .ForeignKey("dbo.Funcionario", t => t.Supervisor_IdFuncionario)
                .Index(t => t.Gerente_IdFuncionario)
                .Index(t => t.Supervisor_IdFuncionario);
            
            CreateTable(
                "dbo.Motivos",
                c => new
                    {
                        IdMotivo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdMotivo);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IdUsuario = c.Long(nullable: false, identity: true),
                        IdFuncionario = c.Long(nullable: false),
                        Login = c.String(),
                        Senha = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionario", "IdFilial", "dbo.Filial");
            DropForeignKey("dbo.Funcionario", "IdOperacao", "dbo.Operacao");
            DropForeignKey("dbo.Operacao", "Supervisor_IdFuncionario", "dbo.Funcionario");
            DropForeignKey("dbo.Operacao", "Gerente_IdFuncionario", "dbo.Funcionario");
            DropForeignKey("dbo.Funcionario", "Operacao_IdOperacao", "dbo.Operacao");
            DropForeignKey("dbo.Medidas", "IdFuncionario", "dbo.Funcionario");
            DropForeignKey("dbo.Funcionario", "IdCargo", "dbo.Cargo");
            DropIndex("dbo.Operacao", new[] { "Supervisor_IdFuncionario" });
            DropIndex("dbo.Operacao", new[] { "Gerente_IdFuncionario" });
            DropIndex("dbo.Medidas", new[] { "IdFuncionario" });
            DropIndex("dbo.Funcionario", new[] { "Operacao_IdOperacao" });
            DropIndex("dbo.Funcionario", new[] { "IdFilial" });
            DropIndex("dbo.Funcionario", new[] { "IdCargo" });
            DropIndex("dbo.Funcionario", new[] { "IdOperacao" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Motivos");
            DropTable("dbo.Operacao");
            DropTable("dbo.Medidas");
            DropTable("dbo.Funcionario");
            DropTable("dbo.Filial");
            DropTable("dbo.Cargo");
            DropTable("dbo.Advertencias");
        }
    }
}
