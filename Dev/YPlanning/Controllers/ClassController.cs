using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Interfaces;
using YPlanning.Dto;

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
        public IActionResult GetClass(int classId)
        {
            if (!_classRepository.ClassExists(classId))
                return NotFound();

            var classDto = _mapper.Map<ClassDto>(_classRepository.GetClassById(classId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(classDto);
        }
    }
}
