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
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestDto>))]
        public IActionResult GetTests() 
        {
            var tests = _testService.GetTests();
            var testsDto= _mapper.Map<List<TestDto>>(tests);
            
            return Ok(testsDto);
        }
        
        [HttpGet("{testId:int}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(TestDto))]
        public IActionResult GetTestByid(int? testId)
        {
            if (testId == null)
                return BadRequest("Test ID cannot be null");

            if (!_testService.DoesTestExistById(testId))
                return NotFound();

            var test = _testService.GetTestById(testId);
            var testDto = _mapper.Map<TestDto>(test);

            return Ok(testDto);
        }
        
        [HttpGet("{classId:int}/{userId:int}")]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(TestDto))]
        public IActionResult GetTestByClassAndUserId(int? classId, int? userId)
        {
            if (classId == null || userId == null)
                return BadRequest("Class / User ID cannot be null");
            
            if (!_testService.DoesTestExistByClassAndUserId(classId, userId))
                return NotFound();

            var test = _testService.GetTestByClassAndUserId(classId, userId);
            var testDto = _mapper.Map<TestDto>(test);
            
            return Ok(testDto);
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
            
            var classes = _testService.GetClassesByUserId(userId);
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
            
            var users = _testService.GetUsersByClassId(classId);
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpPost]
        [AuthorizeRole("admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateTest([FromBody] TestDto testCreate)
        {
            if (testCreate == null)
                return BadRequest("Test cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_testService.DoesTestExistByClassAndUserId(testCreate.ClassId, testCreate.UserId))
            {
                ModelState.AddModelError("", "Test already exists");
                return Conflict(ModelState);
            }

            var testMap = _mapper.Map<Test>(testCreate);
            if (!_testService.CreateTest(testMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Test successfully created");
        }

        [HttpPut("{testId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTest(int? testId, [FromBody] TestDto updatedtest)
        {
            if (testId == null)
                return BadRequest("Test ID cannot be null");

            if (updatedtest == null)
                return BadRequest("Test cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_testService.DoesTestExistById(testId))
                return NotFound();
            
            var testMap = _mapper.Map<Test>(updatedtest);
            testMap.Id = testId ?? -1;
            
            if (!_testService.UpdateTest(testMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{testId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTestById(int? testId)
        {
            if (testId == null)
                return BadRequest("Test ID cannot be null");

            if (!_testService.DoesTestExistById(testId))
                return NotFound();

            if (!_testService.DeleteTestById(testId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the test");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }

        [HttpDelete("{classId:int}/{userId:int}")]
        [AuthorizeRole("admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTestByClassAndUserId(int? classId, int? userId)
        {
            if (classId == null || userId == null)
                return BadRequest("Class / User ID cannot be null");

            if (!_testService.DoesTestExistByClassAndUserId(classId, userId))
                return NotFound();
            
            if (!_testService.DeleteTestByClassAndUserId(classId, userId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the test");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }
    }
}
