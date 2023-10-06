using ServerB.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ServerB.WorkerSyncData
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var facilityService = scope.ServiceProvider.GetRequiredService<FacilityService>();
                    var residentService = scope.ServiceProvider.GetRequiredService<ResidentService>();
                    var progressNoteService = scope.ServiceProvider.GetRequiredService<ProgressNoteService>();

                    await facilityService.SyncData();
                    await residentService.SyncData();
                    await progressNoteService.SyncData();
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
