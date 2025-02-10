using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models
{
    public class CreateDestinationModel
    {
        [Required]
        public string DestinationName { get; set; }
    }
}
