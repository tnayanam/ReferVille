using Foolproof;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReferVille.ViewModels
{
    public class ReferralViewModel
    {
        public int Id { get; set; }

        public bool IsResumeExists { get; set; }

        public bool IsReferralStatus { get; set; }

        public bool IsCoverLetterExists { get; set; }

        [RequiredIf("CompanyId", "0", ErrorMessage = "Enter Company Name")]
        [Display(Name = "Other Company")]
        public string TempCompany { get; set; }

        [Display(Name = "Experience(months)")]
        [Range(0, int.MaxValue, ErrorMessage = "Experience should be greater than or equal to 0.")]
        public int Experience { get; set; }

        [Display(Name = "Referral Name")]
        [Required]
        public string ReferralName { get; set; }

        [Display(Name = "Resume")]
        [Required]
        public int ResumeId { get; set; }
        public IEnumerable<SelectListItem> Resumes { get; set; }

        [Display(Name = "Degree")]
        [Required]
        public int? DegreeId { get; set; }
        public IEnumerable<SelectListItem> Degrees { get; set; }

        [Display(Name = "Skill")]
        [Required]
        public int? SkillId { get; set; }
        public IEnumerable<SelectListItem> Skills { get; set; }

        // preventing from underposting attack
        [Display(Name = "Company Name")]
        [Required]
        public int? CompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }

        // needs to have "?" because when no dropdown is selected we want to pass NULL values
        // question marks makes sure no default value is set by the framework.
        [Display(Name = "Cover Letter")]
        public int? CoverLetterId { get; set; }
        public IEnumerable<SelectListItem> CoverLetters { get; set; }
    }
}