using Microsoft.AspNet.Identity;
using ReferVille.Models;
using ReferVille.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using ReferVille.Helpers;

namespace ReferVille.Controllers
{
    [Authorize(Roles = "Candidate")]
    public class CoverLetterController : Controller
    {
        private ApplicationDbContext _context;

        public CoverLetterController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Resume
        public ActionResult CoverLetterCenter()
        {
            var candidateId = User.Identity.GetUserId();
            var coverLetterList = _context.CoverLetters
                .Include(r => r.Company)
                .Where(r => r.CandidateId == candidateId).OrderByDescending(r => r.datetime);

            return View(coverLetterList);
        }

        public FileContentResult Details(int? coverLetterId)
        {
            var coverLetter = _context.CoverLetters
                .Where(f => f.CoverLetterId == coverLetterId).Single();
            var fileRes = new FileContentResult(coverLetter.Content.ToArray(), coverLetter.ContentType);
            fileRes.FileDownloadName = coverLetter.FileName;

            return fileRes;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int coverLetterId)
        {
            var r = _context.CoverLetters
                .Where(c => c.CoverLetterId == coverLetterId).Single();
            _context.CoverLetters.Remove(r);
            _context.SaveChanges();

            return RedirectToAction("CoverLetterCenter");
        }

        private void ConfigureViewModel(CoverLetterViewModel viewModel)
        {
            var companies = _context.Companies.Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.CompanyId.ToString()
            }).ToList();
            companies.Insert(0, new SelectListItem { Value = "0", Text = "Other" });
            viewModel.Companies = companies;
        }

        public ActionResult UploadCoverLetter()
        {
            var viewModel = new CoverLetterViewModel();
            ConfigureViewModel(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCoverLetter(CoverLetterViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.UploadedCoverLetter != null && viewModel.UploadedCoverLetter.ContentLength > 0)
                    {
                        var tempcoverletter = new CoverLetter
                        {
                            FileName = System.IO.Path.GetFileName(viewModel.UploadedCoverLetter.FileName),
                            ContentType = viewModel.UploadedCoverLetter.ContentType,
                            CoverLetterName = viewModel.CoverLetterName,
                            CandidateId = User.Identity.GetUserId(),
                            datetime = System.DateTime.Now

                        };
                        if (!string.IsNullOrEmpty(viewModel.TempCompany))
                        {
                            var tempCompany = new Company { CompanyName = Utils.MakeFirstLetterCaps(viewModel.TempCompany) };
                            using (var reader = new System.IO.BinaryReader(viewModel.UploadedCoverLetter.InputStream))
                            {
                                tempcoverletter.Content = reader.ReadBytes(viewModel.UploadedCoverLetter.ContentLength);
                            }
                            tempCompany.CoverLetters.Add(tempcoverletter);
                            _context.Companies.Add(tempCompany);
                        }
                        else
                        {
                            tempcoverletter.CompanyId = viewModel.CompanyId.Value;
                            using (var reader = new System.IO.BinaryReader(viewModel.UploadedCoverLetter.InputStream))
                            {
                                tempcoverletter.Content = reader.ReadBytes(viewModel.UploadedCoverLetter.ContentLength);
                            }
                            _context.CoverLetters.Add(tempcoverletter);
                        }
                    }
                    _context.SaveChanges();

                    return RedirectToAction("CoverLetterCenter");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ConfigureViewModel(viewModel);
            return View(viewModel);
        }
    }
}