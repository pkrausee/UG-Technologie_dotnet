using System;
using System.Threading.Tasks;

namespace SchoolApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            CreateRolesAndAdminUser(serviceProvider);
        }

        private static void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            const string adminRoleName = "Administrator";
            string[] roleNames = { adminRoleName, "Guest", "Teacher" };

            foreach (var roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            AddUserToRole(serviceProvider, adminRoleName);
        }

        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }

        private static void AddUserToRole(IServiceProvider serviceProvider, string adminRoleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var checkAppUser = userManager.FindByEmailAsync("admin@admin.pl");
            checkAppUser.Wait();

            var appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                var newAppUser = new IdentityUser
                {
                    Email = "admin@admin.pl",
                    UserName = "admin@admin.pl"
                };

                var taskCreateAppUser = userManager.CreateAsync(newAppUser, "zaq1@WSX");
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            var newUserRole = userManager.AddToRoleAsync(appUser, adminRoleName);
            newUserRole.Wait();
        }
    }
}
