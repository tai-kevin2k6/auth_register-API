namespace study.Entities;
public class User
{
    public int Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!; // demo: plaintext; prod: hash + salt
    public string Role { get; set; } = "user";
}

