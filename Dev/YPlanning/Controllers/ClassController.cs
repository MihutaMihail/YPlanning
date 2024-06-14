using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Models;
using YPlanning.Interfaces.Services;
using YPlanning.Dto;
using YPlanning.Authorize;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public ClassController(IClassService classService, IMapper mapper)
        {
            _classService = classService;
            _mapper = mapper;
        }

        [HttpGet]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassDto>))]
        public IActionResult GetClasses()
        {
            var classes = _classService.GetClasses();
            var classesDto = _mapper.Map<List<ClassDto>>(classes);

            return Ok(classesDto);
        }
        
        [HttpGet("{classId:int}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(ClassDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetClassById(int? classId)
        {
            if (classId == null)
                return BadRequest("Class ID cannot be null");

            if (!_classService.DoesClassExistsById(classId))
                return NotFound();

            var _class = _classService.GetClassById(classId);
            var classDto = _mapper.Map<ClassDto>(_class);

            return Ok(classDto);
        }

        [HttpGet("subject/{subject}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetClassesBySubject(string? subject)
        {
            if (subject == null)
                return BadRequest("Subject cannot be null");

            var _class = _classService.GetClassesBySubject(subject);
            var classDto = _mapper.Map<List<ClassDto>>(_class);

            return Ok(classDto);
        }

        [HttpGet("date/{date}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetClassesByDate(DateTime? date)
        {
            if (date == null)
                return BadRequest("Date cannot be null");

            date = date?.ToUniversalTime();

            var _class = _classService.GetClassesByDate(date);
            var classDto = _mapper.Map<List<ClassDto>>(_class);

            return Ok(classDto);
        }


        [HttpPost]
        [AuthorizeRole("admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateClass([FromBody] ClassDto classCreate)
        {
            if (classCreate == null)
                return BadRequest("Class cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (_classService.DoesClassDtoExists(classCreate))
            {
                ModelState.AddModelError("", "Class already exists");
                return Conflict(ModelState);
            }
            
            classCreate.ClassDate = classCreate.ClassDate?.ToUniversalTime();
           
            var classMap = _mapper.Map<Class>(classCreate);
            if (!_classService.CreateClass(classMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Class successfully created");
        }

        [HttpPut("{classId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateClass(int? classId, [FromBody] ClassDto updatedClass)
        {
            if (classId == null)
                return BadRequest("Class ID cannot be null");

            if (updatedClass == null)
                return BadRequest("Class cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_classService.DoesClassExistsById(classId))
                return NotFound();

            updatedClass.ClassDate = updatedClass.ClassDate?.ToUniversalTime();
            
            var classMap = _mapper.Map<Class>(updatedClass);
            classMap.Id = classId ?? -1;

            if (!_classService.UpdateClass(classMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{classId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassById(int? classId)
        {
            if (classId == null)
                return BadRequest("Class ID cannot be null");
            
            if (!_classService.DoesClassExistsById(classId))
                return NotFound();

            if (!_classService.DeleteClassById(classId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the class");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("subject/{subject}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassesBySubject(string? subject)
        {
            if (subject == null)
                return BadRequest("Subject cannot be null");

            if (!_classService.DeleteClassesBySubject(subject))
            {
                ModelState.AddModelError("", "Something went wrong deleting the class");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("date/{date}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassesByDate(DateTime? date)
        {
            if (date == null)
                return BadRequest("Date cannot be null");

            date = date?.ToUniversalTime();

            if (!_classService.DeleteClassesByDate(date))
            {
                ModelState.AddModelError("", "Something went wrong deleting the class");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
