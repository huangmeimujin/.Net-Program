using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SSO.DB;
using SSO.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSO.Service
{
    public class AccountService : BaseService,IAccountService
    {
        public AccountService(SSODBContext dbContext):base(dbContext)
        {

        }
        public async Task<DataResult<UserInfo>> Login(UserInfo user)
        {
            string sql = "select * from UserInfo where userName=@userName and PassWord=@passWord";
            var ps = new List<SqlParameter>()
                {
                     new SqlParameter("@userName", user.UserName),
                     new SqlParameter("@passWord", user.PassWord),
                }.ToArray();

            var query= this.ExecuteQuery<UserInfo>(sql,ps);
            var datas =await query.FirstOrDefaultAsync();
            var msg = datas != null ? "" : "用户名不存在";
            return BaseResult.CreateDataResult(datas,isSuccess:datas!=null,msg);
        }
        public BaseResult Regist(UserInfo user)
        {
            var data= this.Insert<UserInfo>(user);
            return new BaseResult()
            {
                IsSuccess = data != null ? true : false,
                Msg = ""
            };
        }
    }
}
