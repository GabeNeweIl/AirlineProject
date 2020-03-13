namespace Airline.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrewMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Position = c.String(),
                        Age = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Departure = c.DateTime(nullable: false),
                        Arrival = c.DateTime(nullable: false),
                        FromCountry = c.String(),
                        ToCountry = c.String(),
                        FromCity = c.String(),
                        ToCity = c.String(),
                        TotalNumberPassengers = c.Int(nullable: false),
                        CurrentNumberPassengers = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        StatusReady = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        PassportID = c.String(),
                        Birth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        StatusAfter = c.Boolean(nullable: false),
                        StatusBefore = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlightCrewMembers",
                c => new
                    {
                        Flight_Id = c.Int(nullable: false),
                        CrewMember_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Flight_Id, t.CrewMember_Id })
                .ForeignKey("dbo.Flights", t => t.Flight_Id, cascadeDelete: true)
                .ForeignKey("dbo.CrewMembers", t => t.CrewMember_Id, cascadeDelete: true)
                .Index(t => t.Flight_Id)
                .Index(t => t.CrewMember_Id);
            
            CreateTable(
                "dbo.ProfileFlights",
                c => new
                    {
                        Profile_Id = c.Int(nullable: false),
                        Flight_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_Id, t.Flight_Id })
                .ForeignKey("dbo.Profiles", t => t.Profile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Flights", t => t.Flight_Id, cascadeDelete: true)
                .Index(t => t.Profile_Id)
                .Index(t => t.Flight_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileFlights", "Flight_Id", "dbo.Flights");
            DropForeignKey("dbo.ProfileFlights", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.FlightCrewMembers", "CrewMember_Id", "dbo.CrewMembers");
            DropForeignKey("dbo.FlightCrewMembers", "Flight_Id", "dbo.Flights");
            DropIndex("dbo.ProfileFlights", new[] { "Flight_Id" });
            DropIndex("dbo.ProfileFlights", new[] { "Profile_Id" });
            DropIndex("dbo.FlightCrewMembers", new[] { "CrewMember_Id" });
            DropIndex("dbo.FlightCrewMembers", new[] { "Flight_Id" });
            DropTable("dbo.ProfileFlights");
            DropTable("dbo.FlightCrewMembers");
            DropTable("dbo.Requests");
            DropTable("dbo.Profiles");
            DropTable("dbo.Flights");
            DropTable("dbo.CrewMembers");
        }
    }
}
