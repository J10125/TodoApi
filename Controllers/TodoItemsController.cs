using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.Model;

namespace TodoApi.Controllers
{
    // 使用 DI 將資料庫內容 (TodoContext) 插入到控制器中。 控制器中的每一個 CRUD 方法都會使用資料庫內容。

    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItem()
        {
            List<TodoItem> todoItems = _context.TodoItems.ToList();
            var todoItemList = todoItems;

            return todoItemList;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        // GET: api/UpdateTodoItem/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodoItem(long id, TodoItem todoItem)
        {
            var checkTtodoItem = _context.TodoItems.Find(id);
            if (checkTtodoItem == null)
                return NotFound();
            checkTtodoItem.Name = todoItem.Name;
            checkTtodoItem.Content = todoItem.Content;
            checkTtodoItem.IsComplete = todoItem.IsComplete;
            _context.SaveChanges();
            return checkTtodoItem;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var checkTtodoItem = _context.TodoItems.Find(id);

            if (checkTtodoItem == null)
                return NotFound();

            _context.TodoItems.Remove(checkTtodoItem);
            _context.SaveChanges();

            return Ok();
        }

        // POST: api/TodoItems/Search
        [HttpPost("Search")]
        public async Task<ActionResult<List<TodoItem>>> SearchTodoItem(SearchTodoItem todoItem)
        {
            var todoItemList = _context.TodoItems.ToList();
            if(!String.IsNullOrEmpty(todoItem.Name)) todoItemList = todoItemList.Where(x => x.Name == todoItem.Name).ToList();
            if(!String.IsNullOrEmpty(todoItem.Content)) todoItemList = todoItemList.Where(x => x.Content.Contains(todoItem.Content)).ToList();
            if(todoItem.IsComplete != null) todoItemList = todoItemList.Where(x => x.IsComplete == todoItem.IsComplete).ToList();         

            return todoItemList;
        }

    }
}
