using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// условная бд с пользователями


var builder = WebApplication.CreateBuilder();

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
    });
var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default", 
    pattern: "{action}",
    defaults: new {Controller = "Home", action = "Index"});

app.MapControllerRoute(
    name: "auth",
    pattern: "{controller}/{action}");

app.Run();

