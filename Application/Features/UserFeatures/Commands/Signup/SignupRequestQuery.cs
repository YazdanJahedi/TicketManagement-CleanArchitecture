using Application.Common.Exceptions;
using Application.DTOs.UserDtos;
using Application.Repository;
using Domain.Entities;
using MediatR;


namespace Application.Features.UserFeatures.Commands.Signup
{
    public class SignupRequestQuery : IRequestHandler<SignupRequest, SignupResponse>
    {

        private readonly IUsersRepository _usersRepository;

        public SignupRequestQuery(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<SignupResponse> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            // Check validation
            if (!SignupRequestValidator.IsValid(request))
            {
                throw new ValidationErrorException("Email must be in email format and password can not be empty");
            }

            // Check if email is used
            var foundUser = await _usersRepository.FindByEmailAsync(request.Email);
            if (foundUser != null) throw new Exception("this Email is used before");

            // Creating new user
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = "User",
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword,
                CreationDate = DateTime.Now,
            };

            // Add new user to the data base
            await _usersRepository.AddAsync(user);

            var response = new SignupResponse
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                CreationDate = user.CreationDate,
            };

            return response;
        }
    }
}
