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
        var user = users.ValidateUser(username, password);
        if (user is null) return null;

        var token = jwt.CreateJwt(user.Id, user.Username, user.Role);
        return new LoginResponse { AccessToken = token, Role = user.Role };
    }
    
    public bool Register(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
        if (users.UsernameExist(username)) return false;
        users.CreateUser(username, password, "user");
        return true;
    }

    public bool Delete(string username) 
    {
        if (string.IsNullOrEmpty(username) || !users.UsernameExist(username)) return false;
        users.DeleteUser(username);
        return true;
    }
}

