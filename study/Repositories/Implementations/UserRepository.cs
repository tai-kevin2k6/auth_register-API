using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using study.Entities;
using study.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
namespace study.Repositories.Implementations;

public class UserRepository(string cn) : IUserRepository
{
    public User? ValidateUser(string username, string password)
    {
        string sql = @"SELECT   [Id]
                                ,[Username]
                                ,[Password]
                                ,[Role]
                    FROM [user-profile].[dbo].[Users]
                    WHERE [Username] = @username AND [Password] = @password";
        using var conn = new SqlConnection(cn);
        conn.Open();

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 256) { Value = username });
        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 256) { Value = password });

        using var reader = cmd.ExecuteReader();

        if (!reader.Read()) return null;

        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            Password = reader.GetString(2),
            Role = reader.GetString(3)
        };
    }

    public bool UsernameExist(string username) 
    {
        string sql = @"SELECT [Username]
                    FROM [user-profile].[dbo].[Users]
                    WHERE [Username] = @username";
        using var conn = new SqlConnection(cn);
        conn.Open();

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 256) { Value = username });
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return false; else return true;
    }
    
    public User CreateUser(string username, string password, string role = "user")
    {
        const string sql = @"INSERT INTO Users(Username, Password, Role) 
                            OUTPUT INSERTED.Id
                            VALUES(@username, @password, 'user')";
        using var conn = new SqlConnection(cn);
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 256) { Value = username });
        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 256) { Value = password });
        cmd.Parameters.Add(new SqlParameter("@role", SqlDbType.NVarChar, 256) { Value = role });
        int id = (int)cmd.ExecuteScalar();
        return new User
        {
            Id = id,
            Username = username,
            Password = password,
            Role = role
        };
    }
    public bool DeleteUser(string username)
    {
        int cnt = 0;
        const string sql = @"DELETE FROM Users WHERE Username = @username";
        using var conn = new SqlConnection(cn);
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 256) { Value = username });
        cnt = cmd.ExecuteNonQuery();
        if (cnt == 0) return false; else return true;
    }
}