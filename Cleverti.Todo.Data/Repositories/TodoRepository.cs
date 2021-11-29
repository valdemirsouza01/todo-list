
using Cleverti.Todo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverti.Todo.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        protected readonly ITodoContext _dbContext;

        public TodoRepository(ITodoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var item = _dbContext.Todo.Where(p => p.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.Todo.Remove(item);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<Domain.Entities.Todo> GetById(Guid id)
        {
            try
            {
                return  await Task.Run(() => _dbContext.Todo.Where(p => p.Id == id).FirstOrDefault());  
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Domain.Entities.Todo>> GetAll()
        {
            return await Task.Run(() => _dbContext.Todo);
        }

        public async Task<Domain.Entities.Todo> Add(Domain.Entities.Todo todo)
        {
            try
            {
                _dbContext.Todo.Add(new Domain.Entities.Todo().Create(todo));
                await _dbContext.SaveChangesAsync();
                return todo;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<Domain.Entities.Todo> Edit(Domain.Entities.Todo todo)
        {
            try
            {
                var prevTodo = _dbContext.Todo.Where(p => p.Id == todo.Id).FirstOrDefault();
                if (prevTodo != null)
                {
                    prevTodo.Description = todo.Description;
                    prevTodo.IsDone = todo.IsDone;        
                    await _dbContext.SaveChangesAsync();
                    return todo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return todo;
            }
        }



    }
}
