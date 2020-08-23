using log4net.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestA
{
	public class ServiceA : BackgroundService
	{
		Microsoft.Extensions.Logging.ILogger _logger;
		IConfiguration _config;

		public ServiceA(ILogger<ServiceA> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			//Console.WriteLine("MyServiceA is starting.");
			_logger.LogInformation("Service {x} is starting", "A");

			string test = _config["COMPUTERNAME"];
			using (_logger.BeginScope("Computername {0}", test))
			{
				_logger.LogInformation("My information");
			}
			

			stoppingToken.Register(() => Console.WriteLine("MyServiceA is stopping."));

			while (!stoppingToken.IsCancellationRequested)
			{
				Console.WriteLine("MyServiceA is doing background work.");

				await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
			}

			Console.WriteLine("MyServiceA background task is stopping.");

		}
	}
}
