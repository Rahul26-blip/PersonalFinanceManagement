using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class DebtServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DebtServices(HttpClient httpClient,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<DebtModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl+"api/Debts/GetAll").Result;

            if(!result.IsSuccessStatusCode)
            {
                return new List<DebtModel>();
            }

            var detbs = result.Content.ReadFromJsonAsync<List<DebtModel>>().Result;

            return detbs ?? new List<DebtModel>();
        }
        public DebtModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Debts/GetById/"+Id).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var detbs = result.Content.ReadFromJsonAsync<DebtModel>().Result;

            return detbs;
        }

        public bool Create(DebtModel debtModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

           _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = _httpClient.PostAsJsonAsync(ApiUrl + "api/Debts/Create", debtModel).Result;

              return response.IsSuccessStatusCode;

         }

        public bool Update(DebtModel debtModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/Debts/Update/" + debtModel.DebtsId , debtModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.DeleteAsync(ApiUrl + "api/Debts/Delete/" + Id).Result;

            return responces.IsSuccessStatusCode;
        }

    }
}
