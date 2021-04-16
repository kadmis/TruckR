using Hangfire;
using Location.Infrastructure.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Infrastructure.Jobs.Recurring
{
    public class PersistStatisticsJob
    {
        private const string ID = "Location.Infrastructure.Jobs.Recurring.PersistStatisticsJob";
        private readonly IStatisticsPersistingService _persistingService;

        public PersistStatisticsJob(IStatisticsPersistingService persistingService)
        {
            _persistingService = persistingService;
        }
        private PersistStatisticsJob()
        {
        }

        public static void Register()
        {
            var job = new PersistStatisticsJob();
            RecurringJob.AddOrUpdate(ID, () => job.Perform(CancellationToken.None), Cron.Hourly, TimeZoneInfo.Utc);
        }

        public async Task Perform(CancellationToken cancellationToken = default)
        {
            await _persistingService.Persist(cancellationToken);
        }
    }
}
