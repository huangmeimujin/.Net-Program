using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SSO.DB
{
    [Table("UserInfo")]
    public class UserInfo: BaseEntity<int>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        public string CreateTime { get; set; }
    }
}
