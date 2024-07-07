using BlazorWebApp.Authentication;
using BlazorWebApp.Components;
using BlazorWebApp.Services.Implementation;
using BlazorWebApp.Services.Interface;
using BlazorWebApp.Stores;
using BlazorWebApp.Stores.CounterStore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<CounterStore>();
builder.Services.AddScoped<IActionDispatcher, ActionDispatcher>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped(hc => new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7084/")
});
builder.Services.AddMudServices();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
string jwtKey = "S3cr3tK3yF0rJWT@BlazorApp123456789012345678901234567890";
builder.Services.AddScoped<CustomAuthenticationStateProvider>(provider =>
    new CustomAuthenticationStateProvider(provider.GetRequiredService<IHttpContextAccessor>(), jwtKey));
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication();
// Add authorization
builder.Services.AddAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
