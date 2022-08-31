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
        public ActionResult<IEnumerable<PhotoDto>> Get([FromQuery] ItemQuery query)
        {
            var photos = photoService.GetAll(query);

            return Ok(photos);
        }

        [HttpGet("{id}")]
        public ActionResult<PhotoDto> Get([FromRoute] int id)
        {
            var photo = photoService.GetById(id);

            return Ok(photo);
        }

        [HttpGet("calculateAverage")]
        public ActionResult CalculateAverage()
        {
            var average = photoService.calculateAverage();

            return Ok($"Average of photo's rating = {average}");
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdatePhotoDto updatePhotoDto, [FromRoute] int id)
        {
            photoService.UpdatePhoto(updatePhotoDto, id);

            return Ok();
        }

    }
}
