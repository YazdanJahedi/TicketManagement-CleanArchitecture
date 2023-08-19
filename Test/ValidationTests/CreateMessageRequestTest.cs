using Application.Dtos.MessageDtos;
using Application.Validators;

namespace Test.ValidationTests
{
    public class CreateMessageRequestTest
    {
        private readonly CreateMessageRequestValidator _validator = new CreateMessageRequestValidator();

        [Fact]
        public void EmptyTextTest()
        {
            CreateMessageRequest request = new CreateMessageRequest
            {
                Text = "",
                TicketId = 1,
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmptyTicketIdTest()
        {
            CreateMessageRequest request = new CreateMessageRequest
            {
                Text = "some text",
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CorrectFormatTest()
        {
            CreateMessageRequest request = new CreateMessageRequest
            {
                Text = "some text",
                TicketId = 1,
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }
    }
}
