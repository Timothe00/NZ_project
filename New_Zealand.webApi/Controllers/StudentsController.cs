using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace New_Zealand.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "john", "jane", "Mark", "Emily", "David" };

            return Ok(studentNames);
        }
    }
}
