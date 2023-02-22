using Microsoft.AspNetCore.Mvc;
using StudentsApi1.Services.StudentService;
//using MyWebApplication.Dtos.Character;
//using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace StudentsApi1.Controllers
{
//[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public IStudentService StudentService { get; }

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Student>>>> Get()
        {
            var response = await _studentService.GetAllStudents();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<Student>>>> Delete(int id)
        {
            var response = await _studentService.DeleteStudent(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetSingle(int id)
        {
            return Ok(await _studentService.GetStudentById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Student>>>> AddStudent(Student newStudent)
        {
            return Ok(await _studentService.AddStudent(newStudent));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Student>>> UpdateStudent(Student updatedStudent)
        {
            var response = await _studentService.UpdateStudent(updatedStudent);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
