using CRUDExa.DataAccess.Repository.Employee;
using CRUDExa.Models.Models;
using CRUDExa.Persistencia.Data;
using CRUDExa.Persistencia.Repository;
using CRUDExa.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUDExa.DataAccess.Data.Employee
{
    public class EmployeeRepository : Repository<Models.Models.Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public EmployeeRepository(ApplicationDbContext context, IContenedorTrabajo contenedorTrabajo) : base(context)
        {
            _context = context;
            _contenedorTrabajo = contenedorTrabajo;
        }

        public ModelResponse AddEmployee(Models.Models.Employee employee)
        {
            ModelResponse response = new();
            try
            {
                string mensaje = "";
                string ER = @"[A-ZÑa-zñáÁéÉíÍóÓúÚüÜ ]{5,50}";
                Match match = null;
                match = Regex.Match(employee.FirstName.Trim(), ER);
                if (!match.Success)
                {
                    mensaje += "Nombre no valido.";
                }

                match = Regex.Match(employee.LastName.Trim(), ER);
                if (!match.Success)
                {
                    mensaje += "Apellido no válido.";
                }

                if (employee.Salary < 1)
                {
                    mensaje += "Salario no válido.";
                }
                if(mensaje == "")
                {
                    Models.Models.Employee _employee = _contenedorTrabajo.Employee.Get(employee.EmployeeId);
                    if (employee.EmployeeId == null)
                    {
                        _contenedorTrabajo.Employee.Add(employee);
                        _contenedorTrabajo.save();
                        response.Code = 201;
                        response.Comments = "Guardado con éxito.";
                        response.Response = employee;
                    }
                    else
                    {
                        response.Code = 409;
                        response.Comments = "El empleado ya existe.";
                        response.Response = employee;
                    }
                }
                else
                {
                    response.Code = 422;
                    response.Comments = mensaje;
                    response.Response = employee;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public ModelResponse DeleteEmployee(int employeId)
        {
            ModelResponse response = new();
            try
            {
                Models.Models.Employee employee = _contenedorTrabajo.Employee.Get(employeId);
                if (employee != null) 
                {
                    _contenedorTrabajo.Employee.Remove(employeId);
                    _contenedorTrabajo.save();
                    response.Code = 200;
                    response.Comments = "Eliminado con éxito.";
                    response.Response = string.Empty;
                }
                else
                {
                    response.Code = 404;
                    response.Comments = "El empleado no existe.";
                    response.Response = string.Empty;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        public ModelResponse GetEmployee(int employeId)
        {
            ModelResponse response = new();
            try
            {
                Models.Models.Employee employee = _contenedorTrabajo.Employee.Get(employeId);
                if (employee != null)
                {
                    response.Code = 200;
                    response.Comments = "Empleado encontrado con éxito.";
                    response.Response = employee;
                }
                else
                {
                    response.Code = 404;
                    response.Comments = "El empleado no existe.";
                    response.Response = string.Empty;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        public ModelResponse UpdateEmployee(Models.Models.Employee employee)
        {
            ModelResponse response = new();
            try
            {
                string mensaje = "";
                string ER = @"[A-ZÑa-zñáÁéÉíÍóÓúÚüÜ ]{5,50}";
                Match match = null;
                match = Regex.Match(employee.FirstName.Trim(), ER);
                if (!match.Success)
                {
                    mensaje += "Nombre no valido.";
                }

                match = Regex.Match(employee.LastName.Trim(), ER);
                if (!match.Success)
                {
                    mensaje += "Apellido no válido.";
                }

                if (employee.Salary < 1)
                {
                    mensaje += "Salario no válido.";
                }

                if (mensaje == "")
                {
                    Models.Models.Employee _employee = _contenedorTrabajo.Employee.Get(employee.EmployeeId);
                    if (_employee != null)
                    {
                        _employee.FirstName = employee.FirstName;
                        _employee.LastName = employee.LastName;
                        _employee.Salary = employee.Salary;
                        _context.SaveChanges();
                        response.Code = 200;
                        response.Comments = "Empleado actualizado con éxito.";
                        response.Response = employee;
                    }
                    else
                    {
                        response.Code = 404;
                        response.Comments = "El empleado no existe.";
                        response.Response = string.Empty;
                    }
                }
                else
                {
                    response.Code = 409;
                    response.Comments = mensaje;
                    response.Response = employee;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
    }
}
