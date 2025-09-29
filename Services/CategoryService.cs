using CRUD_Employee_standAlone.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CRUD_Employee_standAlone.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public CategoryService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task<HttpClient> GetAuthorizedHttpClient()
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return _httpClient;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var response = await client.GetAsync("api/Category");

                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                    return categories ?? new List<Category>();
                }

                return new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting categories: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var response = await client.GetAsync($"api/Category/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Category>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting category: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryRequest request)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Category", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryRequest request)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Category/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var response = await client.DeleteAsync($"api/Category/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> GrantPermissionAsync(GrantPermissionRequest request)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Category/grant-permission", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error granting permission: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RevokePermissionAsync(int userId, int categoryId)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var response = await client.DeleteAsync($"api/Category/revoke-permission/{userId}/{categoryId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error revoking permission: {ex.Message}");
                return false;
            }
        }

        public async Task<List<UserPermissionResponse>> GetAllUserPermissionsAsync()
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var response = await client.GetAsync("api/Category/user-permissions");

                if (response.IsSuccessStatusCode)
                {
                    var permissions = await response.Content.ReadFromJsonAsync<List<UserPermissionResponse>>();
                    return permissions ?? new List<UserPermissionResponse>();
                }

                return new List<UserPermissionResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user permissions: {ex.Message}");
                return new List<UserPermissionResponse>();
            }
        }

        public async Task<UserPermissionResponse?> GetUserPermissionsAsync(int userId)
        {
            try
            {
                var client = await GetAuthorizedHttpClient();
                var response = await client.GetAsync($"api/Category/user-permissions/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserPermissionResponse>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user permissions: {ex.Message}");
                return null;
            }
        }
    }
}