using Microsoft.Extensions.Validation;
using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class BudgetServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BudgetServices (HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<BudgetModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl+ "api/Budget/GetAll").Result;

            if(!responce.IsSuccessStatusCode)
            {
                return new List<BudgetModel>();
            }

            var budgetModel = responce.Content.ReadFromJsonAsync<List<BudgetModel>>().Result;

            return budgetModel;

        }
        public BudgetModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue ("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/Budget/GetById/" + Id).Result;

            if(!responce.IsSuccessStatusCode)
            {
                return null;
            }

            var budgetModel = responce.Content.ReadFromJsonAsync<BudgetModel>().Result;

            return budgetModel;

        }

        public bool Create(BudgetModel budgetModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PostAsJsonAsync(ApiUrl+ "api/Budget/Create", budgetModel).Result;

            return responce.IsSuccessStatusCode;
        }
        public bool Update(BudgetModel budgetModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PutAsJsonAsync(ApiUrl + "api/Budget/Update?Id="+budgetModel.BudgetId, budgetModel).Result;

            return responce.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.DeleteAsync(ApiUrl + "api/Budget/Delete/" + Id).Result;

            return responce.IsSuccessStatusCode;
        }

    }

}
