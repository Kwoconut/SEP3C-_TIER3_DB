using System.ComponentModel.DataAnnotations;

namespace SEP3_TIER3.Model
{
    public class Plane
    {
        [Key]
        [Required]
        [StringLength(10, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string RegistrationNo { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Model { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Company { get; set; }

        [Required]
        public FlightPlan FlightPlan { get; set; }

        [Required]
        public Position Position { get; set; }

        public string Status { get; set; } = "In air";

        public override string ToString()
        {
            return $"RegNo: {RegistrationNo} \nModel:{Model} \nCompany: {Company} \nFlightPlan: {FlightPlan} \nPosition: {Position} \nStatus: {Status}";
        }
    }
}