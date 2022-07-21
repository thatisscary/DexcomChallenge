namespace DexcomChallenge.Services.Clients
{
    using System;
    using System.Text.Json;
    using DexcomChallenge.Models.External;
    using DexcomChallenge.Services.Contracts;
    using Microsoft.Extensions.Logging;

    public class EstimatedGlucoseClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EstimatedGlucoseClient> _logger;

        public EstimatedGlucoseClient(HttpClient httpClient, ILogger<EstimatedGlucoseClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IOperationResult<CollectedEstimatedGlucoseValues>> GetCollectedGlucoseValuesAsync()
        {
            try
            {
                return await TryGetCollectedGlucoseValuesAsync();
            }
            catch (InvalidOperationException exception)

            {
                return exception.HandleAndLogException<CollectedEstimatedGlucoseValues>($"Http Client raised an unexpected {exception.GetType().Name}", _logger);
            }
            catch (HttpRequestException exception)
            {
                return exception.HandleAndLogException<CollectedEstimatedGlucoseValues>($"Http Client encountered unexpected network or protocol exception", _logger);
            }
            catch (TaskCanceledException exception)
            {
                return exception.HandleAndLogException<CollectedEstimatedGlucoseValues>($"Http Client timed out before response could be received", _logger);
            }
            catch (Exception exception)
            {
                return exception.HandleAndLogException<CollectedEstimatedGlucoseValues>($"Error ${exception.Message} received when retrieving Estimated Glucose Values ", _logger);
            }
        }

        private async Task<IOperationResult<CollectedEstimatedGlucoseValues>> TryGetCollectedGlucoseValuesAsync()
        {
            using (HttpResponseMessage httpResponse = await _httpClient.SendAsync(BuildHttpRequest()))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    if (httpResponse.Content != null)
                    {
                        using (var streamResult = await httpResponse.Content.ReadAsStreamAsync())
                        {
                            var result = await JsonSerializer.DeserializeAsync<CollectedEstimatedGlucoseValues>(streamResult);
                            return new OperationSuccessResult<CollectedEstimatedGlucoseValues>(result);
                        }
                    }
                    else
                    {
                        return new OperationFailureResult<CollectedEstimatedGlucoseValues>("Service returned empty result when querying for estimated glucose values");
                    }
                }
                else
                {
                    return new OperationFailureResult<CollectedEstimatedGlucoseValues>($"Service returned a exception status of {httpResponse.StatusCode} when querying for estimated glucose values");
                }
            }
        }

        private HttpRequestMessage BuildHttpRequest()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddDays(-16);
            string queryString = $"startDate=${startDate:yyyy-MM-DDTHH:mm:ss}&endDate=${endDate:yyyy-MM-DDTHH:mm:ss}";

            var builder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Port = -1,
                Host = "https://sandbox-api.dexcom.com/",
                Path = "/v2/users/self/egvs"
            };

            builder.Query = queryString;

            var request = new HttpRequestMessage
            {
                RequestUri = builder.Uri,
                Method = HttpMethod.Get,
            };

            return request;
        }
    }
}