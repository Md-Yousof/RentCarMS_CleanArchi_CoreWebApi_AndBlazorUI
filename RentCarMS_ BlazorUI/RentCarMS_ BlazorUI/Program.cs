using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RentCarMS__BlazorUI.Data;
using RentCarMS__BlazorUI.Services.Cars;
using RentCarMS__BlazorUI.Services.DuePayments;
using RentCarMS__BlazorUI.Services.Members;
using RentCarMS__BlazorUI.Services.Payments;
using RentCarMS__BlazorUI.Services.RentCarDetails;
using RentCarMS__BlazorUI.Services.RentCars;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var baseUrl = new Uri("https://localhost:7259/");

builder.Services.AddHttpClient<ICarService, CarService>(client =>{
    client.BaseAddress = baseUrl;
});
builder.Services.AddHttpClient<IMemberService, MemberService>(client => {
    client.BaseAddress = baseUrl;
});

builder.Services.AddHttpClient<IRentCar, RentCarService>(client => {
    client.BaseAddress = baseUrl;
});

builder.Services.AddHttpClient<IRenCareDetailService, RentCarDetailService>(client => {
    client.BaseAddress = baseUrl;
});

builder.Services.AddHttpClient<IPaymentService, PaymentService>(client => {
    client.BaseAddress = baseUrl;
});

builder.Services.AddHttpClient<IDuePayment, DuePaymentService>(client => {
    client.BaseAddress = baseUrl;
});

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
