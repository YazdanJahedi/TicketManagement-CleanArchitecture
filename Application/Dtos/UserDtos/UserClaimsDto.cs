namespace Application.Dtos.UserDtos
{
    public record UserClaimsDto
    {
        public required long Id { get; set; }
        public required string Role { get; set; }
    }
}
