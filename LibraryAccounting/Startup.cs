using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryAccounting.Infrastructure.Repositories;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using LibraryAccounting.Infrastructure.Tools;
using LibraryAccounting.Infrastructure.Validator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using LibraryAccounting.CQRSInfrastructure.Methods;

namespace LibraryAccounting
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
            services.AddHttpContextAccessor();
            services.AddDbContext<DataContext>(op => op.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                op => op.MigrationsAssembly("LibraryAccounting.Infrastructure.Repositories")));

            services.AddIdentity<ApplicationUser, ApplicationUserRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();

            services.AddTransient<IRepository<Book>, BookRepository>();
            services.AddTransient<IRepository<Booking>, BookingRepository>();

            services.AddTransient<ILibrarianTools, LibrarianTools>();
            services.AddTransient<IClientTools, ClientTools>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(MethodsAssembly.GetAssembly());
            services.AddMediatR(MethodsAssembly.GetAssembly());

            services.AddTransient<IValidator<Book>, BookValidator>();
            services.AddTransient<IValidator<Booking>, BookingValidator>();
            services.AddTransient<IValidator<ApplicationUser>, UserValidator>();

            services.AddRazorPages().AddFluentValidation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                 opt =>
                 {
                     opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Index");
                     opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Index");
                 });
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
                app.UseExceptionHandler("/Error");
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
                endpoints.MapRazorPages();
            });



        }
    }
}
