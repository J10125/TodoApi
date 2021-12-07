using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Core;

namespace TodoApi.Controllers
{
    public class TodoItemsController : BaseApiController
    {
        private readonly  TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }    

        // POST: api/TodoItems
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return HandleResult(Result<TodoItem>.Success(null));
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItem()
        {
            var todoItemList = _context.TodoItems.ToList();

            return HandleResult(Result<List<TodoItem>>.Success(todoItemList));
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return HandleResult<TodoItem>(null);
            }

            return HandleResult(Result<TodoItem>.Success(todoItem));
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {

           var checkTodoItem = await _context.TodoItems.FindAsync(id);

            if (checkTodoItem == null)
            {
                return HandleResult<TodoItem>(null);
            }

            checkTodoItem.IsComplete = todoItem.IsComplete;
            checkTodoItem.Name = todoItem.Name;
            checkTodoItem.Content = todoItem.Content;

            await _context.SaveChangesAsync();

            return HandleResult(Result<TodoItem>.Success(null));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return HandleResult<TodoItem>(null);
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return HandleResult(Result<TodoItem>.Success(null));
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchTodoItem(SearchTodoItem todoItem)
        {
            var todoItemList = _context.TodoItems.ToList();
            if(!String.IsNullOrEmpty(todoItem.Name)) todoItemList = todoItemList.Where(x => x.Name == todoItem.Name).ToList();
            if(!String.IsNullOrEmpty(todoItem.Content)) todoItemList = todoItemList.Where(x => x.Content.Contains(todoItem.Content)).ToList();
            if(todoItem.IsComplete != null) todoItemList = todoItemList.Where(x => x.IsComplete == todoItem.IsComplete).ToList();         

            return HandleResult(Result<List<TodoItem>>.Success(todoItemList));
        }


    }
}