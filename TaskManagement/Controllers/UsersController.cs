using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        public UsersController(IUserService userService ,ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var newUserId = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new { id = newUserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id) return BadRequest("ID 不一致");
            var success = await _userService.UpdateAsync(user);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        //[HttpGet("{id}/Tasks")]
        //public async Task<IActionResult> GetTasksByUserId(int id, [FromQuery] bool? isCompleted)
        //{
        //    var tasks = await _taskService.GetByUserIdAsync(id, isCompleted);
        //    return Ok(tasks);
        //}

        [HttpGet("{id}/Tasks")]
        public async Task<IActionResult> GetTasksByUserId(int id,[FromQuery] bool? isCompleted,[FromQuery] int? page,[FromQuery] int? pageSize)
        {
            if (page.HasValue && pageSize.HasValue)
            {
                var result = await _taskService.GetByUserIdPagedAsync(id, isCompleted, page.Value, pageSize.Value);
                return Ok(result);
            }

            var tasks = await _taskService.GetByUserIdAsync(id, isCompleted);
            return Ok(tasks);
        }
    }
}
