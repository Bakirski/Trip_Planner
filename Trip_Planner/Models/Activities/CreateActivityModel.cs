using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models.Activities
{
    public class CreateActivityModel
    {
        [Required]
        public string ActivityName { get; set; }
    }
}
