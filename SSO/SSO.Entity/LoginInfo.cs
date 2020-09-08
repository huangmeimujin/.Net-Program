using System;
using System.ComponentModel.DataAnnotations;

namespace SSO.Entity
{
    public class LoginInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //[Required]
        //public string CaptchaCode { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[Required]
        //public string CaptchaToken { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[Required]
        //public string CaptchaTrackeKey { get; set; }

        /// <summary>
        /// 是否是从手机版进入的, 默认 false
        /// </summary>
        public bool IsFromMobile { get; set; }
    }
}
