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
    }
}
