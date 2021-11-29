using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cleverti.Todo.Data
{
    public interface ITodoContext
    {

        List<Domain.Entities.Todo> Todo { get; set; }

        Task SaveChangesAsync();

    }
}
