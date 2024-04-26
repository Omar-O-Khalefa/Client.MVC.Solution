
using Client.DAL.Data;
using Client.DAL.Models;
using Client.PL.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Client.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var WebApplicationbuilder = WebApplication.CreateBuilder(args);

			#region Configure Services

			WebApplicationbuilder.Services.AddControllersWithViews(); //  Register built-in Services Required By MVC

			///		WebApplicationbuilder.Services.AddScoped<ApplicationDbContext>();
			///		WebApplicationbuilder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>();

			///		WebApplicationbuilder.Services.AddDbContext<ApplicationDbContext>
			///    (
			///    contextLifetime: ServiceLifetime.Scoped,
			///    optionsLifetime: ServiceLifetime.Scoped
			///    );


			// 		WebApplicationbuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfAiles()));

			WebApplicationbuilder.Services.AddDbContext<ApplicationDbContext>(
			 options => options.UseSqlServer(WebApplicationbuilder.Configuration.GetConnectionString("DefaultConnection"))
											   );

			//ApplicationServicesExtensions.AddApplicationServices(		WebApplicationbuilder.Services);
			WebApplicationbuilder.Services.AddApplicationServices();

			WebApplicationbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
			 {
				 Options.Password.RequireDigit = true;
				 Options.Lockout.AllowedForNewUsers = true;
				 Options.Lockout.MaxFailedAccessAttempts = 5;
				 Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
			 }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			WebApplicationbuilder.Services.ConfigureApplicationCookie(options =>
						 {
							 options.LoginPath = "/Account/SignIn";
							 options.ExpireTimeSpan = TimeSpan.FromDays(1);
							 options.AccessDeniedPath = "/Home/Error";
						 });
			WebApplicationbuilder.Services.AddAuthentication();// Will Be Call With AddIdentity We Use It IF We Need The Other Overlodes

			#endregion

			#region Configure Kestrel MiddelWears
			var app = WebApplicationbuilder.Build();

			if (app.Environment.IsDevelopment())
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
			#endregion

			app.Run();
		}


	}
}
