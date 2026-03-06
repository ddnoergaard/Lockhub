using Lockhub.Models;
using Lockhub.Services;
using Lockhub.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Config for JWT auth
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
//Generic JsonFileReader and Writer
builder.Services.AddTransient<JsonFileService<Credential>>();
builder.Services.AddTransient<JsonFileService<Organisation>>();
builder.Services.AddTransient<JsonFileService<Permissions>>();
builder.Services.AddTransient<JsonFileService<Role>>();
builder.Services.AddTransient<JsonFileService<Subscription>>();
builder.Services.AddTransient<JsonFileService<User>>();
builder.Services.AddTransient<JsonFileService<Vault>>();
//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICredentialService, CredentialService>();
builder.Services.AddScoped<IVaultService, VaultService>();
//Repo
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICredentialRepo, CredentialRepo>();
builder.Services.AddScoped<IVaultRepo, VaultRepo>();
//JWT Service
builder.Services.AddScoped<JwtService>();
//JWT Configuration -> Help from Claude
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            }
        };
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
