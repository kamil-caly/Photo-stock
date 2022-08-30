using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/text")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService textService;

        public TextController(ITextService textService)
        {
            this.textService = textService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TextDto>> Get()
        {
            var texts = textService.GetAll();

            return Ok(texts);
        }

        [HttpGet("saveToCsv")]
        public ActionResult GetAndSaveToCsv()
        {
            var texts = textService.GetAll();

            textService.WriteToCsvFile(texts);

            return Ok("Text saved successfuly");
        }

        [HttpPost("author/{authorId}")]
        public ActionResult Create([FromRoute] int authorId, [FromBody] CreateTextDto dto)
        {
            var newTextId = textService.Create(dto, authorId);

            return Created($"New Text id: {newTextId}", null);
        }
    }
}
