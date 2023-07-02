using Engzny.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engzny.DAL.Database;
using Engzny.BL.Repository;
using NToastNotify;
using Newtonsoft.Json.Serialization;
using Engzny.BL.Mapper;
using Engzny.BL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Engzny.BL.hub;
using Engzny.BL.Helper;

namespace Engzny
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
            services.AddControllersWithViews().AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddDbContextPool<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICraftmanRep, CraftmanRep>();
            services.AddScoped<IServiceRep,ServiceRep>();
            services.AddScoped<IHomeRep,HomeRep>();
            services.AddScoped<IAccessRep, AccessRep>();
            services.AddScoped<IRolesRep, RolesRep>();
            services.AddScoped<IUserRep, UserRep>();
            services.AddScoped<IContactRep, ContactRep>();
            services.AddScoped<IOrderRep, OrderRep>();
            services.AddScoped<IMailHelper,MailHelper>();

            services.AddSignalR();










            services.AddMvc().AddNToastNotifyToastr(new  ToastrOptions() {
                ProgressBar = true,
                PreventDuplicates = true,
                CloseButton = true,
                PositionClass = ToastPositions.TopRight,
                
            });
            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Context>().AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
     
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

               
                });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                    endpoints.MapHub<NotifiactionHub>("/notifiactionHub");

            });
        }
    }
}
