using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class TransactionServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransactionServices(HttpClient httpClient,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration= configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<TransactionModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/Transaction/GetAll").Result;

            if(!responce .IsSuccessStatusCode)
            {
                return new List<TransactionModel>();
            }

            var trancaction = responce.Content.ReadFromJsonAsync<List<TransactionModel>>().Result;

            return trancaction;
        }
        public TransactionModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor?.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/Transaction/GetById?Id=" + Id).Result;

            if(!responce .IsSuccessStatusCode)
            {
                return null;
            }
             
            var trancation = responce.Content.ReadFromJsonAsync<TransactionModel>().Result;

            return trancation ;
        }
        public bool Craete(TransactionModel transactionModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PostAsJsonAsync(ApiUrl + "api/Transaction/Create", transactionModel).Result;

            return responce .IsSuccessStatusCode;

        }
        public bool Update(TransactionModel transactionModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PutAsJsonAsync(ApiUrl+ "api/Transaction/Update", transactionModel).Result;

            return responce.IsSuccessStatusCode;

        }

        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.DeleteAsync(ApiUrl + "api/Transaction/Delete?Id=" +Id).Result;

            return responce.IsSuccessStatusCode;
        }
    }
}
