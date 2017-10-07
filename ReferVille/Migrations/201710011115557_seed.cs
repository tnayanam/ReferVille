namespace ReferVille.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class seed : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Degrees (DegreeName) VALUES('Bachelors')");
            Sql("INSERT INTO Degrees (DegreeName) VALUES('Masters')");
        }

        public override void Down()
        {
        }
    }
}
