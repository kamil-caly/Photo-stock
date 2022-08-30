using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PhotoDto>> Get()
        {
            var photos = photoService.GetAll();

            return Ok(photos);
        }

        [HttpGet("{id}")]
        public ActionResult<PhotoDto> Get([FromRoute] int id)
        {
            var photo = photoService.GetById(id);

            return Ok(photo);
        }

    }
}
