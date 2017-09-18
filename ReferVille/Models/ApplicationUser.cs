using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReferVille.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ReferralInstances = new Collection<ReferralInstance>();
        }

        public int CompanyId { get; set; }
        public ICollection<Resume> Resumes { get; set; }
        public ICollection<CoverLetter> CoverLetters { get; set; }
        public ICollection<Referral> CandidateReferral { get; set; }
        public ICollection<ReferralInstance> ReferralInstances { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedRoleType { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}