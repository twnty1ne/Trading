using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trading.Api.Services;
using Trading.Bot;
using Trading.Bot.Sessions;
using Trading.Bot.Strategies;
using Trading.Connections.Binance;
using Trading.Exchange;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Report.Core;
using Trading.Report.DAL;

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

            services
                .AddSingleton<IExchange, Exchange.Exchange>()
                .Configure<Exchange.Options>(x => x.ConnectionType = ConnectionEnum.Binance);

            services
                .AddSingleton<IBot, Bot.Bot>()
                .Configure<Bot.Options>(x => 
                {
                    x.Session = Sessions.BackTest;
                    x.Strategy = Strategies.CandleVolume;
                });

            services.AddTransient<ICredentialsProvider, BinanceCredentialsProvider>();

            services.AddDbContext<SessionContext>(ServiceLifetime.Scoped);
            services.AddTransient<IRepository<Session>, SessionRepository>();
            services.AddTransient<IRepository<Trade>, TradeRepository>();
            services.AddTransient<IRepository<Instrument>, InstrumentRepository>();
            services.AddTransient<IRepository<Strategy>, StrategyRepository>();
            services.AddTransient<IRepository<Timeframe>, TimeframeRepository>();

            services.AddTransient<ICandleService, CandleService>();
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
