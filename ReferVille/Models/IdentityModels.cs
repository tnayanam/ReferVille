using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ReferVille.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<CoverLetter> CoverLetters { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<ReferralInstance> ReferralInstances { get; set; }
        public DbSet<ReferralStatus> ReferralStatuses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder dBModelBuilder)
        {
            // one usr can have many resumes
            dBModelBuilder.Entity<ApplicationUser>()
            .HasMany<Resume>(u => u.Resumes) // application user has many resumes
            .WithRequired(u => u.Candidate)   // each resume required one user
            .HasForeignKey(u => u.CandidateId);
            //.WillCascadeOnDelete(false);

            //// one user can have many coverletters
            dBModelBuilder.Entity<ApplicationUser>()
                 .HasMany<CoverLetter>(u => u.CoverLetters) // application user has many coveletter
                 .WithRequired(u => u.Candidate)   // each coverlletter required one user
                 .HasForeignKey(u => u.CandidateId);
            //     .WillCascadeOnDelete(false);

            //// one cover letter can be there in many referrals
            dBModelBuilder.Entity<CoverLetter>()
                .HasMany(u => u.Referrals)
                .WithOptional(u => u.CoverLetter)
                .HasForeignKey(u => u.CoverLetterId);

            // one resume can be there in many referrals
            dBModelBuilder.Entity<Resume>()
                .HasMany<Referral>(u => u.Refferals)
                .WithRequired(u => u.Resume)
                .HasForeignKey(u => u.ResumeId)
                 // we want all the referrals also to be deleted if the res
                 .WillCascadeOnDelete(true);

            // one referral has one company but one company can have many referrals
            dBModelBuilder.Entity<Company>()
                .HasMany(u => u.Referrals)
                .WithRequired(u => u.Company)
                .HasForeignKey(u => u.CompanyId);
            //.WillCascadeOnDelete(false);

            // one candidate can have many referrals
            dBModelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.CandidateReferral)
            .WithRequired(u => u.Candidate)
            .HasForeignKey(u => u.CandidateId)
            .WillCascadeOnDelete(false);

            ////one referrer can have many referrals
            //dBModelBuilder.Entity<ApplicationUser>()
            //.HasMany(u => u.ReferrerReferral)
            //.WithOptional(u => u.Referrer)
            //.HasForeignKey(u => u.ReferrerId)
            //.WillCascadeOnDelete(false);

            // one referral can be there in referral instances
            // but if a referral instasnce exist then it must be linked to referral
            dBModelBuilder.Entity<Referral>()
                  .HasMany(r => r.ReferralInstances)
                  .WithRequired(r => r.Referral)
                  .HasForeignKey(r => r.ReferralId);

            // one referrer/user can be there in many referralinstances,
            // but if an instance exist it must be tied to a referrer.
            dBModelBuilder.Entity<ApplicationUser>()
                 .HasMany(r => r.ReferralInstances)
                 .WithRequired(r => r.Referrer)
                 .HasForeignKey(r => r.ReferrerId).
                 WillCascadeOnDelete(false);

            // one company can have many coverletters, 
            //one cover letter can belong to just one company
            dBModelBuilder.Entity<Company>()
                .HasMany(c => c.CoverLetters)
                .WithRequired(c => c.Company)
                .HasForeignKey(c => c.CompanyId).
                WillCascadeOnDelete(false);

            // one skills can be there in many referrals
            // one referral must have one skill
            dBModelBuilder.Entity<Skill>()
                .HasMany(r => r.Referrals)
                .WithRequired(s => s.Skill)
                .HasForeignKey(s => s.SkillId);

            dBModelBuilder.Entity<ReferralStatus>()
                .HasMany(r => r.ReferralInstances)
                .WithRequired(r => r.ReferralStatus)
                .HasForeignKey(r => r.ReferralStatusId);

            // one referal can have one or zero referrer.
            dBModelBuilder.Entity<Referral>()
             .HasOptional(a => a.Referrer)
             .WithMany()
             .HasForeignKey(a => a.ReferrerId);

            base.OnModelCreating(dBModelBuilder);
        }
    }
}