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
        [ProducesResponseType(404)]
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
        [ProducesResponseType(404)]
        public IActionResult GetUsersByClassId(int classId)
        {
            var users = _mapper.Map<List<UserDto>>(_attendanceRepository.GetUsersByClassId(classId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateAttendance([FromBody] AttendanceDto attendanceCreate)
        {
            if (attendanceCreate == null)
                return BadRequest("Attendance cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAttendance = _attendanceRepository.GetAttendances()
                .Where(at => at.ClassId == attendanceCreate.ClassId && at.UserId == attendanceCreate.UserId)
                .FirstOrDefault();

            if (existingAttendance != null)
            {
                ModelState.AddModelError("", "Attendance already exists");
                return Conflict(ModelState);
            }

            var attendanceMap = _mapper.Map<Attendance>(attendanceCreate);

            if (!_attendanceRepository.CreateAttendance(attendanceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Attendance successfully created");
        }

        [HttpPut("{classId}/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAttendance(int classId, int userId, [FromBody] AttendanceDto updatedAttendance)
        {
            if (updatedAttendance == null)
                return BadRequest("Attendance cannot be null");

            if (updatedAttendance.ClassId != 0 && classId != updatedAttendance.ClassId)
                return BadRequest("Class ids are not matching");
            if (updatedAttendance.UserId != 0 && userId != updatedAttendance.UserId)
                return BadRequest("User ids are not matching");

            if (!_attendanceRepository.AttendanceExists(classId, userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            updatedAttendance.ClassId = classId;
            updatedAttendance.UserId = userId;

            var attendanceMap = _mapper.Map<Attendance>(updatedAttendance);

            if (!_attendanceRepository.UpdateAttendance(attendanceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
