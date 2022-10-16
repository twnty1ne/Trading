using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trading.Bot;
using Trading.Bot.Sessions;
using Trading.Bot.Strategies;
using Trading.Connections.Binance;
using Trading.Exchange;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;

namespace Trading.Api
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
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddSingleton<IExchange, Exchange.Exchange>().Configure<Exchange.Options>(x => x.ConnectionType = ConnectionEnum.Binance);
            services.AddSingleton<IBot, Bot.Bot>().Configure<Bot.Options>(x => 
            {
                x.Session = Sessions.ForwardTest;
                x.Strategy = Strategies.CandleVolume;
            });
            services.AddTransient<ICredentialsProvider, BinanceCredentialsProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });

            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
