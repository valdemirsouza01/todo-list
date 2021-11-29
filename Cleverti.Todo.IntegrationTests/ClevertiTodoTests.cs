using AutoMapper;
using Cleverti.Todo.Application;
using Cleverti.Todo.Application.ViewModel;
using Cleverti.Todo.DependencyInjection;
using Cleverti.Todo.Domain.Interfaces.Repositories;
using GmeCore.ImportRemitFiles.IntegrationTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Cleverti.Todo.IntegrationTests
{

    [TestClass]
    public class ClevertiTodoTests
    {
        private readonly ITodoProcess _todoProcess;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
       

        public ClevertiTodoTests()
        {
            _configuration = Configuration.GetConfiguration();
            ServiceCollection services = new ServiceCollection();
            DependencyResolver.RegisterServices(services, _configuration);

            services.AddSingleton(_configuration);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            _todoProcess = serviceProvider.GetService<ITodoProcess>();
            _mapper = serviceProvider.GetService<IMapper>();

            
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TodoMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            var res = await _todoProcess.GetAll();

            Assert.IsNotNull(res);
        }


        [TestMethod]
        public async Task GetTodoById_Success()
        {
            Guid id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var res = await _todoProcess.GetTodoById(id);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public async Task Add_Success()
        {
            var todo = new TodoViewModel()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Description = "test test 01",
                IsDone = false
            };

            var domainTodo = _mapper.Map<Domain.Entities.Todo>(todo);
            var res = await Task.Run(() => _todoProcess.Add(domainTodo).Result); 

            Assert.IsNotNull(res);
        }
        [TestMethod]
        public async Task Edit_Success()
        {
            var todo = new TodoViewModel()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Description = "Test update",
                IsDone = true
            };
            var domainTodo = _mapper.Map<Domain.Entities.Todo>(todo);
            var res = await Task.Run(() => _todoProcess.Edit(domainTodo).Result);
            Assert.IsNotNull(res);
        }


        [TestMethod]
        public async Task Remove_Success()
        {
            Guid id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var res = await _todoProcess.Remove(id);
            Assert.IsNotNull(res);
        }
    }
}

