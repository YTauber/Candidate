using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Candidate.Data
{
    public class CandidateRepository
    {
        private string _connectionString;

        public CandidateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddCandidate(Candidates candidate)
        {
            using (var context = new CandidateContext(_connectionString))
            {
                context.Candidates.Add(candidate);
                context.SaveChanges();
                return candidate.Id;
            }
        }

        public IEnumerable<Candidates> GetCandidates(Status status)
        {
            using (var context = new CandidateContext(_connectionString))
            {
                return context.Candidates.Where(c => c.Status == status).ToList();
            }
        }

        public CandidateCount GetCandidateCount()
        {
            using (var context = new CandidateContext(_connectionString))
            {
                return new CandidateCount
                {
                    PendingCount = context.Candidates.Where(c => c.Status == Status.Pending).Count(),
                    ConfirmedCount = context.Candidates.Where(c => c.Status == Status.Accepted).Count(),
                    DeclinedCount = context.Candidates.Where(c => c.Status == Status.Declined).Count()
                };
            }
        }

        public Candidates GetCandidate(int candidateId)
        {
            using (var context = new CandidateContext(_connectionString))
            {
                return context.Candidates.FirstOrDefault(c => c.Id == candidateId);
            }
        }

        public void Confirm(int candidateId)
        {
            using (var context = new CandidateContext(_connectionString))
            {
                Candidates c = context.Candidates.FirstOrDefault(ca => ca.Id == candidateId);
                c.Status = Status.Accepted;
                context.Candidates.Attach(c);
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Decline(int candidateId)
        {
            using (var context = new CandidateContext(_connectionString))
            {
                Candidates c = context.Candidates.FirstOrDefault(ca => ca.Id == candidateId);
                c.Status = Status.Declined;
                context.Candidates.Attach(c);
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Update(int candidateId, Status status)
        {
            using (var context = new CandidateContext(_connectionString))
            {
                Candidates c = context.Candidates.FirstOrDefault(ca => ca.Id == candidateId);
                c.Status = status;
                context.Candidates.Attach(c);
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
