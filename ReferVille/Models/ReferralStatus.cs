using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferVille.Models
{
    public class ReferralStatus
    {
        [Key]
        public int ReferralStatusId { get; set; }

        public string ReferralStatusType { get; set; }

        public ICollection<ReferralInstance> ReferralInstances { get; set; }

        public static readonly int Accept = 2;

        public static readonly int Reject = 1;
    }
}