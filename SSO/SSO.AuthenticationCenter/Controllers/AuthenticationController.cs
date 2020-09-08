using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSO.AuthenticationCenter.Utility;
using SSO.DB;
using SSO.Interface;
using SSO.Service;
using SSO.Entity;

namespace SSO.AuthenticationCenter.Controllers
{
    [Route("api/Token")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> logger;
        private IJWTService JWTService;
        private IAccountService AccountService;
        public AuthenticationController(ILogger<AuthenticationController> log,IJWTService jwtService,IAccountService account)
        {
            this.logger = log;
            this.JWTService = jwtService;
            this.AccountService = account;
        }

        [HttpPost]
        public async Task<DataResult<TokenInfo>> Post([FromBody]LoginInfo info)
        {
            if (string.IsNullOrEmpty(info.UserName) || string.IsNullOrEmpty(info.Password))
            {
                return BaseResult.CreateDataResult<TokenInfo>(null,false,"用户名密码不能为空");
            }
            UserInfo user = new UserInfo()
            {
                UserName = info.UserName,
                PassWord = info.Password
            };
            var result =await AccountService.Login(user);
            if (result.IsSuccess)
            {
                var token = JWTService.GetToken(info.UserName);
                TokenInfo t = new TokenInfo()
                {
                    UserName = user.UserName.Trim(),
                    LoginedOn = DateTime.Now,
                    AccessToken = token
                };

                return BaseResult.CreateDataResult<TokenInfo>(t, true, "");

            }
            else
            {
                return BaseResult.CreateDataResult<TokenInfo>(null, false,result.Msg);
            }


          
        }
    }
}
