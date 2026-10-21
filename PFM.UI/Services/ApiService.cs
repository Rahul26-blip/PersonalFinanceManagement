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
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                throw new InvalidOperationException("APISettings:APIUrl is missing from appsettings.json.");
            }
            var response = _httpClient.PostAsJsonAsync(ApiUrl + "api/Users/Login", loginModel).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception( $"Login API failed. Status: {(int)response.StatusCode} " + $"{response.StatusCode}. Response: {response}");
            }

            var login = response.Content.ReadFromJsonAsync<LoginResponseModel>().Result;

            return login;

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
