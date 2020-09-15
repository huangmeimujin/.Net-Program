using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SSO.DB;
using SSO.Interface;
using SSO.Service;
using SSO.Utility;

namespace SSO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddDbContext<SSODBContext>(opt=> opt.UseSqlServer(Configuration.GetConnectionString("ssoDbContext")));
            var connection = Configuration.GetConnectionString("ssoDbContext");
            services.AddDbContext<SSODBContext>(opt => opt.UseSqlServer(connection,c=>c.MigrationsAssembly("SSO")));
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IAccountService, AccountService>();


            #region JWT鉴权授权
            //1.Nuget引入程序包：Microsoft.AspNetCore.Authentication.JwtBearer 
            //services.AddAuthentication();//禁用  
            var validAudience = this.Configuration["audience"];
            var validIssuer = this.Configuration["issuer"];
            var securityKey = this.Configuration["SecurityKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //默认授权机制名称；                                      
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//是否验证Issuer
                             ValidateAudience = true,//是否验证Audience
                             ValidateLifetime = true,//是否验证失效时间
                             ValidateIssuerSigningKey = true,//是否验证SecurityKey
                             ValidAudience = validAudience,//Audience
                             ValidIssuer = validIssuer,//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))//拿到SecurityKey
                             //AudienceValidator = (m, n, z) =>
                             //{ 
                             //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                             //},//自定义校验规则，可以新登录后将之前的无效 
                         };
                     });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region 通过中间件来支持鉴权授权
            app.UseAuthentication(); //告诉框架 要使用权限认证
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }
    }
}
