using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.DataManager.Scripts.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using UnityEngine;

namespace Assets.DataManager.Scripts.Api
{
    public interface IServiceConnector
    {
        Task<TResponse> GetReportResponse<TRequest, TResponse>(string uri, TRequest request) where TResponse : class;
        Task<TResponse> GetReportResponse<TResponse>(string uri) where TResponse : class;
    }
    public class ServiceConnector : IServiceConnector
    {
        private readonly string _apiServiceUrl;

        public ServiceConnector(IServiceConnectorConfiguration serviceConnectorConfiguration)
        {
            _apiServiceUrl = serviceConnectorConfiguration.ServiceUrl;
        }

        public async Task<TResponse> GetReportResponse<TRequest, TResponse>(string uri, TRequest request) where TResponse : class
        {
            var client = new HttpClient();
            var requestUrl = _apiServiceUrl + uri;
            Debug.Log($"Connecting to {requestUrl}");
            var message = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
            };




            var resp = await client.SendAsync(message);
            var content = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new Exception(resp.ReasonPhrase);

            var apiResponse = JsonConvert.DeserializeObject<TResponse>(content);

            return apiResponse;
        }

        public async Task<TResponse> GetReportResponse<TResponse>(string uri) where TResponse : class
        {

            var client = new HttpClient();
            var requestUrl = _apiServiceUrl + uri;

            var resp = await client.GetAsync(requestUrl);
            var content = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new Exception(resp.ReasonPhrase);

            var apiResponse = JsonConvert.DeserializeObject<TResponse>(content);

            return apiResponse;
        }

    }
}
