using CodeGeneration.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Interfaces
{
    public interface IMembership
    {
        int Login(string userName, string password, bool persistLogin = false);
        void Logout();
        int GetUserId();
        bool IsLoggedIn();
        void RegisterUser(string email, string password, IEmailService emailService, IUrlBuilder urlBuilder);
        bool Confirm(string confirmation);
    }
}
