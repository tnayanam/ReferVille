using Microsoft.AspNet.Identity;
using ReferVille.Models;
using ReferVille.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using ReferVille.Helpers;

namespace ReferVille.Controllers
{
    [Authorize(Roles = "Candidate")]
    public class ReferralController : Controller
    {
        private ApplicationDbContext _context;

        public ReferralController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ReferralCenter()
        {
            var candidateId = User.Identity.GetUserId();
            // it will show only the pending ones.
            var viewModel = _context.Referrals
                .Include("Company")
                .Include("Resume")
                .Include("CoverLetter")
                .Include("Degree")
                .Where(r => r.CandidateId == candidateId).OrderByDescending(r => r.dateTime);

            return View(viewModel);
        }

        private void ConfigureViewModel(ReferralViewModel model)
        {
            var candidateId = User.Identity.GetUserId();
            var companies = _context.Companies.Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.CompanyId.ToString()
            }).ToList();
            companies.Insert(0, new SelectListItem { Value = "0", Text = "Other" });
            model.Companies = companies;
            model.Degrees = _context.Degrees.Select(r => new SelectListItem
            {
                Text = r.DegreeName,
                Value = r.DegreeId.ToString()
            });
            model.Skills = _context.Skills.Select(r => new SelectListItem
            {
                Text = r.SkillName,
                Value = r.SkillId.ToString()
            });
            model.Resumes = _context.Resumes.Where(r => r.CandidateId == candidateId).Select(r => new SelectListItem
            {
                Text = r.ResumeName,
                Value = r.ResumeId.ToString()
            });

            if (model.Resumes.Count() > 0)
            {
                model.IsResumeExists = true;
            }

            if (model.CompanyId.HasValue)
            {
                model.CoverLetters = _context.CoverLetters.Where(r => r.CandidateId == candidateId && r.CompanyId == model.CompanyId).Select(c => new SelectListItem
                {
                    Text = c.CoverLetterName,
                    Value = c.CoverLetterId.ToString()
                });
            }
            else
            {
                model.CoverLetters = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public ActionResult AddReferral()
        {
            ReferralViewModel viewModel = new ReferralViewModel();
            ConfigureViewModel(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReferral(ReferralViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ConfigureViewModel(viewModel);
                return View(viewModel);
            }

            var candidateId = User.Identity.GetUserId();
            var referral = _context.Referrals
                   .Where(r => ((r.CandidateId == candidateId)
                    && (r.CompanyId == viewModel.CompanyId)
                    && (r.SkillId == viewModel.SkillId) && (!r.IsReferralSuccessful))).SingleOrDefault();
            // create a new referral if there is not matched referreal entry ins found, or if found then that referral has already been accepted by some Referrer
            if (referral == null)
            {
                referral = new Referral
                {
                    ReferralName = viewModel.ReferralName,
                    ResumeId = viewModel.ResumeId,
                    CandidateId = candidateId,
                    DegreeId = viewModel.DegreeId.Value,
                    CoverLetterId = viewModel.CoverLetterId,
                    SkillId = viewModel.SkillId.Value,
                    dateTime = DateTime.Now
                };
                if (!string.IsNullOrEmpty(viewModel.TempCompany))
                {
                    var TempCompany = Utils.MakeFirstLetterCaps(viewModel.TempCompany);
                    var companyId = _context.Companies
                        .Where(c => c.CompanyName == TempCompany).Select(c => c.CompanyId).SingleOrDefault();
                    if (companyId != 0)
                    {
                        referral.CompanyId = companyId;
                        _context.Referrals.Add(referral);
                    }
                    else
                    {
                        var newCompany = new Company
                        {
                            CompanyName = Utils.MakeFirstLetterCaps(viewModel.TempCompany)
                        };
                        newCompany.Referrals.Add(referral);
                        _context.Companies.Add(newCompany);
                    }

                }
                else
                {
                    referral.CompanyId = viewModel.CompanyId.Value;
                    _context.Referrals.Add(referral);
                }
            }
            else
            {
                referral.ResumeId = viewModel.ResumeId;
                referral.CoverLetterId = viewModel.CoverLetterId;
                referral.dateTime = DateTime.Now;
                referral.ReferralName = viewModel.ReferralName;
            }
            _context.SaveChanges();

            return RedirectToAction("ReferralCenter");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int referralId)
        {
            var referralToBeDeleted = _context.Referrals.Where(d => d.ReferralId == referralId).Single();
            _context.Referrals.Remove(referralToBeDeleted);
            _context.SaveChanges();

            return RedirectToAction("ReferralCenter");
        }

        [HttpPost]
        public JsonResult ListOfCoverLetterByCompanyId(int companyId)
        {
            var candidateId = User.Identity.GetUserId();
            var coverletters = _context.CoverLetters
                .Where(c => (c.CompanyId == companyId) && (c.CandidateId == candidateId)).Select(c => new
                {
                    Value = c.CoverLetterId.ToString(),
                    Text = c.CoverLetterName
                });

            return Json(coverletters);
        }

        [HttpPost]
        public JsonResult CheckForExistingReferral(ReferralViewModel viewModel)
        {
            bool hasPreviousRequest = false;
            var candidateId = User.Identity.GetUserId();

            // if successful matching referral exists with isreferralsuccessful as true.
            if (_context.Referrals
                .Any(r => ((r.CandidateId == candidateId)
                               && (r.CompanyId == viewModel.CompanyId)
                               && (r.SkillId == viewModel.SkillId) && r.IsReferralSuccessful)))
            {
                // if successul referral status exists but there is more entry like simliar to that with "not succcess status" so again it'sa dupe
                if ((_context.Referrals
                .Any(r => ((r.CandidateId == candidateId)
                               && (r.CompanyId == viewModel.CompanyId)
                               && (r.SkillId == viewModel.SkillId) && !r.IsReferralSuccessful))))
                    hasPreviousRequest = true;
                else
                    //all the matched entry as success as true and no matched entry with "Not success exists" that means mark it as not duplicate.
                    hasPreviousRequest = false;
            }
            // if everything matches but no success entry then overwrite
            else if (_context.Referrals
                .Any(r => ((r.CandidateId == candidateId)
                               && (r.CompanyId == viewModel.CompanyId)
                               && (r.SkillId == viewModel.SkillId))))
                hasPreviousRequest = true;
            else
                hasPreviousRequest = false;

            return Json(new { hasPreviousRequest = hasPreviousRequest });
        }

    }
}