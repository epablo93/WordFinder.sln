using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordFinderController : ControllerBase
    {
        private readonly WordFinderService _wordFinderService;

        public WordFinderController(WordFinderService wordFinderService)
        {
            _wordFinderService = wordFinderService;
        }

        [HttpPost("find")]
        public IActionResult Find([FromBody] WordFinderRequest request)
        {
            var result = _wordFinderService.Find(request.Matrix, request.WordStream);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Words);
        }
    }
}
