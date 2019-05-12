using Candidate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate.Web.Models
{
    public class CandidatesViewModel
    {
        public string Message { get; set; }
        public bool Pending { get; set; }
        public IEnumerable<Candidates> Candidates { get; set; }
    }

    public class CandidateViewModel
    {
        public Candidates Candidate { get; set; }
    }
}
