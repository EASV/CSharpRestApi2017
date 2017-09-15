using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CustomerRestAPI
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
				loggerFactory.AddConsole(Configuration.GetSection("Logging"));
				loggerFactory.AddDebug();

				app.UseDeveloperExceptionPage();
                var facade = new BLLFacade();

                var address = facade.AddressService.Create(
                    new AddressBO() {
                        City = "Kolding",
                        Street = "SesamStrasse",
                        Number = "22A"
                    });

                var cust = facade.CustomerService.Create(
                    new CustomerBO() {
                        FirstName="Lars",
                        LastName = "Bilde",
                        Addresses = new List<AddressBO>() { address }
                    });
                facade.CustomerService.Create(
                    new CustomerBO()
                    {
                        FirstName = "Ole",
                        LastName = "Eriksen",
                        Addresses = new List<AddressBO>() { address }
                    });

                for (int i = 0; i < 5; i++){
					facade.OrderService.Create(
					new OrderBO()
					{
						DeliveryDate = DateTime.Now.AddMonths(1),
						OrderDate = DateTime.Now.AddMonths(-1),
                        CustomerId = cust.Id
					});
                }

            }

            app.UseMvc();
        }
    }
}
