using System.Diagnostics;

namespace WorkerServiceExample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _batchFile;
        private readonly string _outputDir;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _batchFile = configuration.GetValue<string>("BatchFile");
            _outputDir = configuration.GetValue<string>("OutputDir");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.TimeOfDay.Hours == 14 && DateTime.Now.TimeOfDay.Minutes == 55 && DateTime.Now.TimeOfDay.Seconds == 0)
                //{
                var process = new Process();
                process.StartInfo.FileName = _batchFile;
                var dateTime = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss");
                process.StartInfo.Arguments = $"{_outputDir} {dateTime}";
                process.Start();
                await Task.Delay(1000, stoppingToken);
                //}
            }
        }
    }
}