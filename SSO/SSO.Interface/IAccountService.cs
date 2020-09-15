using SSO.DB;
using SSO.Entity;
using System;
using System.Threading.Tasks;

namespace SSO.Interface
{
    public interface IAccountService
    {
        Task<DataResult<UserInfo>> Login(LoginInfo user);

        BaseResult Regist(UserInfo user);
        
    }
}
