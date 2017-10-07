namespace ReferVille.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class seed1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Companies (CompanyName) VALUES('Google')");
            Sql("INSERT INTO Companies (CompanyName) VALUES('Yahoo')");
            Sql("INSERT INTO Companies (CompanyName) VALUES('Cognizant')");
        }

        public override void Down()
        {
        }
    }
}
