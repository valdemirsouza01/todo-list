using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace Cleverti.Todo.Data
{
    public class TodoContext : ITodoContext
    {
        public TodoContext()
        {
            Todo = JsonConvert.DeserializeObject<List<Domain.Entities.Todo>>(String.IsNullOrEmpty(File.ReadAllText(@"todo.json")) ? "[]" : File.ReadAllText(@"todo.json"));
            if (null == Todo)
            {
                Todo = new List<Domain.Entities.Todo>();
            }
        }    
        public async Task SaveChangesAsync()
        {
            await File.WriteAllTextAsync("todo.json", JsonConvert.SerializeObject(Todo));
        }
        public List<Domain.Entities.Todo> Todo { get; set; }

    }
}
