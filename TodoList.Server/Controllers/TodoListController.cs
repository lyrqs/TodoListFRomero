using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Interfaces.TodoList.Domain.Interfaces;
using TodoList.Server.Models;

namespace TodoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoList _todoList;
        private readonly ITodoListRepository _repo;

        public TodoListController(ITodoList todoList, ITodoListRepository repo)
        {
            _todoList = todoList;
            _repo = repo;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _repo.GetAllCategories();
            return Ok(categories);
        }


        [HttpPost("add")]
        public IActionResult AddItem([FromBody] AddTodoItemRequest request)
        {
            var id = _repo.GetNextId();
            try
            {
                _todoList.AddItem(id, request.Title, request.Description, request.Category);
                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("progress")]
        public IActionResult AddProgression([FromBody] AddProgressionRequest request)
        {
            try
            {
                _todoList.RegisterProgression(request.Id, request.Date, request.Percent);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("print")]
        public IActionResult Print()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            _todoList.PrintItems();

            var output = sw.ToString();
            return Ok(output);
        }
    }
}
