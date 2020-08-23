using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Threading.Tasks;

namespace TestA
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("Starting");
			var builder = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<ServiceA>();
					services.AddHostedService<ServiceB>();
					services.Configure<MyConfiguration>(hostContext.Configuration.GetSection(
										MyConfiguration.Prefix));
					
				});
			//builder.UseConsoleLifetime();
			
			builder.ConfigureLogging((ctx, log) => log.AddLog4Net("log4net.xml",true));
			//builder.ConfigureLogging((ctx, log) => log.AddConsole());
			builder.ConfigureAppConfiguration(builder => builder.AddEnvironmentVariables());
			

			//var host = builder.Build();
			//await host.RunAsync();

			await builder.RunConsoleAsync();
			Console.WriteLine("Finished");
		}
	}
}
