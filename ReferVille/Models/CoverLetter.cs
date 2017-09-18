using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferVille.Models
{
    public class CoverLetter
    {
        [Key]
        public int CoverLetterId { get; set; }

        [Required]
        [Display(Name = "Cover Letter")]
        [StringLength(255)]
        public string CoverLetterName { get; set; }

        [StringLength(255)]
        [Required]
        public string FileName { get; set; }

        [StringLength(100)]
        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Display(Name = "Date Uploaded")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required]
        public DateTime datetime { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        public string CandidateId { get; set; }
        public virtual ApplicationUser Candidate { get; set; }

        public ICollection<Referral> Referrals { get; set; }
    }
}