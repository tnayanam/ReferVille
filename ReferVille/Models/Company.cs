using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ReferVille.Models
{
    public class Company
    {
        public Company()
        {
            Referrals = new Collection<Referral>();
            CoverLetters = new Collection<CoverLetter>();
        }

        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Referral> Referrals { get; set; }
        public ICollection<CoverLetter> CoverLetters { get; set; }
    }
}