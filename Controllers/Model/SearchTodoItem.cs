namespace TodoApi.Controllers
{
    public class SearchTodoItem
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public string Content { get; set; }

        public bool? IsComplete { get; set; }
    }
}