using Microsoft.AspNetCore.Mvc;
using TechJobPortal.Models;
using System.Collections.Generic;
using System.Linq;

namespace TechJobPortal.Controllers
{
    public class JobController : Controller
    {
        //job listings data 
        private static List<JobListing> jobListings = new List<JobListing>
        {
            // Sample job listings
            new JobListing { Id = 1, Title = "Software Engineer", CompanyName = "Tech Corp", Location = "New York", Type = JobType.FullTime, PostedDate = DateTime.Now.AddDays(-10) },
            new JobListing { Id = 2, Title = "Project Manager", CompanyName = "Business Solutions", Location = "San Francisco", Type = JobType.Contract, PostedDate = DateTime.Now.AddDays(-5) },
            new JobListing { Id = 3, Title = "Data Analyst", CompanyName = "Data Inc", Location = "Chicago", Type = JobType.PartTime, PostedDate = DateTime.Now.AddDays(-8) },
            new JobListing { Id = 4, Title = "Web Developer", CompanyName = "Web Solutions", Location = "Los Angeles", Type = JobType.Remote, PostedDate = DateTime.Now.AddDays(-3) },
            new JobListing { Id = 5, Title = "System Administrator", CompanyName = "IT Services", Location = "Houston", Type = JobType.FullTime, PostedDate = DateTime.Now.AddDays(-1) }
        };

        // Display the list of job listings with filtering, sorting, and searching
        public IActionResult Index(JobType? jobType, string sortOrder, string searchString)
        {
            // Filter job listings based on job type and search string
            var filteredJobListings = jobListings.AsQueryable();

            if (jobType.HasValue)
            {
                filteredJobListings = filteredJobListings.Where(j => j.Type == jobType.Value); // Filter by job type
            }

            
            if (!string.IsNullOrEmpty(searchString))
            {
                filteredJobListings = filteredJobListings.Where(j => j.Title.Contains(searchString) || j.CompanyName.Contains(searchString)); // Search by title or company names
            }

            filteredJobListings = sortOrder switch  // Sort job listings based on sort order
            {
                "date_asc" => filteredJobListings.OrderBy(j => j.PostedDate), // Sort by date in ascending order
                "date_desc" => filteredJobListings.OrderByDescending(j => j.PostedDate), // Sort by date in descending order
                _ => filteredJobListings
            };

            ViewBag.JobType = jobType;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchString = searchString;
            return View(filteredJobListings.ToList());
        }

        // Display the details of a selected job
        public IActionResult Details(int id) 
        {
            var jobListing = jobListings.FirstOrDefault(j => j.Id == id); // Find the job listing by id
            if (jobListing == null)
            {
                return NotFound(); // Return 404 Not Found if job listing is not found
            }
            return View(jobListing);
        }
    }
}




