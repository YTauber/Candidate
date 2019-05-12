using Candidate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate.Web
{
    public class CandidateLayoutAttribute : ActionFilterAttribute
    {
        private string _connectionString;

        public CandidateLayoutAttribute(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            CandidateRepository repo = new CandidateRepository(_connectionString);
            var controller = (Controller)context.Controller;
            controller.ViewBag.Count = repo.GetCandidateCount();
            base.OnActionExecuted(context);
        }
    }

    public enum Active
    {
        Home,
        Pending,
        Confirmed,
        Declined
    }

}
