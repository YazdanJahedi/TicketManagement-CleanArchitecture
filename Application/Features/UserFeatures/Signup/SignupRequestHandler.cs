using Application.Common.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Web.Helpers;


namespace Application.Features.UserFeatures.Signup
{
    public class SignupRequestHandler : IRequestHandler<SignupRequest, SignupResponse>
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public SignupRequestHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<SignupResponse> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            // Check if email is used
            var foundUser = await _usersRepository.GetAsync(e => e.Email == request.Email);
            if (foundUser != null) throw new Exception("this Email is used before");

            // Creating new user
            var user = _mapper.Map<User>(request);

            // Add new user to the data base
            await _usersRepository.AddAsync(user);

            var response = _mapper.Map<SignupResponse>(user);
            return response;
        }
    }
}
