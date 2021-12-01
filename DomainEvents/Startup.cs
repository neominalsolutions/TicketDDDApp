
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using DomainEvents.Src.Domain.Tickets.Entities;
using DomainEvents.Src.SeedWork;
using DomainEvents.Src.EFCore;
using DomainEvents.Src.Domain.Tickets.Repositories;
using DomainEvents.Src.EFCore.Repositories;
using DomainEvents.Src.Domain.Tickets.Services;
using DomainEvents.Src.Infrastructure.Email;
using DomainEvents.Src.Infrastructure.Email.NetSmtp;

namespace DomainEvents
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
            services.AddRazorPages();
            services.AddDbContext<TicketContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
            services.AddMediatR(typeof(Startup));

            services.AddScoped<ITicketRepository, EFTicketRepository>();
            services.AddScoped<ITicketAssignmentDomainService, TicketAssignmentDomainService>();

            services.AddSingleton<IEmailSender, NetSmptMailService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
