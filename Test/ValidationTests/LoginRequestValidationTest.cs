
using Application.Dtos.UserDtos;
using Application.Validators;

namespace Test.ValidationTests;

public class LoginRequestValidationTest
{
    private readonly LoginRequestValidator _validator = new LoginRequestValidator();

    [Fact]
    public void EmptyEmailTest()
    {
        var request = new LoginRequest
        {
            Email = "", // empty email
            Password = "1234"
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
    }


    [Fact]
    public void WrongEmailFormatTest()
    {
        var request = new LoginRequest
        {
            Email = "invalidemail", // invalid email format
            Password = "password123"
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyPasswordTest()
    {
        var request = new LoginRequest
        {
            Email = "test@example.com",
            Password = "" // empty password
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
    }


    [Fact]
    public void CorrectFormaTest()
    {
        var request = new LoginRequest
        {
            Email = "test@example.com",
            Password = "password123"
        }; // valid Email and valid password

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

}
