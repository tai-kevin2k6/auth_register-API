namespace study.Model.DTOs;

public class UploadOptions
{
    public string[] AllowedExtension { get; set; } = [".jpg", ".jpeg", ".png", ".webp", ".gif"];
    public int MaxFileSizeMB { get; set; } = 10;
}
