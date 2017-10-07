using Microsoft.AspNet.Identity;
using ReferVille.Models;
using System.Linq;
using System.Web.Mvc;

namespace ReferVille.Controllers
{
    [Authorize(Roles = "Candidate")]
    public class RewardsController : Controller
    {
        private ApplicationDbContext _context;

        public RewardsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Rewards
        public ActionResult SuccessfulReferrals()
        {
            var candidateId = User.Identity.GetUserId();
            ViewBag.SuccessfulReferrals = _context.Referrals
                .Where(s => s.CandidateId == candidateId
                && (s.IsReferralSuccessful)).Count();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}