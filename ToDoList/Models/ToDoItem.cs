namespace ToDoList.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public DateTime Deadline { get; set; }
        public string? FilePath { get; set; }
    }
}
