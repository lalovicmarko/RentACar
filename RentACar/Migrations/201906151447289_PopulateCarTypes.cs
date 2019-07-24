namespace RentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCarTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.CarTypes (Id, Name) VALUES (1, N'Malo auto')");
            Sql("INSERT INTO dbo.CarTypes (Id, Name) VALUES (2, N'Porodièno auto')");
            Sql("INSERT INTO dbo.CarTypes (Id, Name) VALUES (3, N'Sportsko auto')");
            Sql("INSERT INTO dbo.CarTypes (Id, Name) VALUES (4, N'SUV')");

        }
    
        
        public override void Down()
        {
        }
    }
}
