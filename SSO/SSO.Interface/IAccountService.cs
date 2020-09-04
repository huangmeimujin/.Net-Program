using SSO.DB;
using System;
using System.Threading.Tasks;

namespace SSO.Interface
{
    public interface IAccountService
    {
        Task<string> Login(UserInfo user);
        
    }
}
