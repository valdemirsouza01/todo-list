using Cleverti.Todo.Application.Exceptions;
using Cleverti.Todo.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleverti.Todo.Application
{
    public class TodoProcess : ITodoProcess
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger<TodoProcess> _logger;

        public TodoProcess(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
          
        }

        public async Task<bool> Remove(Guid id)
        {
            var res = true;
            try
            {
                await GetTodoById(id);
                res = await _todoRepository.Remove(id);

            }
            catch (Exception exception)
            {
                res = false;
                throw new ApiError(500, "Internal Server error");
            }

            return res;
        }

        public async Task<Domain.Entities.Todo> GetTodoById(Guid id)
        {
            var found = _todoRepository.GetById(id);

            if (found == null)
            {
                throw new ApiError(404, $"There's no task {id}");
            }

            return await _todoRepository.GetById(id);
        }

        public async Task<List<Domain.Entities.Todo>> GetAll()

        {
            var res = await _todoRepository.GetAll();

            if (null == res)
            {
                throw new ApiError(404, $"There's no task ");
            }
            return res;
        }

        public async Task<Domain.Entities.Todo> Add(Domain.Entities.Todo todo)
        {
            return await _todoRepository.Add(todo);

        }

        public async Task<Domain.Entities.Todo> Edit(Domain.Entities.Todo todo)
        {
            return await _todoRepository.Edit(todo);
        }

    }
}
