using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class InvestmentServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvestmentServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<InvestmentModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Investment/GetAll").Result;

            if (!result.IsSuccessStatusCode)
            {
                return new List<InvestmentModel>();
            }

            var Investment = result.Content.ReadFromJsonAsync<List<InvestmentModel>>().Result;

            return Investment;
        }
        public InvestmentModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Investment/GetById/" + Id).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var Investment = result.Content.ReadFromJsonAsync<InvestmentModel>().Result;

            return Investment;
        }

        public bool Create(InvestmentModel investmentModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PostAsJsonAsync(ApiUrl + "api/Investment/Create", investmentModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Update(InvestmentModel investmentModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/Investment/Update/" + investmentModel.InvestmentId, investmentModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.DeleteAsync(ApiUrl + "api/Investment/Delete/" + Id).Result;

            return responces.IsSuccessStatusCode;
        }
    }
}
