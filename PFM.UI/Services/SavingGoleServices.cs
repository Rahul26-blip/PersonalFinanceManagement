using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class SavingGoleServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public SavingGoleServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpcontextAccessor = contextAccessor;
        }

        public List<SavingGoleModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/SavingGole/GetAll").Result;
           

            if (!responce.IsSuccessStatusCode)
            {
                return new List<SavingGoleModel>();
            }
            var savingGole = responce.Content.ReadFromJsonAsync<List<SavingGoleModel>>().Result;

            return savingGole;
        }
        public SavingGoleModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/SavingGole/GetById/" + Id).Result;

            if (!responce.IsSuccessStatusCode)
            {
                return null;
            }
            var savingGole = responce.Content.ReadFromJsonAsync<SavingGoleModel>().Result;

            return savingGole;
        }
        public bool Create(SavingGoleModel savingGolemodel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PostAsJsonAsync(ApiUrl + "api/SavingGole/Create", savingGolemodel).Result;

            return responce.IsSuccessStatusCode;
        }
        public bool Update(SavingGoleModel savingGolemodel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PutAsJsonAsync(ApiUrl + "api/SavingGole/Update/" + savingGolemodel.SavingsGolesId,savingGolemodel).Result;

            return responce.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.DeleteAsync(ApiUrl + "api/SavingGole/Delete/" + Id).Result;

            return responce.IsSuccessStatusCode;
        }

    }
}
