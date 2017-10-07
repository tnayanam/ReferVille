using ReferVille.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ReferVille.Controllers.Api
{
    public class ResumesController : ApiController
    {
        private ApplicationDbContext _context;

        public ResumesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/resumes
        public IEnumerable<Degree> GetResumes()
        {
            //var candidateId = User.Identity.GetUserId();
            //var resumeList = _context
            //    .Resumes.Where(r => r.CandidateId == candidateId)
            //    .OrderByDescending(r => r.datetime).ToList();
            return _context.Degrees.ToList();
            //return resumeList;
        }

    }
}
