using System.Text;

namespace ReferVille.ViewModels
{
    public class SmallButtonViewModel
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
        public int? ReferralId { get; set; }
        public int? ResumeId { get; set; }
        public int? CoverLetterId { get; set; }
        public string CandidateId { get; set; }
        public int? SubscriptionId { get; set; }
        public string Title { get; set; }
        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder("?");
                if (ReferralId != null & ReferralId > 0)
                    param.Append(string.Format("{0}={1}&", "referralId", ReferralId));

                if (ResumeId != null & ResumeId > 0)
                    param.Append(string.Format("{0}={1}&", "resumeId", ResumeId));


                if (CoverLetterId != null & CoverLetterId > 0)
                    param.Append(string.Format("{0}={1}&", "coverLetterId", CoverLetterId));


                if (SubscriptionId != null & SubscriptionId > 0)
                    param.Append(string.Format("{0}={1}&", "subscriptionId", SubscriptionId));

                if (CandidateId != null)
                    param.Append(string.Format("{0}={1}&", "candidateId", CandidateId));

                return param.ToString().Substring(0, param.Length - 1);
            }
        }
    }
}