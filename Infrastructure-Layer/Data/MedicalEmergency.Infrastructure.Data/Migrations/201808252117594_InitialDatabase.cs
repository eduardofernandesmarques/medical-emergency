namespace MedicalEmergency.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Manager.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 25, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmergencyType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50, unicode: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HealthUnit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InstitutionTypeID = c.Int(),
                        EmergencyTypeID = c.Int(),
                        Name = c.String(maxLength: 150, unicode: false),
                        Address = c.String(maxLength: 8000, unicode: false),
                        Phone = c.String(maxLength: 50, unicode: false),
                        Latitude = c.String(maxLength: 20, unicode: false),
                        Longitude = c.String(maxLength: 20, unicode: false),
                        LinkPT = c.String(maxLength: 100, unicode: false),
                        LinkEN = c.String(maxLength: 150, unicode: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmergencyType", t => t.EmergencyTypeID)
                .ForeignKey("dbo.InstitutionType", t => t.InstitutionTypeID)
                .Index(t => t.InstitutionTypeID)
                .Index(t => t.EmergencyTypeID);
            
            CreateTable(
                "dbo.InstitutionType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50, unicode: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Speciality",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50, unicode: false),
                        Language = c.String(maxLength: 2, unicode: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Active = c.Boolean(),
                        HealthUnit_ID = c.Int(),
                        HealthUnit_ID1 = c.Int(),
                        HealthUnit_ID2 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HealthUnit", t => t.HealthUnit_ID)
                .ForeignKey("dbo.HealthUnit", t => t.HealthUnit_ID1)
                .ForeignKey("dbo.HealthUnit", t => t.HealthUnit_ID2)
                .Index(t => t.HealthUnit_ID)
                .Index(t => t.HealthUnit_ID1)
                .Index(t => t.HealthUnit_ID2);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Speciality", "HealthUnit_ID2", "dbo.HealthUnit");
            DropForeignKey("dbo.Speciality", "HealthUnit_ID1", "dbo.HealthUnit");
            DropForeignKey("dbo.Speciality", "HealthUnit_ID", "dbo.HealthUnit");
            DropForeignKey("dbo.HealthUnit", "InstitutionTypeID", "dbo.InstitutionType");
            DropForeignKey("dbo.HealthUnit", "EmergencyTypeID", "dbo.EmergencyType");
            DropIndex("dbo.Speciality", new[] { "HealthUnit_ID2" });
            DropIndex("dbo.Speciality", new[] { "HealthUnit_ID1" });
            DropIndex("dbo.Speciality", new[] { "HealthUnit_ID" });
            DropIndex("dbo.HealthUnit", new[] { "EmergencyTypeID" });
            DropIndex("dbo.HealthUnit", new[] { "InstitutionTypeID" });
            DropTable("dbo.Speciality");
            DropTable("dbo.InstitutionType");
            DropTable("dbo.HealthUnit");
            DropTable("dbo.EmergencyType");
            DropTable("Manager.Account");
        }
    }
}
