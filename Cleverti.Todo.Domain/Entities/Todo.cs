using System;


namespace Cleverti.Todo.Domain.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDone{ get; set; }

        public Todo Create(Todo todo)
        {
            return new Todo()
            {
                Id = todo.Id,
                Description= todo.Description,
                CreatedDate = todo.CreatedDate,
                IsDone=todo.IsDone
              
            };
        }

    }
}
