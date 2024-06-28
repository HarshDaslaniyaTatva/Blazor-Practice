using BlazorWebApp.Components;
using BlazorWebApp.Services.Implementation;
using BlazorWebApp.Services.Interface;
using BlazorWebApp.Stores;
using BlazorWebApp.Stores.CounterStore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<CounterStore>();
builder.Services.AddScoped<IActionDispatcher, ActionDispatcher>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped(hc => new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7084/")
});
builder.Services.AddMudServices();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
