using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Candidate.Web.Models;
using Candidate.Data;
using Microsoft.Extensions.Configuration;

namespace Candidate.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            ViewBag.Page = Active.Home;
            return View();
        }

        public IActionResult Candidates()
        {
            ViewBag.Page = Active.Pending;
            CandidateRepository repo = new CandidateRepository(_connectionString);
            CandidatesViewModel vm = new CandidatesViewModel();
            vm.Candidates = repo.GetCandidates(Status.Pending);
            vm.Message = "Pending";
            vm.Pending = true;
            return View(vm);
        }

        public IActionResult Confirmed()
        {
            ViewBag.Page = Active.Confirmed;
            CandidateRepository repo = new CandidateRepository(_connectionString);
            CandidatesViewModel vm = new CandidatesViewModel();
            vm.Candidates = repo.GetCandidates(Status.Accepted);
            vm.Message = "Confirmed";
            return View("Candidates", vm);
        }

        public IActionResult Declined()
        {
            ViewBag.Page = Active.Declined;
            CandidateRepository repo = new CandidateRepository(_connectionString);
            CandidatesViewModel vm = new CandidatesViewModel();
            vm.Candidates = repo.GetCandidates(Status.Declined);
            vm.Message = "Declined";
            return View("Candidates", vm);
        }

        public IActionResult ViewCandidate(int candidateId)
        {
            CandidateRepository repo = new CandidateRepository(_connectionString);
            CandidateViewModel vm = new CandidateViewModel();
            vm.Candidate = repo.GetCandidate(candidateId);
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddCandidate(Candidates candidate)
        {
            candidate.Status = Status.Pending;
            CandidateRepository repo = new CandidateRepository(_connectionString);
            repo.AddCandidate(candidate);
            return Redirect("/home/candidates");
        }

        [HttpPost]
        public void Update(int candidateId, Status status)
        {
            CandidateRepository repo = new CandidateRepository(_connectionString);
            repo.Update(candidateId, status);
        }

        public IActionResult GetCounts()
        {
            CandidateRepository repo = new CandidateRepository(_connectionString);
            return Json(new { Count = repo.GetCandidateCount() });
        }

    }
}
