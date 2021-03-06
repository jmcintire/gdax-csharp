﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.Fundings.Models;
using GDAXSharp.Services.Fundings.Types;
using GDAXSharp.Shared.Utilities.Queries;

namespace GDAXSharp.Services.Fundings
{
    public class FundingsService : AbstractService
    {
        private readonly IQueryBuilder queryBuilder;

        public FundingsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IQueryBuilder queryBuilder)
                : base(httpClient, httpRequestMessageService)
        {
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<IList<Funding>>> GetAllFundingsAsync(
            int limit = 100,
            FundingStatus? status = null,
            int numberOfPages = 0)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("status", status?.ToString().ToLower()));

            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<Funding>(HttpMethod.Get, "/funding" + queryString, numberOfPages: numberOfPages);

            return httpResponseMessage;
        }
    }
}
