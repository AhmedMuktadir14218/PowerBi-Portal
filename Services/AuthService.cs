//using System.Net.Http.Json;
//using System.Text.Json;
//using CRUD_Employee_standAlone.Models;
//using Microsoft.JSInterop;

//namespace CRUD_Employee_standAlone.Services
//{
//    public class AuthService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly IJSRuntime _jsRuntime;

//        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
//        {
//            _httpClient = httpClient;
//            _jsRuntime = jsRuntime;
//        }

//        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
//        {
//            try
//            {
//                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginRequest);

//                if (response.IsSuccessStatusCode)
//                {
//                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
//                    if (loginResponse != null)
//                    {
//                        await SaveTokenAsync(loginResponse.Token);
//                        await SaveRoleAsync(loginResponse.Role);
//                        return loginResponse;
//                    }
//                }
//                return null;
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        public async Task LogoutAsync()
//        {
//            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
//            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userRole");
//        }

//        public async Task<string?> GetTokenAsync()
//        {
//            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
//        }

//        public async Task<string?> GetRoleAsync()
//        {
//            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userRole");
//        }

//        private async Task SaveTokenAsync(string token)
//        {
//            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
//        }

//        private async Task SaveRoleAsync(string role)
//        {
//            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userRole", role);
//        }

//        public async Task<bool> IsAuthenticatedAsync()
//        {
//            var token = await GetTokenAsync();
//            return !string.IsNullOrEmpty(token);
//        }
//    }
//}


using System.Net.Http.Json;
using System.Text.Json;
using CRUD_Employee_standAlone.Models;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace CRUD_Employee_standAlone.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    if (loginResponse != null)
                    {
                        await SaveTokenAsync(loginResponse.Token);
                        await SaveRoleAsync(loginResponse.Role);
                        return loginResponse;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerRequest);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserResponse?> GetProfileAsync()
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync("api/Auth/profile");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserResponse>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<UserLogin>?> GetLoginHistoryAsync()
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync("api/Auth/logins");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserLogin>>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateProfileAsync(UpdateUserRequest updateRequest)
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync("api/Auth/profile", updateRequest);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Admin methods
        public async Task<List<UserResponse>?> GetAllUsersAsync()
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync("api/Auth/users");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserResponse>>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync($"api/Auth/user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserResponse>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest updateRequest)
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync($"api/Auth/user/{id}", updateRequest);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.DeleteAsync($"api/Auth/user/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userRole");
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        public async Task<string?> GetRoleAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userRole");
        }

        private async Task SaveTokenAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
        }

        private async Task SaveRoleAsync(string role)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userRole", role);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}