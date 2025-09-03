using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using study.Model.Entities;
using study.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
namespace study.Repositories.Implementations;

public class UserRepository(UsersContext ctx) : IUserRepository
{
    public Userprofile? ValidateUser(string username)
    {
        return ctx.Userprofiles.FirstOrDefault(u => u.Username == username);
    }

    public bool UsernameExist(string username) 
    {
        return ctx.Userprofiles.Any(u => u.Username == username);
    }
    
    public bool CreateUser(string username, string password, string role = "user")
    {
        if (UsernameExist(username)) return false;
        var user = new Userprofile()
        {
            Username = username,
            PasswordHash = password,
            Role = role
        };
        ctx.Userprofiles.Add(user);
        ctx.SaveChanges();
        return true;
    }
    public bool DeleteUser(string username)
    {
        var user = ctx.Userprofiles.FirstOrDefault(u => u.Username == username);
        if (user == null) return false;
        ctx.Userprofiles.Remove(user);
        ctx.SaveChanges();
        return true;
    }
}