using System;

namespace Candidate.Data
{
    public class Candidates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Notes { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Pending,
        Accepted,
        Declined
    }

    public class CandidateCount
    {
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
    }
}
