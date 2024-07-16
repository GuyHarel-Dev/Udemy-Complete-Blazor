using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Data;
using BookStoreApp.Services;
using BookStoreApp.Services.Interfaces;
using BookStoreApp.Services.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<BookStoreApiClientFactory>() ;
builder.Services.AddScoped<IBookStoreAuthService, BookStoreAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider>( o => o.GetRequiredService<ApiAuthenticationStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
