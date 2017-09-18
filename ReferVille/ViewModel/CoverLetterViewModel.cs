using ReferVille.CustomValidation;
using Foolproof;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ReferVille.ViewModels
{
    public class CoverLetterViewModel
    {
        public IEnumerable<SelectListItem> Companies { get; set; }
        [Required]
        public int? CompanyId { get; set; }

        [RequiredIf("CompanyId", "0", ErrorMessage = "Company Name is required")]
        [Display(Name = "Other Company")]
        public string TempCompany { get; set; }

        [Required(ErrorMessage = "Enter Cover Letter Name.")]
        [Display(Name = "CoverLetter Name")]
        public string CoverLetterName { get; set; }

        [FileType("pdf|doc|docx|PDF", ErrorMessage = "File type is not valid.")]
        [Required]
        [Display(Name = "Upload CoverLetter")]
        public HttpPostedFileBase UploadedCoverLetter { get; set; }
    }
}