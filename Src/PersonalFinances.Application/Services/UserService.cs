using AutoMapper;
using PersonalFinances.Application.Interfaces;
using PersonalFinances.Application.ViewModel.Response;
using PersonalFinances.Domain.Entities;
using PersonalFinances.Domain.Interfaces;

namespace PersonalFinances.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = (List<User>) await _userRepository.GetAllAsync();

            List<UserResponse> userResponses = _mapper.Map<List<UserResponse>>(users);

            return userResponses;
        }

        public async Task<UserResponse> GetById(int id)
        {
            User user = (User) await _userRepository.GetByIdAsync(id);
            UserResponse userResponse = _mapper.Map<UserResponse>(user); 

            return userResponse;
        }
    }
}