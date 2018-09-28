namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPerfilAUsuarios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        IdPerfil = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.IdPerfil);
            
            AddColumn("dbo.Medidas", "DataInicioSuspensao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Medidas", "DataFinalSuspensao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Medidas", "Comentario1", c => c.String());
            AddColumn("dbo.Medidas", "Comentario2", c => c.String());
            AddColumn("dbo.Medidas", "Alinea", c => c.String());
            AddColumn("dbo.Usuarios", "Perfil_IdPerfil", c => c.Int());
            CreateIndex("dbo.Usuarios", "Perfil_IdPerfil");
            AddForeignKey("dbo.Usuarios", "Perfil_IdPerfil", "dbo.Perfil", "IdPerfil");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "Perfil_IdPerfil", "dbo.Perfil");
            DropIndex("dbo.Usuarios", new[] { "Perfil_IdPerfil" });
            DropColumn("dbo.Usuarios", "Perfil_IdPerfil");
            DropColumn("dbo.Medidas", "Alinea");
            DropColumn("dbo.Medidas", "Comentario2");
            DropColumn("dbo.Medidas", "Comentario1");
            DropColumn("dbo.Medidas", "DataFinalSuspensao");
            DropColumn("dbo.Medidas", "DataInicioSuspensao");
            DropTable("dbo.Perfil");
        }
    }
}
