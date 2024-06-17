using API.DTO;
using Application;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private List<string> allowedExtensions = new List<string> { ".mp3", ".jpg", ".png", ".jpeg", ".wav", ".flac", ".aac", ".ogg" };
        private Dictionary<string, string> extensionsAndMimeTypes = new Dictionary<string, string>
        {
            { ".mp3", "audio/mpeg" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".wav", "audio/wav" },
            { ".flac", "audio/flac" },
            { ".aac", "audio/aac" },
            { ".ogg", "audio/ogg" },
        };

        private IApplicationActor _actor;
        public FilesController(IApplicationActor actor)
        {
            _actor = actor;
        }

        [Authorize]
        [HttpGet("{fileName}/checkIfExists")]
        public IActionResult GetFile(string fileName)
        {
            var path = Path.Combine("wwwroot", "temp", fileName);

            return Ok(new { exists = Path.Exists(path) });
        }

        [Authorize]
        [HttpGet("getStream")]
        public IActionResult GetFileStream([FromQuery] FileQueryStringDto query)
        {
            var filepath = Path.Combine("wwwroot", query.Folder, query.FileName);

            if (!System.IO.File.Exists(filepath))
            {
                return NotFound();
            }

            var contentType = GetContentType(filepath);

            if (!string.IsNullOrEmpty(contentType))
            {

                var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(stream, contentType);
            }

            return Ok(new { message = "Invalid file extension" });
        }

        private string GetContentType(string filepath)
        {
            var extension = Path.GetExtension(filepath);

            if (extensionsAndMimeTypes.Any(x => x.Key == extension))
            {
                return extensionsAndMimeTypes[extension];
            }
            return "";
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromForm] FileUploadDto dto)
        {
            if (_actor.AllowedUseCases.Intersect(UseCaseInfo.UseCasesThatNeedFileUpload).Count() <= 0)
            {
                return Unauthorized();
            }

            var extension = Path.GetExtension(dto.File.FileName);

            if (!allowedExtensions.Contains(extension))
            {
                return new UnsupportedMediaTypeResult();
            }

            string fileName = Guid.NewGuid().ToString() + extension;
            string savePath = Path.Combine("wwwroot", "temp", fileName);

            using var fs = new FileStream(savePath, FileMode.Create);
            dto.File.CopyTo(fs);

            return StatusCode(201, new { file = fileName });
        }
    }
}
