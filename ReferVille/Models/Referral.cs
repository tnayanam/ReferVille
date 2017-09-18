using Foolproof;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ReferVille.Models
{
    public class Referral
    {
        public Referral()
        {
            ReferralInstances = new Collection<ReferralInstance>();
        }

        [Key]
        public int ReferralId { get; set; }

        public string ReferralName { get; set; }

        public DateTime dateTime { get; set; }

        public bool IsReferralSuccessful { get; set; } = false;

        //docment proof related stuff
        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        [RequiredIf("IsReferralSuccessful", "1")]
        public string ReferrerId { get; set; }
        public virtual ApplicationUser Referrer { get; set; }

        [Required]
        public int DegreeId { get; set; }
        public virtual Degree Degree { get; set; }

        [Required]
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        public int? CoverLetterId { get; set; }
        public virtual CoverLetter CoverLetter { get; set; }

        [Required]
        public int ResumeId { get; set; }
        public virtual Resume Resume { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public string CandidateId { get; set; }
        public virtual ApplicationUser Candidate { get; set; }

        public ICollection<ReferralInstance> ReferralInstances { get; set; }
    }
}