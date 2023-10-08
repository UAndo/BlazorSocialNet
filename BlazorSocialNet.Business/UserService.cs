using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using BlazorSocialNet.Entities;
using BlazorSocialNet.Repository;

namespace BlazorSocialNet.Business
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(User user)
        {
            try 
            {
                return await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try 
            {
                return await _userRepository.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                return await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _userRepository.GetSingleAsync<User>("sp_Users_GetByEmail", new { Email = email });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByToken(string token)
        {
            try
            {
                return await _userRepository.GetSingleAsync<User>("sp_Users_GetByToken", new { Token = token });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}