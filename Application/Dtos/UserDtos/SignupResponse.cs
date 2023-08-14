namespace Application.Dtos.UserDtos
{
    public record SignupResponse
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
