using Application.Features.UserFeatures.Login;
using Application.Features.UserFeatures.Signup;

namespace Test
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void TestCreateUserRequestValidation1()
        {
            SignupRequest req = new SignupRequest
            { 
                Email = "ali.123",   // email is not valid
                Name  = "ali",
                Password = "something",
                PhoneNumber = "1234567890",              
            };

            SignupRequestValidator validator = new();
            var validatorResult = validator.Validate(req);

            if (validatorResult.IsValid)
                Assert.Fail();
        }

        [TestMethod]
        public void TestCreateUserRequestValidation2()
        {
            SignupRequest req = new SignupRequest
            {
                Email = "ali@ali.com",   // email is valid
                Name = "ali",
                Password = "something",
                PhoneNumber = "1234567890",
            };

            SignupRequestValidator validator = new();
            var validatorResult = validator.Validate(req);

            if (!validatorResult.IsValid)
                Assert.Fail();
        }

        [TestMethod]
        public void TestLoginRequestValidation1()
        {
            LoginRequest req = new LoginRequest
            {
                Email = "hassan@",   // email is not valid
                Password = "something",
            };

            LoginRequestValidator validator = new();
            var validatorResult = validator.Validate(req);

            if (validatorResult.IsValid)
                Assert.Fail();
        }

        [TestMethod]
        public void TestLoginRequestValidation2()
        {
            LoginRequest req = new LoginRequest
            {
                Email = "ali33@ali12.com",   // email is valid
                Password = "something",
            };

            LoginRequestValidator validator = new();
            var validatorResult = validator.Validate(req);

            if (!validatorResult.IsValid)
                Assert.Fail();
           
        }
    }
}


