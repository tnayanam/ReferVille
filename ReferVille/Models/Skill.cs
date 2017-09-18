using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferVille.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public ICollection<Referral> Referrals { get; set; }
    }
}