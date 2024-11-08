using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MedicalAPI;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("secret.json")
});

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
app.UseCors("EnableAll");
app.UseHttpsRedirection();

app.Run();