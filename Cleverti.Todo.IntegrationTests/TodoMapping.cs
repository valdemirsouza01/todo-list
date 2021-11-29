using AutoMapper;
using Cleverti.Todo.Application.ViewModel;

namespace Cleverti.Todo.IntegrationTests
{
    public class TodoMapping : Profile
    {
        public TodoMapping()
        {
            CreateMap<TodoViewModel, Domain.Entities.Todo>().ReverseMap();
        }
    }
}
