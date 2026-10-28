using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class RoleServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<RoleModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Roles/GetAll").Result;

            if (!result.IsSuccessStatusCode)
            {
                return new List<RoleModel>();
            }

            var RoleModel = result.Content.ReadFromJsonAsync<List<RoleModel>>().Result;

            return RoleModel;
        }

        public RoleModel? GetById(int id)
        {
            var apiUrl = _configuration["ApiSettings:APIUrl"];

            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                throw new InvalidOperationException(
                    "APISettings:APIUrl is missing from appsettings.json.");
            }

            var token = _httpContextAccessor.HttpContext?
                .Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(token)
                    ? null
                    : new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", token);

            var response = _httpClient
                .GetAsync($"{apiUrl.TrimEnd('/')}/api/Roles/GetById/{id}")
                .Result;

            // Show the actual reason if the API request fails.
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;

                throw new Exception(
                    $"Get Role failed. Status: {(int)response.StatusCode} " +
                    $"{response.StatusCode}. API message: {errorMessage}");
            }

            var role = response.Content
                .ReadFromJsonAsync<RoleModel>().Result;

            if (role == null)
            {
                throw new Exception(
                    $"The API returned an empty response for Role ID {id}.");
            }

            return role;
        }

        public bool Create(RoleModel roleModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PostAsJsonAsync(ApiUrl + "api/Roles/Create", roleModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Update(RoleModel roleModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/Roles/Update/" + roleModel.RoleId, roleModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.DeleteAsync(ApiUrl + "api/Roles/Delete/" + Id).Result;

            return responces.IsSuccessStatusCode;
        }
    }
}
