using study.Repositories.Interfaces;
using study.Repositories.Implementations;
using study.Model.DTOs;
using study.Helpers;
using study.Services.Interfaces;
namespace study.Services.Implementations;
public class AuthService(IUserRepository users, JwtHelper jwt) : IAuthService
{
    public LoginResponse? Login(string username, string password)
    {
        var user = users.ValidateUser(username);
        if (user is null) return null;
        var hash = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!hash) return null;
        var token = jwt.CreateJwt(user.Id, user.Username, user.Role);
        return new LoginResponse { AccessToken = token, Role = user.Role };
    }
    
    public bool Register(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
        if (users.UsernameExist(username)) return false;

        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        users.CreateUser(username, hash, "user");
        return true;
    }

    public bool Delete(string username) 
    {
        if (string.IsNullOrEmpty(username) || !users.UsernameExist(username)) return false;
        users.DeleteUser(username);
        return true;
    }
}