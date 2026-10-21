using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<UserModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Users/GetAll").Result;

            if (!result.IsSuccessStatusCode)
            {
                return new List<UserModel>();
            }

            var UserModel = result.Content.ReadFromJsonAsync<List<UserModel>>().Result;

            return UserModel;
        }
        public UserModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Users/GetById/" + Id).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var userModel = result.Content.ReadFromJsonAsync<UserModel>().Result;

            return userModel;
        }

        public bool Create(UserModel userModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PostAsJsonAsync(ApiUrl + "api/Users/Create", userModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Update(UserModel userModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/Users/Update/" + userModel.UserId, userModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.DeleteAsync(ApiUrl + "api/Users/Delete/" + Id).Result;

            return responces.IsSuccessStatusCode;
        }
    }
}
