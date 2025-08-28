using study.Model.DTOs;
namespace study.Services.Interfaces;
public interface IAuthService
{
    LoginResponse? Login(string username, string password);
    bool Register(string username, string password);
    bool Delete(string username);
}
