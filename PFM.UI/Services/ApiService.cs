using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public ApiService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpcontextAccessor = httpContextAccessor;
        }



        public LoginResponseModel Login(LoginModel loginModel)
        {
            var apiUrl = _configuration["ApiSettings:APIUrl"];

            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                throw new InvalidOperationException(
                    "ApiSettings:APIUrl is missing from appsettings.json.");
            }

            try
            {
                // Send login details to the API.
                var response = _httpClient.PostAsJsonAsync(apiUrl + "api/Users/Login", loginModel).GetAwaiter().GetResult();

                // Show the actual API error response if login fails.
                if (!response.IsSuccessStatusCode)
                {
                    var error = response.Content.ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();

                    throw new Exception(
                        $"Login API failed: {(int)response.StatusCode} " +
                        $"{response.StatusCode}. Response: {error}");
                }

                // Convert the API response into LoginResponseModel.
                var login = response.Content
                    .ReadFromJsonAsync<LoginResponseModel>()
                    .GetAwaiter()
                    .GetResult();

                return login ?? throw new Exception(
                    "The login API returned an empty response.");
            }
            catch (HttpRequestException ex)
            {
                // Include the inner exception, which may identify the SSL problem.
                throw new Exception(
                    $"Unable to connect to the PFM API. Details: {ex}", ex);
            }
        }

        public bool Register(RegisterModel registerModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                throw new InvalidOperationException("ApiSettings:APIUrl is missing.");
            }

            var response = _httpClient .PostAsJsonAsync( ApiUrl + "api/Users/Register", registerModel).Result;

            return response.IsSuccessStatusCode;
        }
    }
}
