using Client.DAL.Data;
using Client.DAL.Models;
using Client.PL.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Client.PL
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews(); //  Register built-in Services Required By MVC

			///services.AddScoped<ApplicationDbContext>();
			///services.AddScoped<DbContextOptions<ApplicationDbContext>>();

			///services.AddDbContext<ApplicationDbContext>
			///    (
			///    contextLifetime: ServiceLifetime.Scoped,
			///    optionsLifetime: ServiceLifetime.Scoped
			///    );


			// services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			services.AddDbContext<ApplicationDbContext>(
				options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
													   );

			//ApplicationServicesExtensions.AddApplicationServices(services);
			services.AddApplicationServices();

			services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
			{
				Options.Password.RequireDigit = true;
				Options.Lockout.AllowedForNewUsers = true;
				Options.Lockout.MaxFailedAccessAttempts = 5;
				Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
			}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.AccessDeniedPath = "/Home/Error";
			});
			 services.AddAuthentication();// Will Be Call With AddIdentity We Use It IF We Need The Other Overlodes
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
			});
		}
	}
}
