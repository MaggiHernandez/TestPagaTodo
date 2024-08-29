using CRUDExa.DataAccess.Repository.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExa.Persistencia.Repository
{
    public interface IContenedorTrabajo :IDisposable
    {
        public IEmployeeRepository Employee { get; }
        void save();
    }
}
