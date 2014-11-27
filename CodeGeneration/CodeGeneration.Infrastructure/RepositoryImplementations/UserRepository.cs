using AutoMapper;
using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Infrastructure.RepositoryImplementations
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            Mapper.CreateMap<UserModel, UserProfile>();
            Mapper.CreateMap<UserProfile, UserModel>();
        }

        public int SaveNewUser(UserModel user)
        {
            using (var db = new CodeGenerationEntities())
            {
                UserProfile userProfile = Mapper.Map<UserProfile>(user);
                userProfile.LastLoggedIn = null;
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
                return userProfile.id_user;
            }
        }

        public UserModel GetUser(int id)
        {
            using (var db = new CodeGenerationEntities())
            {
                //get user from db and convert to model
                return new UserModel();
            }
        }

        public UserModel GetUserViaEmail(string email)
        {
            using (var db = new CodeGenerationEntities())
            {
                var dbUser = db.UserProfiles.SingleOrDefault(x => x.email == email);
                UserModel userModel = Mapper.Map<UserModel>(dbUser);
                if (dbUser != null)
                {
                    userModel.Email = dbUser.email;
                }

                return userModel;
            }
        }

        public bool RecordUserLogin(int id, DateTime nowTime)
        {
            using (var db = new CodeGenerationEntities())
            {
                UserProfile userProfile = db.UserProfiles.Find(id);
                userProfile.LastLoggedIn = nowTime;
                return true;
            }
        }


    }
}
