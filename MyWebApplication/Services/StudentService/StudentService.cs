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
        public async Task<ServiceResponse<List<Student>>> DeleteStudent(int id)
        {
            ServiceResponse<List<Student>> response = new ServiceResponse<List<Student>>();
            try
            {
                Student student = await _context.Students
                    .FirstOrDefaultAsync(c => c.StudentId == id); //&& c.User.Id == GetUserId());
                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    response.Data = _context.Students
                        //.Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<Student>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Student not found";
                }
            }
                
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<Student>> UpdateStudent(Student updatedStudent)
        {
            ServiceResponse<Student> response = new ServiceResponse<Student>();
            try
            {
                var student = await _context.Students
                    //.Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.StudentId == updatedStudent.StudentId);
                //if (student.User.Id == GetUserId())
                //{
                //    student.FirstName = updatedStudent.FirstName;
                //    student.LastName = updatedStudent.LastName;
                //    student.Email = updatedStudent.Email;
                //    student.Phone = updatedStudent.Phone;
                //    student.University = updatedStudent.University;
                //    student.Course = updatedStudent.Course;
                //    await _context.SaveChangesAsync();


                //    response.Data = _mapper.Map<Student>(student);
                //}
                //else 
                //{
                //    response.Success = false;
                //    response.Message = "Student not found.";
                //}

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        //public async Task<ServiceResponse<Student>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        //{
        //    var response = new ServiceResponse<Student>();
        //    try
        //    {
        //        var character = await _context.Characters
        //            .Include(c => c.Weapon)
        //            .Include(c => c.Skills)
        //            .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
        //            c.User.Id == GetUserId());

        //        if (character == null)
        //        {
        //            response.Success = false;
        //            response.Message = "Character not found.";
        //            return response;
        //        }

        //        var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
        //        if (skill == null)
        //        {
        //            response.Success = false;
        //            response.Message = "Skill not found.";
        //            return response;
        //        }
        //        character.Skills.Add(skill);
        //        await _context.SaveChangesAsync();
        //        response.Data = _mapper.Map<GetCharacterDto>(character);
        //    }

        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = ex.Message;
        //    }
        //    return response;
        //}
    }
}
