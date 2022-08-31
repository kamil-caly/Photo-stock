using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get([FromQuery] ItemQuery query)
        {
            var authors = authorService.GetAll(query);

            return Ok(authors);
        }
    }
}
