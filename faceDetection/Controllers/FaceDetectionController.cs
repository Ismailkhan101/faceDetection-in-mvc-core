using faceDetection.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace FaceDetectionMVC.Controllers
{
    public class FaceDetectionController : Controller
    {
        private readonly FaceDetectionService _faceDetectionService;

        public FaceDetectionController(FaceDetectionService faceDetectionService)
        {
            _faceDetectionService = faceDetectionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetectFaces(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return Content("File not selected");

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();
                var result = _faceDetectionService.DetectFaces(imageData);
               return File(result, "image/png", "detected_faces.png");
            }
        }
        public async Task<IActionResult> RealtimeFacedetection()
        {
            return View();
        }
        public async Task<IActionResult> RealDetectFaces()
        {
            return View();
        }
            [HttpPost]
        public async Task<IActionResult> RealDetectFaces(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded");

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();

                // Call your face detection logic here
                var faces = _faceDetectionService.RealDetectFaces(imageData); // Assume it returns a list of rectangles

                return Json(new { faces });
            }
        }

    }
}