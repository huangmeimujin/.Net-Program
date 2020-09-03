using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.DB
{
    public class SSODBContext:DbContext
    {
        public SSODBContext(DbContextOptions<SSODBContext> options) : base(options)
        {

        }
        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
