using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Users.Handlers;

namespace ToDoApp.Users;

public class LoginUserCommandHandler(IUserStorage userStorage, Logger logger)
{
    public LoginResponse HandleCommand(LoginCommand command)
    {
        string? name = command.Name?.Trim();

        if (string.IsNullOrEmpty(name) || name.Length > 100)
        {
            throw new ArgumentException("Имя должно содержать от 1 до 100 символов");
        }

        User? found = userStorage.FindUserByName(name);

        if (found == null)
        {
            throw new ArgumentException("Такого имени нет");
        }

        if(command.Password != found.Password)
        {
            throw new ArgumentException("Пароль неверный.");
        }

        logger.LogInfo($"Зашёл пользователь {found.Name}");
        
        var token = GenerateToken(found);

        return new LoginResponse
        {
            Token = token
        };
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name!)
        };

        var token = new JwtSecurityToken(
            issuer: AuthConstants.Issuer,
            audience: AuthConstants.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(AuthConstants.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}