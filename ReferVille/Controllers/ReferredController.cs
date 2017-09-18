using Microsoft.AspNet.Identity;
using ReferVille.Models;
using System.Linq;
using System.Web.Mvc;

namespace ReferVille.Controllers
{
    public class ReferredController : Controller
    {
        private ApplicationDbContext _context;

        public ReferredController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Referred
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReferredCandidates()
        {
            var referrerId = User.Identity.GetUserId();
            var successfulReferrals = _context.Referrals
                 .Where(r => (r.ReferrerId == referrerId) && (r.IsReferralSuccessful));

            return View(successfulReferrals);
        }
    }
}