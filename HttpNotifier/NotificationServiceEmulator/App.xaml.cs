using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

#pragma warning disable CS1591

namespace NotificationServiceEmulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int ApiHostingPort => 5020;

        public App()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls($"http://*:{ApiHostingPort}")
                .UseStartup<Startup>()
                .Build();
            SimpleServiceResolver.CreateInstance(host);

            //Required because of a flaw in JetBrains Rider
            SimpleViewModel.SetIsDesignMode(false);
        }

        //Probably not needed if not hosting AspNetCore (?)
        protected override async void OnStartup(StartupEventArgs e)
        {
            await SimpleServiceResolver.Instance.StartupHost();
            base.OnStartup(e);
        }

        //Probably not needed if not hosting AspNetCore (?)
        protected override async void OnExit(ExitEventArgs e)
        {
            await SimpleServiceResolver.Instance.ShutdownHost();
            base.OnExit(e);
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add services needed by application
            //services.AddSingleton<ISimpleHttpClientFactory, SimpleHttpClientFactory>();
            services.AddSingleton<ISimpleMessaging>(SimpleMessaging.Instance);

            //Added for Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "NotificationServiceEmulator.xml"));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service Emulator API");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
