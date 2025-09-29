using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorStandAlone.Models;
using System.Collections.Generic;

namespace BlazorStandAlone.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _http;
        public EmployeeService(HttpClient http) => _http = http;

        // GET all employees
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Employee>>("api/Employees");
            return list ?? new List<Employee>();
        }

        // POST new employee
        public async Task<Employee> AddEmployeeAsync(AddEmployeeDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Employees", dto);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<Employee>();
            return created!;
        }

        // PUT update existing employee
        public async Task<Employee?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/Employees/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Employee>();
            }

            return null;
        }

        // DELETE employee
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/Employees/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
