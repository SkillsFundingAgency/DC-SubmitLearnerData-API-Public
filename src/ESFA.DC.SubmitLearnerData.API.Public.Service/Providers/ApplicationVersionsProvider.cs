﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model.Config;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Providers
{
    public class ApplicationVersionsProvider : IApplicationVersionsProvider
    {
        private const string CacheEntry = "Versions";

        private readonly IRepositoryService _applicationVersionsRepositoryService;
        private readonly IAPICacheRetrievalService _apiCacheRetrieval;
        private readonly APIConfiguration _configuration;

        public ApplicationVersionsProvider(
            IRepositoryService applicationVersionsRepositoryService,
            IAPICacheRetrievalService apiCacheRetrieval,
            APIConfiguration configuration)
        {
            _applicationVersionsRepositoryService = applicationVersionsRepositoryService;
            _apiCacheRetrieval = apiCacheRetrieval;
            _configuration = configuration;
        }

        public async Task<ApplicationVersionLocation> DownloadLocation()
        {
            return new ApplicationVersionLocation
            {
                Url = _configuration.SubmitLearnerDataDownloadsUrl
            };
        }

        public async Task<bool> IsLatestVersion(string academicYear, Version version, CancellationToken cancellationToken)
        {
            return await _apiCacheRetrieval
                .GetOrCreate(string.Concat(
                    CacheEntry, academicYear),
                    _configuration.CacheExpiration,
                    CheckApplicationVersion(academicYear, version, cancellationToken));
        }

        private async Task<bool> CheckApplicationVersion(string academicYear, Version version, CancellationToken cancellationToken)
        {
            return await _applicationVersionsRepositoryService.IsLatestDesktopApplicationVersion(academicYear, version, cancellationToken);
        }
    }
}