namespace SCM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sunday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fuels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ColorificValue = c.String(),
                        DeleteForbidden = c.Boolean(nullable: false),
                        EditForbidden = c.Boolean(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        LastModifed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                        DeleteForbidden = c.Boolean(nullable: false),
                        EditForbidden = c.Boolean(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        LastModifed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Inn = c.String(),
                        DeleteForbidden = c.Boolean(nullable: false),
                        EditForbidden = c.Boolean(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        LastModifed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupplyFuels",
                c => new
                    {
                        Supply_Id = c.Int(nullable: false),
                        Fuel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Supply_Id, t.Fuel_Id })
                .ForeignKey("dbo.Supplies", t => t.Supply_Id, cascadeDelete: true)
                .ForeignKey("dbo.Fuels", t => t.Fuel_Id, cascadeDelete: true)
                .Index(t => t.Supply_Id)
                .Index(t => t.Fuel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplyFuels", "Fuel_Id", "dbo.Fuels");
            DropForeignKey("dbo.SupplyFuels", "Supply_Id", "dbo.Supplies");
            DropIndex("dbo.SupplyFuels", new[] { "Fuel_Id" });
            DropIndex("dbo.SupplyFuels", new[] { "Supply_Id" });
            DropTable("dbo.SupplyFuels");
            DropTable("dbo.Providers");
            DropTable("dbo.Supplies");
            DropTable("dbo.Fuels");
        }
    }
}
