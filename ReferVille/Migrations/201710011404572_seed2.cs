namespace ReferVille.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class seed2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Skills (SkillName) VALUES('C++')");
            Sql("INSERT INTO Skills (SkillName) VALUES('Java')");
            Sql("INSERT INTO Skills (SkillName) VALUES('Javascript')");
        }

        public override void Down()
        {
        }
    }
}
