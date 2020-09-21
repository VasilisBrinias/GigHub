namespace GigHubBC10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES (Id, Name) VALUES (1, 'Rock')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (2, 'Pop')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (3, 'Hip Hop')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (4, 'Blues')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM GENRES (Id, Name) WHERE Id (1,2,3,4) ");
        }
    }
}
