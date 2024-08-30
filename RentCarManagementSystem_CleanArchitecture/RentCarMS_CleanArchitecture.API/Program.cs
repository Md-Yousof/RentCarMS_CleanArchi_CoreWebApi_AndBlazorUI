using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Infrastructure;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
using RentCarMS_CleanArchitecture.Application;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationDependencies(); 
builder.Services.AddInfrastructure(builder.Configuration);


//builder.Services.AddDbContext<ApplicationDbContext>(options =>  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped(typeof(IMemberRepository), typeof(MemberRepository));
//builder.Services.AddTransient<IRepository<Member>, MemberRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});

app.UseStaticFiles(); //add extra it

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
