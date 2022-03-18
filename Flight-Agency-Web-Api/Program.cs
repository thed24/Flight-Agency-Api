using Flight_Agency_Api.Features.Authorization.Services;

var builder = WebApplication.CreateBuilder(args);

var context = new UserContext();
context.Database.EnsureCreated();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(service => service.AddDefaultPolicy(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader())
);

builder.Services.AddSingleton(context);
builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddSingleton<ITripsService, TripsService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.Urls.Add("http://+:8080");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
