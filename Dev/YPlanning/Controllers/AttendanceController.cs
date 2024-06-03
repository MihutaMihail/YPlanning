using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Interfaces;
using YPlanning.Dto;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceDto>))]
        public IActionResult GetAttendances() 
        {
            var attendancesDto = _mapper.Map<List<AttendanceDto>>(_attendanceRepository.GetAttendances());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(attendancesDto);
        }

        [HttpGet("{userId}/classes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetClassesByUserId(int userId)
        {
            var classes = _mapper.Map<List<ClassDto>>(_attendanceRepository.GetClassesByUserId(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(classes);
        }

        [HttpGet("{classId}/users")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByClassId(int classId)
        {
            var users = _mapper.Map<List<UserDto>>(_attendanceRepository.GetUsersByClassId(classId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
    }
}
