using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Infraestructure.Interface
{
    public interface ILogRepository
    {
        Task Add(string message);
    }
}
