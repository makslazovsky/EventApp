namespace Application.DTOs
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = default!;
        public DateTime RegistrationDate { get; set; }
    }
}
