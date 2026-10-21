using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class RolePermissionServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RolePermissionServices(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<RolePermissionModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/RolePermission/GetAll").Result;

            if (!result.IsSuccessStatusCode)
            {
                return new List<RolePermissionModel>();
            }

            var RolePermissionModel = result.Content.ReadFromJsonAsync<List<RolePermissionModel>>().Result;

            return RolePermissionModel;
        }
        public RolePermissionModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _httpClient.GetAsync(ApiUrl + "api/RolePermission/GetById/" + Id).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var RolePermissionModel = result.Content.ReadFromJsonAsync<RolePermissionModel>().Result;

            return RolePermissionModel;
        }

        public bool Create(RolePermissionModel rolePermissionModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PostAsJsonAsync(ApiUrl + "api/RolePermission/Create", rolePermissionModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Update(RolePermissionModel rolePermissionModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.PutAsJsonAsync(ApiUrl + "api/RolePermission/Update/" + rolePermissionModel.RolePermissionId, rolePermissionModel).Result;

            return responces.IsSuccessStatusCode;
        }
        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responces = _httpClient.DeleteAsync(ApiUrl + "api/RolePermission/Delete/" + Id).Result;

            return responces.IsSuccessStatusCode;
        }
    }
}
