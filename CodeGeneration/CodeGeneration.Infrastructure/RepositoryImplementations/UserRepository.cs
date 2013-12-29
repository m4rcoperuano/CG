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

        public int SaveUser(UserModel user)
        {
            using (var db = new CodeGenerationEntities())
            {
                UserProfile userProfile = Mapper.Map<UserProfile>(user);
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
                return 1;
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

        public bool RecordUserLogin(int id, DateTime nowTime)
        {
            using (var db = new CodeGenerationEntities())
            {
                UserProfile userProfile = db.UserProfiles.Find(id);
                //userProfile.LastLoggedIn = nowTime;
                return true;
            }
        }
    }
}
