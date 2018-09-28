namespace CMD.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlagSuspensaoMedidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medidas", "Suspensao", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medidas", "Suspensao");
        }
    }
}
