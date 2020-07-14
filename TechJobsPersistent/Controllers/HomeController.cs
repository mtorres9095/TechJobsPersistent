using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> allEmployers = context.Employers.ToList();
            List<Skill> AllSkills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(allEmployers, AllSkills);
            return View(addJobViewModel);
            
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = addJobViewModel.JobName,
                    Employer = context.Employers.Find(addJobViewModel.EmployerId)
                };
                
                foreach (var skill in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill

                    {
                        JobId = newJob.Id,
                        Job = newJob,
                        SkillId = int.Parse(skill),
                        Skill = context.Skills.Find(int.Parse(skill))
                    };
                    context.JobSkills.Add(jobSkill);
                    
                }
                context.Jobs.Add(newJob);
                context.SaveChanges();
                return Redirect("/Home");
            }

            return View("Add", addJobViewModel);
        }
        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }

    }
}
