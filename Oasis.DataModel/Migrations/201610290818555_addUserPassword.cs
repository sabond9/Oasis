namespace Oasis.DataModel.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addUserPassword : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PasswordHash = c.String(nullable: false, maxLength: 100),
                        PasswordCreatedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Permissions", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 100,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Username",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPasswords", "UserId", "dbo.Users");
            DropIndex("dbo.UserPasswords", new[] { "UserId" });
            AlterColumn("dbo.Users", "PasswordHash", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String(
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Username",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.Permissions", "Name", c => c.String());
            DropTable("dbo.UserPasswords");
        }
    }
}
