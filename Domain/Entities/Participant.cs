namespace Domain.Entities
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;


        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
