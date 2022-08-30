using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/author/{authorId}/text")]
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

        [HttpPost]
        public ActionResult Create([FromRoute] int authorId, [FromBody] CreateTextDto dto)
        {
            var newTextId = textService.Create(dto, authorId);

            return Created($"api/author/{authorId}/text/{newTextId}", null);
        }
    }
}
