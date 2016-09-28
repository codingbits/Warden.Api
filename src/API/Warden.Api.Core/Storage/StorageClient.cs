﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Api.Core.Settings;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public class StorageClient : IStorageClient
    {
        private readonly StorageSettings _settings;
        private readonly HttpClient _httpClient;
        private string BaseAddress => _settings.Url.EndsWith("/") ? _settings.Url : $"{_settings.Url}/";

        public StorageClient(StorageSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient {BaseAddress = new Uri(BaseAddress)};
            _httpClient.DefaultRequestHeaders.Remove("Accept");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<Maybe<T>> GetAsync<T>(string endpoint) where T : class
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
                return new Maybe<T>();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }
    }
}