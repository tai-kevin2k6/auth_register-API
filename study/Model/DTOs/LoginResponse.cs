namespace study.Model.DTOs;

public class LoginResponse
{
    public string AccessToken { get; set; } = default!;
    public string Role { get; set; } = default!;
}
