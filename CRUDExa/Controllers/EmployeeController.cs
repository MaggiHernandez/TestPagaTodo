using AutoMapper;
using CRUDExa.Models.Models;
using CRUDExa.Persistencia.Repository;
using CRUDExa.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDExa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public EmployeeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        /// <summary>
        /// Acción HTTP POST para agregar un nuevo empleado.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("AddEmployee")]
        public ActionResult<ModelResponse> AddEmployee(Employee employee)
        {
            return _contenedorTrabajo.Employee.AddEmployee(employee);
        }

        /// <summary>
        /// Acción HTTP GET para obtener los detalles de un empleado por su ID.
        /// </summary>
        /// <param name="employeId"></param>
        /// <returns></returns>
        [HttpGet("GetEmploye")]
        public ActionResult<ModelResponse> GetEmployee(int employeId)
        {
            return  _contenedorTrabajo.Employee.GetEmployee(employeId);
        }

        /// <summary>
        /// Acción HTTP DELETE para eliminar un empleado por su ID.
        /// </summary>
        /// <param name="employeId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteEmployee")]
        public ActionResult<ModelResponse> DeleteEmployee(int employeId)
        {
            return _contenedorTrabajo.Employee.DeleteEmployee(employeId);
        }

        /// <summary>
        /// Acción HTTP PATCH para actualizar los datos de un empleado.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPatch("UpdateEmployee")]
        public ActionResult<ModelResponse> UpdateEmployee(Employee employee)
        {
            return _contenedorTrabajo.Employee.UpdateEmployee(employee);
        }
    }
}
