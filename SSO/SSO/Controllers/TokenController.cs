using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSO.DB;
using SSO.Entity;
using SSO.Interface;
using SSO.Utility;

namespace SSO.Controllers
{
    [Route("api/Token")]
    public class TokenController : Controller
    {

        private IJWTService JWTService;
        private IAccountService AccountService;
        public TokenController(IJWTService jwtService, IAccountService account)
        {
            this.JWTService = jwtService;
            this.AccountService = account;
        }

        [HttpPost]
        public async Task<DataResult<TokenInfo>> Post([FromBody]LoginInfo info)
        {
            if (string.IsNullOrEmpty(info.UserName) || string.IsNullOrEmpty(info.Password))
            {
                return BaseResult.CreateDataResult<TokenInfo>(null, false, "用户名密码不能为空");
            }
            var result = await AccountService.Login(info);
            if (result.IsSuccess)
            {
                var token = JWTService.GetToken(info.UserName);
                TokenInfo t = new TokenInfo()
                {
                    UserName = info.UserName.Trim(),
                    LoginedOn = DateTime.Now,
                    AccessToken = token
                };

                return BaseResult.CreateDataResult<TokenInfo>(t, true, "");

            }
            else
            {
                return BaseResult.CreateDataResult<TokenInfo>(null, false, result.Msg);
            }



        }
    }
}