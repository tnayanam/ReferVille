namespace ReferVille.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class seed3 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ReferralStatus (ReferralStatusType) VALUES('Reject')");
            Sql("INSERT INTO ReferralStatus (ReferralStatusType) VALUES('Accept')");
        }

        public override void Down()
        {
        }
    }
}
