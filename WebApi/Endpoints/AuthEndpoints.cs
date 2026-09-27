using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Endpoints;

public class AuthEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login",
                async ([FromBody] LoginRequest request, IConfiguration configuration, HttpContext httpContext) =>
                {
                    var email = configuration["Auth:Email"] ?? throw new ArgumentNullException();
                    var password = configuration["Auth:Password"] ?? throw new ArgumentNullException();

                    if (request.Email != email || request.Password != password)
                        return Results.Unauthorized();

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email, request.Email),
                        new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    await httpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return Results.Ok("Успешный вход");
                })
            .WithTags("auth")
            .AllowAnonymous()
            .WithName("login")
            .WithSummary("Авторизация пользователя")
            .WithDescription("Создает cookie аутентификации.")
            .Produces<string>()
            .Produces(StatusCodes.Status401Unauthorized);
    }
}