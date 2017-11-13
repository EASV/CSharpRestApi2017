using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;

namespace CustomerRestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

			services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
				builder.WithOrigins("http://localhost:4200")
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));

            services.AddSingleton(Configuration);
            services.AddScoped<IBLLFacade, BLLFacade>();
        
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            if (env.IsDevelopment())
            {
				loggerFactory.AddConsole(Configuration.GetSection("Logging"));
				loggerFactory.AddDebug();

				app.UseDeveloperExceptionPage();
     //           var facade = new BLLFacade();

     //           var address = facade.AddressService.Create(
     //               new AddressBO() {
     //                   City = "Kolding",
     //                   Street = "SesamStrasse",
     //                   Number = "22A"
     //               });

     //           var address2 = facade.AddressService.Create(
     //               new AddressBO()
     //               {
     //                   City = "BingoCity",
     //                   Street = "DingoDoiok",
     //                   Number = "2e2"
     //               });

     //           var address3 = facade.AddressService.Create(
     //               new AddressBO()
     //               {
     //                   City = "Hurly Smurf",
     //                   Street = "Trainstiik",
     //                   Number = "44d"
     //               });

     //           var cust = facade.CustomerService.Create(
     //               new CustomerBO() {
     //                   FirstName="Lars",
     //                   LastName = "Bilde",
     //                   AddressIds = new List<int>() { address.Id, address3.Id }
     //               });
     //           facade.CustomerService.Create(
     //               new CustomerBO()
     //               {
     //                   FirstName = "Ole",
     //                   LastName = "Eriksen",
     //                   AddressIds = new List<int>() { address.Id, address2.Id }
     //               });

     //           for (int i = 0; i < 5; i++){
					//facade.OrderService.Create(
					//new OrderBO()
					//{
					//	DeliveryDate = DateTime.Now.AddMonths(1),
					//	OrderDate = DateTime.Now.AddMonths(-1),
     //                   CustomerId = cust.Id
					//});
                //}

            }

            app.UseMvc();
        }
    }
}

