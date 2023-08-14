namespace Application.Dtos.FaqDtos
{
    public record GetFaqItemsResponse
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
