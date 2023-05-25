using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application;
using TravelAgency.Application.Helpers;
using TravelAgency.Application.Services.Interfaces;
using TravelAgency.Services;
using Image = TravelAgency.Domain.Entities.Image;

namespace TravelAgency.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly UserService userService;

        public ImagesController(IImageService imageService, UserService userService)
        {
            _imageService = imageService;
            this.userService = userService;
        }

        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<Image>> AddImage([FromForm] IFormFile imageFile)
        {
            var user = await userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var addedImage = await _imageService.AddImage(imageFile, user);
            if (addedImage == null)
            {
                return BadRequest("File is not a valid image.");
            }

            return CreatedAtAction(nameof(GetImage), new { id = addedImage.Id }, addedImage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetImage(long id)
        {
            var imageData = await _imageService.GetImageData(id);
            if (imageData == null)
            {
                return NotFound("Image not found.");
            }

            return File(imageData, ImageHelpers.GetContentType(imageData));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveImage(long id)
        {
            var user = await userService.GetUserAsync(User);
            var image = await _imageService.GetImage(id);
            if (user == null || image == null)
            {
                return NotFound();
            }

            var roles = await userService.GetUserRoles(user);
            if ((image.Owner == null || image.UserId != user.Id) && !roles.Contains(Roles.AdminRoleName))
            {
                return Forbid("This image does not belong to you.");
            }

            var result = await _imageService.RemoveImage(id);
            if (!result)
            {
                return NotFound("Image not deleted.");
            }

            return NoContent();
        }
    }
}
