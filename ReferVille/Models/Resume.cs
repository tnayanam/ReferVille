using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferVille.Models
{
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }

        [Required]
        [Display(Name = "RESUME NAME")]
        [StringLength(255)]
        public string ResumeName { get; set; }

        [StringLength(255)]
        [Required]
        public string FileName { get; set; }

        [StringLength(100)]
        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }

        //public HttpPostedFileBase Filess { get; set; }

        [Display(Name = "Date Uploaded")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required]
        public DateTime datetime { get; set; }

        [Required]
        public string CandidateId { get; set; }
        public virtual ApplicationUser Candidate { get; set; }

        public ICollection<Referral> Refferals { get; set; }

    }
}