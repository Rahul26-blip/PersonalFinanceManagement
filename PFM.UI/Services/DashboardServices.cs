
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class DashboardServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardServices(
            HttpClient httpClient,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public DashboardModel GetDashboard()
        {
            var apiUrl = _configuration["ApiSettings:APIUrl"];

            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                throw new InvalidOperationException(
                    "ApiSettings:APIUrl is missing.");
            }

            var token = _httpContextAccessor.HttpContext?
                .Session.GetString("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException(
                    "Login session token was not found. Please log in again.");
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = _httpClient.GetAsync(
                apiUrl + "api/Dashboard/GetDashboard").Result;

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsStringAsync().Result;

                throw new HttpRequestException(
                    $"Dashboard API returned {(int)response.StatusCode} " +
                    $"{response.StatusCode}. Response: {error}");
            }

            var dashboard = response.Content
                .ReadFromJsonAsync<DashboardModel>().Result;

            if (dashboard == null)
            {
                throw new InvalidOperationException(
                    "Dashboard API returned an empty response.");
            }

            return dashboard;
        }
    }
}
