using study.Model.Entities.User;
namespace study.Repositories.Interfaces;

public interface IUserRepository
{
    Userprofile? ValidateUser(string username);
    bool UsernameExist(string username);
    bool CreateUser(string username, string password, string role);
    bool DeleteUser(string username);
}
