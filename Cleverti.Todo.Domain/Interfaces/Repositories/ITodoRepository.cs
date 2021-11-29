using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleverti.Todo.Domain.Interfaces.Repositories
{
    public interface ITodoRepository 
    {

        Task<Entities.Todo> GetById(Guid id);
        Task<List<Entities.Todo>> GetAll();
        Task<Entities.Todo> Add(Entities.Todo obj);
        Task<Entities.Todo> Edit(Entities.Todo obj);
        Task<bool> Remove(Guid id);

    }
}
