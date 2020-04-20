using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZJWebAdvert.AdvertApi.Services;

namespace ZJWebAdvert.AdvertApi.HealthChecks
{
    public class StorageHealthCheck : IHealthCheck
    {
        private readonly IAdvertStorageService _storageService;
        public StorageHealthCheck(IAdvertStorageService storageService)
        {
            _storageService = storageService;
        }
        public async ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            var isStorageOK = await _storageService.CheckHealthAsync();
            return HealthCheckResult.FromStatus(isStorageOK ? CheckStatus.Healthy : CheckStatus.Unhealthy, "");
        }
    }
}
