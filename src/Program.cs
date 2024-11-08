using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MedicalAPI;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddCors(options => options.AddPolicy("EnableAll", policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddControllers();
services.AddRepositories(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("EnableAll");
app.UseHttpsRedirection();
app.MapControllers();

app.MapControllers();

app.Run();