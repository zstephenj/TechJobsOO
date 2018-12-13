using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job job = jobData.Find(id);
            return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid) 
            {
                Employer newEmployer = jobData.Employers.ToList().Find(x => x.ID == newJobViewModel.EmployerID);
                Location newLocation = jobData.Locations.ToList().Find(x => x.ID == newJobViewModel.LocationsID);
                CoreCompetency newCoreCompetency = jobData.CoreCompetencies.ToList().Find(x => x.ID == newJobViewModel.CoreCompetenciesID);
                PositionType newPositionType = jobData.PositionTypes.ToList().Find(x => x.ID == newJobViewModel.PositionTypesID);
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = newEmployer,
                    Location = newLocation,
                    CoreCompetency = newCoreCompetency,
                    PositionType = newPositionType

                };
                jobData.Jobs.Add(newJob);
                return RedirectToAction("Index", "Job", new { id = newJob.ID });
            }

            return View(newJobViewModel);
        }
    }
}
