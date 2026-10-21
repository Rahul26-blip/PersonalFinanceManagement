using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class PermissionServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PermissionServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<PermissionModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Permission/GetAll").Result;

            if (!result.IsSuccessStatusCode)
            {
                return new List<PermissionModel>();
            }

            var PermissionModel = result.Content.ReadFromJsonAsync<List<PermissionModel>>().Result;

            return PermissionModel;
        }
        public PermissionModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/Permission/GetById/" + Id).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var PermissionModel = result.Content.ReadFromJsonAsync<PermissionModel>().Result;

            return PermissionModel;
        }

        public bool Create(PermissionModel permissionModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PostAsJsonAsync(ApiUrl + "api/Permission/Create", permissionModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Update(PermissionModel permissionModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/Permission/Update/" + permissionModel.PermissionId, permissionModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.DeleteAsync(ApiUrl + "api/Permission/Delete/" + Id).Result;

            return responces.IsSuccessStatusCode;
        }
    }
}
