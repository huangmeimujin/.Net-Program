using SSO.DB;
using System;
using System.Threading.Tasks;

namespace SSO.Interface
{
    public interface IAccountService
    {
        Task<DataResult<UserInfo>> Login(UserInfo user);

        BaseResult Regist(UserInfo user);
        
    }
}
