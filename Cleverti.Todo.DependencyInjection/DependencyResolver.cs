using Cleverti.Todo.Application;
using Cleverti.Todo.Data;
using Cleverti.Todo.Data.Repositories;
using Cleverti.Todo.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cleverti.Todo.DependencyInjection
{
    public class DependencyResolver
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //Database
            services.AddSingleton<ITodoContext, TodoContext>();

            //Application
            services.AddScoped<ITodoProcess, TodoProcess>();

            //Repositories
            services.AddScoped<ITodoRepository, TodoRepository>();
        }

    }
}
