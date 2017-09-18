namespace ReferVille.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullModelMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.CoverLetters",
                c => new
                    {
                        CoverLetterId = c.Int(nullable: false, identity: true),
                        CoverLetterName = c.String(nullable: false, maxLength: 255),
                        FileName = c.String(nullable: false, maxLength: 255),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(nullable: false),
                        datetime = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CandidateId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CoverLetterId)
                .ForeignKey("dbo.AspNetUsers", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        SelectedRoleType = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Referrals",
                c => new
                    {
                        ReferralId = c.Int(nullable: false, identity: true),
                        ReferralName = c.String(),
                        dateTime = c.DateTime(nullable: false),
                        IsReferralSuccessful = c.Boolean(nullable: false),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        ReferrerId = c.String(maxLength: 128),
                        DegreeId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        CoverLetterId = c.Int(),
                        ResumeId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CandidateId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ReferralId)
                .ForeignKey("dbo.Degrees", t => t.DegreeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ReferrerId)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.CandidateId)
                .ForeignKey("dbo.CoverLetters", t => t.CoverLetterId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.ReferrerId)
                .Index(t => t.DegreeId)
                .Index(t => t.SkillId)
                .Index(t => t.CoverLetterId)
                .Index(t => t.ResumeId)
                .Index(t => t.CompanyId)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.Degrees",
                c => new
                    {
                        DegreeId = c.Int(nullable: false, identity: true),
                        DegreeName = c.String(),
                    })
                .PrimaryKey(t => t.DegreeId);
            
            CreateTable(
                "dbo.ReferralInstances",
                c => new
                    {
                        ReferralInstanceId = c.Int(nullable: false, identity: true),
                        ReferralId = c.Int(nullable: false),
                        ReferrerId = c.String(nullable: false, maxLength: 128),
                        ReferralStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReferralInstanceId)
                .ForeignKey("dbo.ReferralStatus", t => t.ReferralStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Referrals", t => t.ReferralId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ReferrerId)
                .Index(t => t.ReferralId)
                .Index(t => t.ReferrerId)
                .Index(t => t.ReferralStatusId);
            
            CreateTable(
                "dbo.ReferralStatus",
                c => new
                    {
                        ReferralStatusId = c.Int(nullable: false, identity: true),
                        ReferralStatusType = c.String(),
                    })
                .PrimaryKey(t => t.ReferralStatusId);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        ResumeId = c.Int(nullable: false, identity: true),
                        ResumeName = c.String(nullable: false, maxLength: 255),
                        FileName = c.String(nullable: false, maxLength: 255),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(nullable: false),
                        datetime = c.DateTime(nullable: false),
                        CandidateId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ResumeId)
                .ForeignKey("dbo.AspNetUsers", t => t.CandidateId, cascadeDelete: true)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Referrals", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CoverLetters", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Referrals", "CoverLetterId", "dbo.CoverLetters");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Resumes", "CandidateId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReferralInstances", "ReferrerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CoverLetters", "CandidateId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Referrals", "CandidateId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Referrals", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.Referrals", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Referrals", "ReferrerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReferralInstances", "ReferralId", "dbo.Referrals");
            DropForeignKey("dbo.ReferralInstances", "ReferralStatusId", "dbo.ReferralStatus");
            DropForeignKey("dbo.Referrals", "DegreeId", "dbo.Degrees");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Resumes", new[] { "CandidateId" });
            DropIndex("dbo.ReferralInstances", new[] { "ReferralStatusId" });
            DropIndex("dbo.ReferralInstances", new[] { "ReferrerId" });
            DropIndex("dbo.ReferralInstances", new[] { "ReferralId" });
            DropIndex("dbo.Referrals", new[] { "CandidateId" });
            DropIndex("dbo.Referrals", new[] { "CompanyId" });
            DropIndex("dbo.Referrals", new[] { "ResumeId" });
            DropIndex("dbo.Referrals", new[] { "CoverLetterId" });
            DropIndex("dbo.Referrals", new[] { "SkillId" });
            DropIndex("dbo.Referrals", new[] { "DegreeId" });
            DropIndex("dbo.Referrals", new[] { "ReferrerId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CoverLetters", new[] { "CandidateId" });
            DropIndex("dbo.CoverLetters", new[] { "CompanyId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Skills");
            DropTable("dbo.Resumes");
            DropTable("dbo.ReferralStatus");
            DropTable("dbo.ReferralInstances");
            DropTable("dbo.Degrees");
            DropTable("dbo.Referrals");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CoverLetters");
            DropTable("dbo.Companies");
        }
    }
}
