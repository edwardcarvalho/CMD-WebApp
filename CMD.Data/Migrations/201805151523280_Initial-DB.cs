namespace CMD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertencias",
                c => new
                    {
                        AdvertenciaId = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdvertenciaId);
            
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        CargoId = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.CargoId);
            
            CreateTable(
                "dbo.Filial",
                c => new
                    {
                        FilialId = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FilialId);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        FuncionarioId = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Cpf = c.String(),
                        Rg = c.String(),
                        Matricula = c.String(),
                        Email = c.String(),
                        OperacaoId = c.Long(),
                        CargoId = c.Long(nullable: false),
                        FilialId = c.Long(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FuncionarioId)
                .ForeignKey("dbo.Cargo", t => t.CargoId, cascadeDelete: true)
                .ForeignKey("dbo.Filial", t => t.FilialId, cascadeDelete: true)
                .ForeignKey("dbo.Operacao", t => t.OperacaoId)
                .Index(t => t.OperacaoId)
                .Index(t => t.CargoId)
                .Index(t => t.FilialId);
            
            CreateTable(
                "dbo.Medidas",
                c => new
                    {
                        MedidaId = c.Long(nullable: false, identity: true),
                        FuncionarioId = c.Long(),
                        FuncSolicitanteId = c.Long(),
                        FuncAprovadorId = c.Long(),
                        AdvertenciaId = c.Long(nullable: false),
                        MotivoId = c.Long(nullable: false),
                        DataOcorrencia = c.DateTime(precision: 7, storeType: "datetime2"),
                        DataSolicitacao = c.DateTime(precision: 7, storeType: "datetime2"),
                        DataInicioSuspensao = c.DateTime(precision: 7, storeType: "datetime2"),
                        DataFinalSuspensao = c.DateTime(precision: 7, storeType: "datetime2"),
                        IdTipoOcorrencia = c.Int(nullable: false),
                        Telefone = c.String(),
                        Descricao = c.String(),
                        Comentario1 = c.String(),
                        Comentario2 = c.String(),
                        Alinea = c.String(),
                        FilialId = c.Long(nullable: false),
                        StatusId = c.Long(nullable: false),
                        Suspensao = c.Boolean(nullable: false),
                        Modificado = c.DateTime(precision: 7, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MedidaId)
                .ForeignKey("dbo.Advertencias", t => t.AdvertenciaId, cascadeDelete: true)
                .ForeignKey("dbo.Filial", t => t.FilialId, cascadeDelete: true)
                .ForeignKey("dbo.Funcionario", t => t.FuncAprovadorId)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId)
                .ForeignKey("dbo.Funcionario", t => t.FuncSolicitanteId)
                .ForeignKey("dbo.Motivos", t => t.MotivoId, cascadeDelete: true)
                .ForeignKey("dbo.StatusMedida", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.FuncionarioId)
                .Index(t => t.FuncSolicitanteId)
                .Index(t => t.FuncAprovadorId)
                .Index(t => t.AdvertenciaId)
                .Index(t => t.MotivoId)
                .Index(t => t.FilialId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Motivos",
                c => new
                    {
                        MotivoId = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MotivoId);
            
            CreateTable(
                "dbo.StatusMedida",
                c => new
                    {
                        StatusId = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Operacao",
                c => new
                    {
                        OperacaoId = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        SupervisorId = c.Long(),
                        GerenteId = c.Long(),
                    })
                .PrimaryKey(t => t.OperacaoId)
                .ForeignKey("dbo.Funcionario", t => t.GerenteId)
                .ForeignKey("dbo.Funcionario", t => t.SupervisorId)
                .Index(t => t.SupervisorId)
                .Index(t => t.GerenteId);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        PerfilId = c.Long(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.PerfilId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FuncionarioId = c.Long(nullable: false),
                        PerfilId = c.Long(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId, cascadeDelete: true)
                .ForeignKey("dbo.Perfil", t => t.PerfilId, cascadeDelete: true)
                .Index(t => t.FuncionarioId)
                .Index(t => t.PerfilId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FuncionarioId", "dbo.Funcionario");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Funcionario", "OperacaoId", "dbo.Operacao");
            DropForeignKey("dbo.Operacao", "SupervisorId", "dbo.Funcionario");
            DropForeignKey("dbo.Operacao", "GerenteId", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "StatusId", "dbo.StatusMedida");
            DropForeignKey("dbo.Medidas", "MotivoId", "dbo.Motivos");
            DropForeignKey("dbo.Medidas", "FuncSolicitanteId", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "FuncionarioId", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "FuncAprovadorId", "dbo.Funcionario");
            DropForeignKey("dbo.Medidas", "FilialId", "dbo.Filial");
            DropForeignKey("dbo.Medidas", "AdvertenciaId", "dbo.Advertencias");
            DropForeignKey("dbo.Funcionario", "FilialId", "dbo.Filial");
            DropForeignKey("dbo.Funcionario", "CargoId", "dbo.Cargo");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PerfilId" });
            DropIndex("dbo.AspNetUsers", new[] { "FuncionarioId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Operacao", new[] { "GerenteId" });
            DropIndex("dbo.Operacao", new[] { "SupervisorId" });
            DropIndex("dbo.Medidas", new[] { "StatusId" });
            DropIndex("dbo.Medidas", new[] { "FilialId" });
            DropIndex("dbo.Medidas", new[] { "MotivoId" });
            DropIndex("dbo.Medidas", new[] { "AdvertenciaId" });
            DropIndex("dbo.Medidas", new[] { "FuncAprovadorId" });
            DropIndex("dbo.Medidas", new[] { "FuncSolicitanteId" });
            DropIndex("dbo.Medidas", new[] { "FuncionarioId" });
            DropIndex("dbo.Funcionario", new[] { "FilialId" });
            DropIndex("dbo.Funcionario", new[] { "CargoId" });
            DropIndex("dbo.Funcionario", new[] { "OperacaoId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Perfil");
            DropTable("dbo.Operacao");
            DropTable("dbo.StatusMedida");
            DropTable("dbo.Motivos");
            DropTable("dbo.Medidas");
            DropTable("dbo.Funcionario");
            DropTable("dbo.Filial");
            DropTable("dbo.Cargo");
            DropTable("dbo.Advertencias");
        }
    }
}
