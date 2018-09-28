namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCampoEmailFuncionario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionario", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionario", "Email");
        }
    }
}
