using System;
 

namespace Cleverti.Todo.Application.ViewModel
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDone { get; set; }

    }
}
