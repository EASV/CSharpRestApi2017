using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;

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
                var cust = facade.CustomerService.Create(
                    new CustomerBO() {
                        FirstName="Lars",
                        LastName = "Bilde",
                        Address = "Home"
                    });
                facade.CustomerService.Create(
                    new CustomerBO()
                    {
                        FirstName = "Ole",
                        LastName = "Eriksen",
                        Address = "Somewhere"
                    });

                for (int i = 0; i < 10000; i++){
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
