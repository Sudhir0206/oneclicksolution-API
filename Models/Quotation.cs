using System.ComponentModel.DataAnnotations;

namespace OneClickSolution.Models
{
    public class Quotation
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public string? CompanyName { get; set; }
        public string? ProductRequirements { get; set; }
        public string? Website { get; set; }
        public string? ExactRequirements { get; set; }
    }
}
