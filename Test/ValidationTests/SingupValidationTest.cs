using Application.Dtos.UserDtos;
using Application.Validators;

namespace Test.ValidationTests
{
    public class SingupValidationTest
    {
        private readonly SignupRequestValidator _validator = new SignupRequestValidator();

        [Fact]
        public void EmptyEmailTest()
        {
            SignupRequest requeset = new SignupRequest
            {
                Email = "",
                Name = "name",
                Password = "password",
                PhoneNumber = "1234567890",
            };
            
            var result = _validator.Validate(requeset);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmptyNameTest()
        {
            SignupRequest requeset = new SignupRequest
            {
                Email = "email@email.email",
                Name = "",
                Password = "password",
                PhoneNumber = "1234567890",
            };

            var result = _validator.Validate(requeset);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CorrectFormatTest()
        {
            SignupRequest requeset = new SignupRequest
            {
                Email = "Email@Email.Email",
                Name = "name",
                Password = "password",
                PhoneNumber = "1234567890",
            };

            var result = _validator.Validate(requeset);

            Assert.True(result.IsValid);
        }
    }
}
