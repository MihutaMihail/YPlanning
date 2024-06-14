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
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceService attendanceService, IMapper mapper)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
        }

        [HttpGet]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceDto>))]
        public IActionResult GetAttendances() 
        {
            var attendances = _attendanceService.GetAttendances();
            var attendancesDto = _mapper.Map<List<AttendanceDto>>(attendances);
            
            return Ok(attendancesDto);
        }

        [HttpGet("{attendanceId:int}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(AttendanceDto))]
        public IActionResult GetAttendanceById(int? attendanceId)
        {
            if (attendanceId == null)
                return BadRequest("Attendance ID cannot be null");

            if (!_attendanceService.DoesAttendanceExistById(attendanceId))
                return NotFound();

            var attendance = _attendanceService.GetAttendanceById(attendanceId);
            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);

            return Ok(attendanceDto);
        }

        [HttpGet("{classId:int}/{userId:int}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(AttendanceDto))]
        public IActionResult GetAttendanceByClassAndUserId(int? classId, int? userId)
        {
            if (classId == null || userId == null)
                return BadRequest("Class / User ID cannot be null");
            
            if (!_attendanceService.DoesAttendanceExistByClassAndUserId(classId, userId))
                return NotFound();

            var attendance = _attendanceService.GetAttendanceByClassAndUserId(classId, userId);
            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);

            return Ok(attendanceDto);
        }
        
        [HttpGet("{userId:int}/classes")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetClassesByUserId(int? userId)
        {
            if (userId == null)
                return BadRequest("User ID cannot be null");

            var classes = _attendanceService.GetClassesByUserId(userId);
            var classesDto = _mapper.Map<List<ClassDto>>(classes);
           
            return Ok(classesDto);
        }

        [HttpGet("{classId:int}/users")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUsersByClassId(int? classId)
        {
            if (classId == null)
                return BadRequest("Class ID cannot be null");
            
            var users = _attendanceService.GetUsersByClassId(classId);
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpPost]
        [AuthorizeRole("admin")]
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

            if (_attendanceService.DoesAttendanceExistByClassAndUserId(attendanceCreate.ClassId, attendanceCreate.UserId))
            {
                ModelState.AddModelError("", "Attendance already exists");
                return Conflict(ModelState);
            }

            var attendanceMap = _mapper.Map<Attendance>(attendanceCreate);
            if (!_attendanceService.CreateAttendance(attendanceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Attendance successfully created");
        }

        [HttpPut("{attendanceId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAttendance(int? attendanceId, [FromBody] AttendanceDto updatedAttendance)
        {
            if (attendanceId == null)
                return BadRequest("Attendance ID cannot be null");

            if (updatedAttendance == null)
                return BadRequest("Attendance cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_attendanceService.DoesAttendanceExistById(attendanceId))
                return NotFound();
            
            var attendanceMap = _mapper.Map<Attendance>(updatedAttendance);
            attendanceMap.Id = attendanceId ?? -1;

            if (!_attendanceService.UpdateAttendance(attendanceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{attendanceId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAttendanceById(int? attendanceId)
        {
            if (attendanceId == null)
                return BadRequest("Attendance ID cannot be null");

            if (!_attendanceService.DoesAttendanceExistById(attendanceId))
                return NotFound();

            if (!_attendanceService.DeleteAttendanceById(attendanceId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the attendance");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }

        [HttpDelete("{classId:int}/{userId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAttendanceByClassAndUserId(int? classId, int? userId)
        {
            if (classId == null || userId == null)
                return BadRequest("Class / User ID cannot be null");

            if (!_attendanceService.DoesAttendanceExistByClassAndUserId(classId, userId))
                return NotFound();
            
            if (!_attendanceService.DeleteAttendanceByClassAndUserId(classId, userId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the attendance");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }
    }
}
