using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Entity
{
    public class TokenInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //public string Oper { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime LoginedOn { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //public Powers Power { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccessToken { get; set; }
    }
}
