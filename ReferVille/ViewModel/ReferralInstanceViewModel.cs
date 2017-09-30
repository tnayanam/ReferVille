using Foolproof;
using ReferVille.CustomValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ReferVille.ViewModels
{
    public class ReferralInstanceViewModel
    {
        [Key]
        public int Id { get; set; }

        [FileType("pdf|doc|docx|PDF|jpg|jpeg|JPG|jpeg", ErrorMessage = "File type is not valid.")]
        [RequiredIf("ReferralStatusId", "2", ErrorMessage = "File is required.")]
        public HttpPostedFileBase ProofDoc { get; set; }

        [Required]
        public int ReferralId { get; set; }

        [Required]
        [Display(Name ="Application Status")]
        public int? ReferralStatusId { get; set; }
        public IEnumerable<SelectListItem> ReferralStatuses { get; set; }

    }
}