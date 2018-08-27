namespace MedicalEmergency.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
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
                        Latitude = c.String(maxLength: 25, unicode: false),
                        Longitude = c.String(maxLength: 25, unicode: false),
                        LinkES = c.String(maxLength: 150, unicode: false),
                        LinkEN = c.String(maxLength: 150, unicode: false),
                        LinkES1 = c.String(maxLength: 8000, unicode: false),
                        SpecialtiesPT = c.String(maxLength: 8000, unicode: false),
                        SpecialtiesEN = c.String(maxLength: 8000, unicode: false),
                        SpecialtiesES = c.String(maxLength: 8000, unicode: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HealthUnit", "InstitutionTypeID", "dbo.InstitutionType");
            DropForeignKey("dbo.HealthUnit", "EmergencyTypeID", "dbo.EmergencyType");
            DropIndex("dbo.HealthUnit", new[] { "EmergencyTypeID" });
            DropIndex("dbo.HealthUnit", new[] { "InstitutionTypeID" });
            DropTable("dbo.InstitutionType");
            DropTable("dbo.HealthUnit");
            DropTable("dbo.EmergencyType");
            DropTable("dbo.Account");
        }
    }
}
