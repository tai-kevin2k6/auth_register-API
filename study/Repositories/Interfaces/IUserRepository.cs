using study.Entities;
namespace study.Repositories.Interfaces;

public interface IUserRepository
{
    User? ValidateUser(string username);
    bool UsernameExist(string username);
    User CreateUser(string username, string password, string role);
    bool DeleteUser(string username);
}
