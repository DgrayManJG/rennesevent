namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rue = c.String(),
                        Ville = c.String(),
                        Cp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Evenements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        Descriptif = c.String(),
                        Place = c.Int(nullable: false),
                        Adresse_Id = c.Int(),
                        Theme_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.Adresse_Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id)
                .Index(t => t.Adresse_Id)
                .Index(t => t.Theme_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        Evenement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Evenements", t => t.Evenement_Id)
                .Index(t => t.Evenement_Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Horaires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JourDebut = c.Int(nullable: false),
                        JourFin = c.Int(nullable: false),
                        HeureDebut = c.Double(nullable: false),
                        HeureFin = c.Double(nullable: false),
                        Parking_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parkings", t => t.Parking_Id)
                .Index(t => t.Parking_Id);
            
            CreateTable(
                "dbo.Tarifs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prix = c.Double(nullable: false),
                        HeureDebut = c.Double(nullable: false),
                        HeureFin = c.Double(nullable: false),
                        Tarification = c.Double(nullable: false),
                        IsFirstDay = c.Boolean(nullable: false),
                        Horaire_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Horaires", t => t.Horaire_Id)
                .Index(t => t.Horaire_Id);
            
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifiant = c.String(),
                        Nom = c.String(),
                        Statut = c.String(),
                        PlacesMax = c.Int(nullable: false),
                        PlacesLibres = c.Int(nullable: false),
                        Coordonnees = c.String(),
                        Adresse = c.String(),
                        TexteHoraires = c.String(),
                        TexteTarifs = c.String(),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Horaires", "Parking_Id", "dbo.Parkings");
            DropForeignKey("dbo.Tarifs", "Horaire_Id", "dbo.Horaires");
            DropForeignKey("dbo.Evenements", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Images", "Evenement_Id", "dbo.Evenements");
            DropForeignKey("dbo.Evenements", "Adresse_Id", "dbo.Adresses");
            DropIndex("dbo.Tarifs", new[] { "Horaire_Id" });
            DropIndex("dbo.Horaires", new[] { "Parking_Id" });
            DropIndex("dbo.Images", new[] { "Evenement_Id" });
            DropIndex("dbo.Evenements", new[] { "Theme_Id" });
            DropIndex("dbo.Evenements", new[] { "Adresse_Id" });
            DropTable("dbo.Parkings");
            DropTable("dbo.Tarifs");
            DropTable("dbo.Horaires");
            DropTable("dbo.Themes");
            DropTable("dbo.Images");
            DropTable("dbo.Evenements");
            DropTable("dbo.Adresses");
        }
    }
}
