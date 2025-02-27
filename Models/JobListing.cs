using System.ComponentModel.DataAnnotations;

namespace TechJobPortal.Models
{
    public class JobListing
    {
        // Properties of a job listing
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string Location { get; set; }

        [Required]
        public JobType Type { get; set; }

        public DateTime PostedDate { get; set; }
    }

    public enum JobType
    {
        FullTime,
        PartTime,
        Remote,
        Contract
    }
}