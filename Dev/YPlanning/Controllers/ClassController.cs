using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Interfaces;
using YPlanning.Dto;
using YPlanning.Models;
using YPlanning.Repository;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassController(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassDto>))]
        public IActionResult GetClasses()
        {
            var classesDto = _mapper.Map<List<ClassDto>>(_classRepository.GetClasses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(classesDto);
        }

        [HttpGet("{classId}")]
        [ProducesResponseType(200, Type = typeof(ClassDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetClass(int classId)
        {
            if (!_classRepository.ClassExists(classId))
                return NotFound();

            var classDto = _mapper.Map<ClassDto>(_classRepository.GetClassById(classId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(classDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateClass([FromBody] ClassDto classCreate)
        {
            if (classCreate == null)
                return BadRequest("Class cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingClass = _classRepository.GetClasses()
                .Where(c =>
                    c.Subject == classCreate.Subject &&
                    c.ClassDate == classCreate.ClassDate &&
                    c.StartTime == classCreate.StartTime &&
                    c.EndTime == classCreate.EndTime &&
                    c.Room == classCreate.Room)
                .FirstOrDefault();

            if (existingClass != null)
            {
                ModelState.AddModelError("", "Class already exists");
                return Conflict(ModelState);
            }

            classCreate.ClassDate = classCreate.ClassDate?.ToUniversalTime();
            var classMap = _mapper.Map<Class>(classCreate);

            if (!_classRepository.CreateClass(classMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Class successfully created");
        }
    }
}
