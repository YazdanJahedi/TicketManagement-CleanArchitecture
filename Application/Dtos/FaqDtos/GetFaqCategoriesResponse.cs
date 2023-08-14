namespace Application.Dtos.FaqDtos
{
    public record GetFaqCategoriesResponse
    {
        public required long Id { get; set; }
        public required string CategoryName { get; set; }
    }
}
