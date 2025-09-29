////using BlazorStandAlone.Services;
////using CRUD_Employee_standAlone;
////using CRUD_Employee_standAlone.Services;
////using Microsoft.AspNetCore.Components.Authorization;
////using Microsoft.AspNetCore.Components.Web;
////using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
////using Microsoft.Extensions.DependencyInjection;
////using System;
////using System.Net.Http;
////using System.Threading.Tasks;

////var builder = WebAssemblyHostBuilder.CreateDefault(args);
////builder.RootComponents.Add<App>("#app");

////// HttpClient registration
////builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7118/") });

////// Register your existing services
////builder.Services.AddScoped<EmployeeService>();

////// Register authentication services BEFORE AuthenticationStateProvider
////builder.Services.AddScoped<AuthService>();
////builder.Services.AddScoped<CustomAuthStateProvider>();
////builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());

////// Add authorization services
////builder.Services.AddAuthorizationCore();

////await builder.Build().RunAsync();

//using BlazorStandAlone.Services;
//using CRUD_Employee_standAlone;
//using CRUD_Employee_standAlone.Services;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");

//// HttpClient registration
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7118/") });

//// Register your existing services
//builder.Services.AddScoped<EmployeeService>();

//// Register authentication services BEFORE AuthenticationStateProvider
//builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<CustomAuthStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());

//// Add authorization services
//builder.Services.AddAuthorizationCore();

//await builder.Build().RunAsync();

// using BlazorStandAlone.Services;
// using CRUD_Employee_standAlone;
// using CRUD_Employee_standAlone.Services;
// using Microsoft.AspNetCore.Components.Authorization;
// using Microsoft.AspNetCore.Components.Web;
// using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Net.Http;
// using System.Threading.Tasks;

// var builder = WebAssemblyHostBuilder.CreateDefault(args);
// builder.RootComponents.Add<App>("#app");

// // HttpClient registration
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:2030/") });
// // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.11.39:2040/") });

// // Register your existing services
// builder.Services.AddScoped<EmployeeService>();

// // Register authentication services BEFORE AuthenticationStateProvider
// builder.Services.AddScoped<AuthService>();
// builder.Services.AddScoped<CustomAuthStateProvider>();
// builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());

// // Add Category service - NEW
// builder.Services.AddScoped<CategoryService>();

// // Add authorization services
// builder.Services.AddAuthorizationCore();

// await builder.Build().RunAsync();

using BlazorStandAlone.Services;
using CRUD_Employee_standAlone;
using CRUD_Employee_standAlone.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// ✅ API Base Address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:2030/") });

// ✅ Services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AuthService>();

// ✅ Authentication & Authorization
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddAuthorizationCore();

// ✅ Run app
await builder.Build().RunAsync();