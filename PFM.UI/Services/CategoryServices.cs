using PFM.UI.Models;

namespace PFM.UI.Services
{
    public class CategoryServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public CategoryServices(HttpClient httpClient, IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpcontextAccessor = httpContextAccessor;
        }

        public List<CategoryModel> GetAll()
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = _httpClient.GetAsync(ApiUrl + "api/Categorie/GetAll").Result;

            if (!response.IsSuccessStatusCode)
            {
                return new List<CategoryModel>();
            }

            var category = response.Content.ReadFromJsonAsync<List<CategoryModel>>().Result;

            return category;

        }

        public CategoryModel GetById(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.GetAsync(ApiUrl + "api/Categorie/GetById/" + Id).Result;

            if(!responce.IsSuccessStatusCode)
            {
                return null;
            }
            var category = responce.Content.ReadFromJsonAsync<CategoryModel>().Result;

            return category;
        }

        public bool Create(CategoryModel categoryModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.PostAsJsonAsync(ApiUrl + "api/Categorie/Create", categoryModel).Result;

            return responce.IsSuccessStatusCode;

        }

        public bool Update(CategoryModel categoryModel)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = _httpClient.PutAsJsonAsync(ApiUrl + "api/Categorie/Update?Id=" + categoryModel.CategoriesId, categoryModel).Result;

            return response.IsSuccessStatusCode;
        }

        public bool Delete(int Id)
        {
            var ApiUrl = _configuration["ApiSettings:APIUrl"];

            var token = _httpcontextAccessor.HttpContext?.Session.GetString("token");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responce = _httpClient.DeleteAsync(ApiUrl + "api/Categorie/Delete/" + Id).Result;

            return responce.IsSuccessStatusCode;  
        }
       
    }
}
