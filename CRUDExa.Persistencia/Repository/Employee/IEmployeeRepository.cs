using CRUDExa.Persistencia.Repository;
using CRUDExa.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExa.DataAccess.Repository.Employee
{
    public interface IEmployeeRepository : IRepository<Models.Models.Employee>
    {
        ModelResponse AddEmployee(Models.Models.Employee employee);
        ModelResponse GetEmployee(int employeId);
        ModelResponse DeleteEmployee(int employeId);
        ModelResponse UpdateEmployee(Models.Models.Employee employee);
    }
}
