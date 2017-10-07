using Microsoft.AspNet.Identity;
using ReferVille.Models;
using ReferVille.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace ReferVille.Controllers
{
    [Authorize(Roles = "Referrer")]
    public class ReferrerController : Controller
    {
        private ApplicationDbContext _context;

        public ReferrerController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ReferrerCenter()
        {
            var referrerId = User.Identity.GetUserId();
            var companyId = _context.Users
                .Where(r => r.Id == referrerId)
                .Select(r => r.CompanyId).Single();

            var referrals = _context.Referrals
                .Where(r => ((r.CompanyId == companyId)
                && (!r.IsReferralSuccessful))
                && !r.ReferralInstances.Any(e => (e.ReferrerId == referrerId) && (e.ReferralStatusId == ReferralStatus.Reject)))
               .Include("Company")
               .Include("CoverLetter")
               .Include("Resume")
               .Include("Degree")
               .Include("Skill")
               .Include("Candidate").OrderByDescending(r => r.dateTime);

            return View(referrals);
        }

        private void ConfigureViewModel(ReferralInstanceViewModel viewModel)
        {
            viewModel.ReferralStatuses = _context.ReferralStatuses.Select(x => new SelectListItem
            {
                Text = x.ReferralStatusType,
                Value = x.ReferralStatusId.ToString()
            });
        }

        public ActionResult ReferralConfirmation(int referralId)
        {
            var viewModel = new ReferralInstanceViewModel
            {
                ReferralId = referralId
            };
            ConfigureViewModel(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReferralConfirmation(ReferralInstanceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ConfigureViewModel(viewModel);
                return View(viewModel);
            }

            var referrerId = User.Identity.GetUserId();
            var referral = _context.Referrals
                .Where(r => r.ReferralId == viewModel.ReferralId).Single();

            var instance = new ReferralInstance();
            if (viewModel.ReferralStatusId == ReferralStatus.Reject)
            {
                instance.ReferrerId = referrerId;
                instance.ReferralId = viewModel.ReferralId;
                instance.ReferralStatusId = ReferralStatus.Reject;
            }
            else if (viewModel.ReferralStatusId == ReferralStatus.Accept)
            {
                if (viewModel.ProofDoc != null && viewModel.ProofDoc.ContentLength > 0)
                {
                    referral.FileName = System.IO.Path.GetFileName(viewModel.ProofDoc.FileName);
                    referral.ContentType = viewModel.ProofDoc.ContentType;
                    instance.ReferrerId = referrerId;
                    instance.ReferralId = referral.ReferralId;
                    instance.ReferralStatusId = ReferralStatus.Accept;
                    using (var reader = new System.IO.BinaryReader(viewModel.ProofDoc.InputStream))
                    {
                        referral.Content = reader.ReadBytes(viewModel.ProofDoc.ContentLength);
                    }
                }
                referral.IsReferralSuccessful = true;
                referral.ReferrerId = referrerId;
            }
            else
                return RedirectToAction("ReferrerCenter");

            _context.ReferralInstances.Add(instance);
            _context.SaveChanges();
            return RedirectToAction("ReferrerCenter");
        }

        public ActionResult Details(string candidateId)
        {
            var candidate = _context.Users
                .Where(u => u.Id == candidateId).SingleOrDefault();
            return View(candidate);
        }

        // Canceling of the application by a referrer.
        [HttpPost]
        public JsonResult Cancel(int referralId)
        {
            var referrerId = User.Identity.GetUserId();
            // add the instance in the referrer table
            var instance = new ReferralInstance();
            instance.ReferrerId = referrerId;
            instance.ReferralId = referralId;
            // set the status to rejected
            instance.ReferralStatusId = ReferralStatus.Reject;
            _context.ReferralInstances.Add(instance);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
