using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerformanceManagement.DATA;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.HomeRepository;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.Components;
using ProjectF.ExernalServices;
using System;
using System.Collections.Generic;

namespace ProjectF
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
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                               );
            services.AddRazorPages();
            string connectionString = this.Configuration.GetConnectionString("MyDefaultContext");
            services.AddDbContext<PerformanceManagementDBContext>(Options => 
            Options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
           
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetConnectionString("MyHangfireConnection"), new SqlServerStorageOptions
                    {
                      CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                      SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                      QueuePollInterval = TimeSpan.Zero,
                      UseRecommendedIsolationLevel = true,
                      UsePageLocksOnDequeue = true,
                      DisableGlobalLocks = true
                    }));
            services.AddHangfireServer();
            JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("MyHangfireConnection"));




            services.AddControllersWithViews();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<ISystemeRepository, SystemeRepository>();
            services.AddScoped<ICommitsController, CommitsController>();
            services.AddScoped<IUserBadgeRepository, UserBadgeRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IToDosController, ToDosController>();
            services.AddScoped<IHangfireRecurringJobScheduler, HangfireRecurringJobScheduler>();

            services.AddIdentity<User, AppRole>()
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<PerformanceManagementDBContext>();
            

            services.AddAutoMapper(typeof(Startup));
            //services.AddMvcCore(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //}).AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            UserManager<User> userManager,
            RoleManager<AppRole> roleManager,
            IHangfireRecurringJobScheduler Scheduler
            )
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


            ////HANGFIRE
            //Scheduler.ScheduleCommitbadgeTask();
            ////Scheduler.ScheduleUserbadgeTask();
            Scheduler.ScheduleUserbadgeTask();



            app.UseRouting();
            app.UseAuthentication();
            app.UseHangfireDashboard("/mydashboard", new DashboardOptions
            {
                //AppPath = "" //The path for the Back To Site link. Set to null in order to hide the Back To  Site link.
                DashboardTitle = "My Website",
                Authorization = new[]
                    {
                new HangfireCustomBasicAuthenticationFilter{
                    User = Configuration.GetSection("HangfireSettings:UserName").Value,
                    Pass = Configuration.GetSection("HangfireSettings:Password").Value
                }
            }
            });
            app.UseAuthorization();
            SeedData.Seed(userManager, roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();



            });
        }
    }
}
