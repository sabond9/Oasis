namespace Oasis.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePermissions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PermissionRoles", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.PermissionRoles", new[] { "RoleId" });
            DropIndex("dbo.PermissionRoles", new[] { "PermissionId" });
            DropTable("dbo.Permissions");
            DropTable("dbo.PermissionRoles");
        }
    }
}
