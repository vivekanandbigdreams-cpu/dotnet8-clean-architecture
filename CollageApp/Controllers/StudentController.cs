using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger) {

            _logger = logger;
        }


       
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents() {

            //var student = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students) { 
            //    StudentDTO studentDTO = new StudentDTO() { 
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,

            //    };

            //    student.Add(studentDTO);
            //}
            _logger.LogInformation("Your message");
            var student = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
            });
            return Ok(student);
        }

        
        //CreatedATRoute
        //StatusCodes.Status200k

        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpPost("Create")]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model) {


            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //if (model.AddmissionDate <= DateTime.Now) 
            //{
            //    //1. Directly adding error message to models
            //    //2. Using custom Attributes 

            //    ModelState.AddModelError("AdmissionDate Error", "Addmision date must be greter than or equal to todays date");

            //    return BadRequest(ModelState);
            //}
            if (model == null)
                return BadRequest();

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
            };

            CollegeRepository.Students.Add(student);

            model.Id = student.Id;
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            //return Ok(model);

        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id) {
            // BadRequest - 400 Badrequest - Client error
            if (id <= 0) {

                _logger.LogTrace("Log message from trace method");
                _logger.LogDebug("Log message from Debug method");
                _logger.LogInformation("Log message from Information method");
                _logger.LogWarning("Log message from Warning method");
                _logger.LogError("Log message from Error method");
                _logger.LogCritical("Log message from Critical method");


                return BadRequest();
            }

            var student =  CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            // NotFound - 404 NotFound- Client error
            if (student == null) { 
                return NotFound($"The studnet with id {id} not found");
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            // OK - 200 - Success
            return Ok(studentDTO);
        }

        [HttpGet("{name}", Name = "GetStudentByName")]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {

            //BadRequest - 400 - BadRequest - Client Error
            if (string.IsNullOrEmpty(name)) 
            { 
                return BadRequest();
            }
            var student =  CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();


            // NotFound - 404 NotFound- Client error
            if (student == null)
            {
                return NotFound($"The studnet with name {name} not found");
            }
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            return Ok(studentDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            // BadRequest - 400 Badrequest - Client error
            if (id <= 0)
            {
                return BadRequest();
            }
            var student =   CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);

            // NotFound - 404 NotFound- Client error
            if (student == null)
            {
                return NotFound($"The studnet with id {id} not found");
            }
            return true;
        }
    }
}
