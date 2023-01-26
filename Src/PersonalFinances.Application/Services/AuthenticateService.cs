using Microsoft.Extensions.Configuration;
using PersonalFinances.Application.Interfaces;
using PersonalFinances.Application.ViewModel.Request.User;
using PersonalFinances.Domain.Entities;
using PersonalFinances.Domain.Exceptions;
using PersonalFinances.Domain.Interfaces;

namespace PersonalFinances.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private IAsyncRepository<User> _userRepository;
        private ITokenService _tokenService;
        private IConfiguration _configuration;

        public AuthenticateService(IAsyncRepository<User> userRepository, ITokenService tokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            configuration = _configuration;
        }

        public async Task<Token> Login(LoginRequest login)
        {
            User user = (await _userRepository.FindByAsync(x => x.Email == login.Email)).FirstOrDefault();

            if(user == null)
                throw new BusinessException("Email inválido");
            
            if(!user.IsValidLoginPassword(login.Password))
                throw new BusinessException("Senha inválida");

            Token token = _tokenService.GenerateToken(user, _configuration);

            if(token == null)
                throw new BusinessException("Não foi possivel gerar o token");

            return token;
        }

        public async Task Register(CreateUserRequest createUserRequest)
        {
            int REGULAR_ROLE_ID = 1;

            User user = new User(createUserRequest.Username, createUserRequest.Email, createUserRequest.Password, REGULAR_ROLE_ID);
            await _userRepository.AddAsync(user);
        }
    }
}