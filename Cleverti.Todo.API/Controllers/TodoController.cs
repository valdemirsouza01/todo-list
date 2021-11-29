using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Cleverti.Todo.Application;
using Cleverti.Todo.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cleverti.Todo.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoProcess _process;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoProcess process, IMapper mapper)
        {
            _process = process;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TodoViewModel>> GetTodoById(Guid id)
        {
            var res = await _process.GetTodoById(id);
            if (res == null)
            {
                return NotFound();
            }

            var todoViewModel = _mapper.Map<Domain.Entities.Todo, TodoViewModel>(res);

            return Ok(todoViewModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<TodoViewModel>>> GetAll()
        {

            var res = await _process.GetAll();

            if (null == res)
                return NotFound();

            //IEnumerable great to readonly
            var todoViewModel = _mapper.Map<IEnumerable<Domain.Entities.Todo>, IEnumerable<TodoViewModel>>(res);

            return Ok(todoViewModel);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Edit([FromBody] TodoViewModel todo)
        {

            var domainTodo = _mapper.Map<Domain.Entities.Todo>(todo);

            var res = await Task.Run(() => _process.Edit(domainTodo).Result);

            if (null == res)
                return NotFound();

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TodoViewModel todo)
        {
            var domainTodo = _mapper.Map<Domain.Entities.Todo>(todo);
            var res = await Task.Run(() => _process.Add(domainTodo).Result);
            if (null == res)
                return NotFound();

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(Guid id)
        {
            await _process.Remove(id);
            return Ok();
        }
    }
}









