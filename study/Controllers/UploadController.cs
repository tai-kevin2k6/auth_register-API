using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using study.Model.DTOs;
using study.Services.Interfaces;

namespace study.Controllers;


[ApiController]
[Route("")]
[AllowAnonymous]
public class UploadController(IFileService fileService) : ControllerBase
{

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadResult>> Upload(IFormFile file)
    {
        try
        {
            var result = await fileService.SaveImageAsync(file, HttpContext);
            return Ok(result);
        }
        catch (InvalidOperationException ex) //validate
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex) // empty file
        {
            return BadRequest(new { error = "empty file" });
        }
        catch (Exception ex) // trùng tên (hiếm) / lỗi IO
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }
}
