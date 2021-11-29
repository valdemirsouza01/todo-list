using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleverti.Todo.Application
{
    public interface ITodoProcess
    {       
        Task<Domain.Entities.Todo> GetTodoById(Guid id);
        Task<List<Domain.Entities.Todo>> GetAll();
        Task<Domain.Entities.Todo> Add(Domain.Entities.Todo obj);
        Task<Domain.Entities.Todo> Edit(Domain.Entities.Todo obj);
        Task<bool> Remove(Guid id);
    }
}
