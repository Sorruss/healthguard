using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthguard.Controllers
{
    [Route("api/files")]
    [ApiController]
    
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("upload-image")]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] FileModel fileModel)
        {
            await _fileService.Upload(fileModel);
            return Ok(new { ok = true });
        }

        [HttpGet]
        [Route("get-image")]
        public async Task<IActionResult> Get(string fileName)
        {
            var imageFileStream = await _fileService.Get(fileName);
            return File(imageFileStream, "image/png");
        }
    }
}
