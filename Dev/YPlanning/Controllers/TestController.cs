using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Interfaces;
using YPlanning.Dto;

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
        public IActionResult GetTest(int testId)
        {
            if (!_testRepository.TestExists(testId))
                return NotFound();

            var testDto = _mapper.Map<TestDto>(_testRepository.GetTestById(testId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(testDto);
        }
    }
}
