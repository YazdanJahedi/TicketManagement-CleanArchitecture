using Application.Common.Exceptions;
using Application.Repository;
using AutoMapper;
using MediatR;


namespace Application.Features.UserFeatures.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public LoginHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {

            // Find user by email
            var user = await _usersRepository.FindByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException("username or password is not correct");

            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordVerified) throw new NotFoundException("username or password is not correct");

            // create login response 
            var response = _mapper.Map<LoginResponse>(user);

            return response;
        }
    }
}
