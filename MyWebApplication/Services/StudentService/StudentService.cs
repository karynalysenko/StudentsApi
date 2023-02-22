using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentsApi1.Data;
using StudentsApi1.Models;
//using MyWebApplication.Dtos.Student;
using System.Security.Claims;

namespace StudentsApi1.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<Student>>> AddStudent(Student newStudent)
        {
            var serviceResponse = new ServiceResponse<List<Student>>();
            Student student = _mapper.Map<Student>(newStudent);
            //student.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Students
                //.Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<Student>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Student>>> GetAllStudents()
        {
            var response = new ServiceResponse<List<Student>>();
            var dbStudents = await _context.Students
            //.Where(c => c.User.Id == GetUserId())
                .ToListAsync();
            response.Data = dbStudents.Select(c => _mapper.Map<Student>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<Student>> GetStudentById(int id)
        {
            var serviceResponse = new ServiceResponse<Student>();
            var dbStudent = await _context.Students
                //.Include(c => c.Weapon)
                //.Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.StudentId == id); //&& c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<Student>(dbStudent);
            return serviceResponse;
        }
    }
}
