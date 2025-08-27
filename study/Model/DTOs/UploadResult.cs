using System.Runtime.CompilerServices;

namespace study.Model.DTOs;

public record UploadResult (string FileName, string Url, long size);