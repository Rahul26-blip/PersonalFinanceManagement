using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class AccountServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<AccountModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            // Get JWT token from Session
            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            // Add JWT token to request
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers. AuthenticationHeaderValue("Bearer", token);

            // Call API
            var response = _httpClient.GetAsync(ApiUrl + "api/Account/GetAll").Result;


            if (!response.IsSuccessStatusCode)
            {
                return new List<AccountModel>();
            }

            var accounts = response.Content.ReadFromJsonAsync<List<AccountModel>>().Result;

            return accounts;
        }

        public AccountModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/Account/GetById/" + Id).Result;

            if (!responce.IsSuccessStatusCode)
            {
                return null;
            }

            var account = responce.Content.ReadFromJsonAsync<AccountModel>().Result;

            return account;
        }

        public bool Create(AccountModel accountmodel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PostAsJsonAsync(ApiUrl + "api/Account/Create", accountmodel).Result;

            return responce.IsSuccessStatusCode;
        }

        public bool Update(AccountModel accountmodel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/Account/Update/" + accountmodel.AccountId, accountmodel).Result;


            return responces.IsSuccessStatusCode;
        }

        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.DeleteAsync(ApiUrl + "api/Account/Delete/" + Id).Result;

            return responce.IsSuccessStatusCode;
        }




    }
}
