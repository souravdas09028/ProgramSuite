using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Participant
    {
        [Key]
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTime DateOfBirth { get; init; }
        public string NationalID { get; set; }
        public Program? Program { get; init; }
    }
}
