using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models.Destinations
{
    public class CreateDestinationModel
    {
        [Required]
        public string DestinationName { get; set; }
    }
}
