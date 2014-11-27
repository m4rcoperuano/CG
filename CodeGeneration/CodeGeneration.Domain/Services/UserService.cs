using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserModel GetUser(int id)
        {
            UserModel user = this._userRepository.GetUser(id);
            return user;
        }

        public int SaveUser(UserModel userModel)
        {
            userModel.CreatedAt = DateTime.Now;
            userModel.UpdatedAt = DateTime.Now;
            return this._userRepository.SaveNewUser(userModel);
        }

        public void RecordUserLogin(int userId, DateTime now)
        {
            this._userRepository.RecordUserLogin(userId, now);
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            UserModel user = this._userRepository.GetUserViaEmail(email);
            return user != null;
        }
    }
}
