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
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestController(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestDto>))]
        public IActionResult GetTests()
        {
            var testsDto = _mapper.Map<List<TestDto>>(_testRepository.GetTests());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(testsDto);
        }

        [HttpGet("{testId}")]
        [ProducesResponseType(200, Type = typeof(TestDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTest(int testId)
        {
            if (!_testRepository.TestExists(testId))
                return NotFound();

            var testDto = _mapper.Map<TestDto>(_testRepository.GetTestById(testId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(testDto);
        }

        [HttpPost]
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

            var existingTest = _testRepository.GetTests()
                .Where(t => t.ClassId == testCreate.ClassId && t.UserId == testCreate.UserId)
                .FirstOrDefault();

            if (existingTest != null)
            {
                ModelState.AddModelError("", "Test already exists");
                return Conflict(ModelState);
            }

            var testMap = _mapper.Map<Test>(testCreate);

            if (!_testRepository.CreateTest(testMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Test succesfully created");
        }

        [HttpPut("{testId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTest(int testId, [FromBody] TestDto updatedTest)
        {
            if (updatedTest == null)
                return BadRequest("Test cannot be null");

            if (updatedTest.Id != 0 && testId != updatedTest.Id)
                return BadRequest("Ids are not matching");

            if (!_testRepository.TestExists(testId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            updatedTest.Id = testId;

            var testMap = _mapper.Map<Test>(updatedTest);

            if (!_testRepository.UpdateTest(testMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
