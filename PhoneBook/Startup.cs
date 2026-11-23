using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// اضافه‌شده برای EF Core: متد UseSqlServer در این namespace قرار دارد.
using Microsoft.EntityFrameworkCore;

// اضافه‌شده برای EF Core: namespace کلاس PhoneBookDbContext خودمان است.
using PhoneBook.Data;

namespace PhoneBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // اضافه‌شده برای EF Core:
            // PhoneBookDbContext را ثبت می‌کند تا کنترلر بتواند
            // آن را از طریق سازنده دریافت کند.
            services.AddDbContext<PhoneBookDbContext>(options =>
                options.UseSqlServer(
                    // این نام باید با کلید موجود در appsettings.json یکسان باشد.
                    Configuration.GetConnectionString("PersonnelReview")));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}