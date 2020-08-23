using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestA
{
	public class ServiceB : BackgroundService
	{
		Microsoft.Extensions.Logging.ILogger _logger;

		private bool _stopping;
		private Task _backgroundTask;
		private MyConfiguration _config;

		public ServiceB(ILogger<ServiceB> logger, IOptions<MyConfiguration> config)
		{
			_logger = logger;
			_config = config.Value;
		}


		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("MyServiceB is starting.");
			Console.WriteLine(_config.KEYB);
			_backgroundTask = BackgroundTask();
			return Task.CompletedTask;

		}

		private async Task BackgroundTask()
		{
			while (!_stopping)
			{
				await Task.Delay(TimeSpan.FromSeconds(7));
				Console.WriteLine("MyServiceB is doing background work.");
			}

			Console.WriteLine("MyServiceB background task is stopping.");
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine("MyServiceB is stopping.");
			_stopping = true;
			if (_backgroundTask != null)
			{
				// TODO: cancellation
				await _backgroundTask;
			}
		}

		public override void Dispose()
		{
			Console.WriteLine("MyServiceB is disposing.");
		}


	}
}
