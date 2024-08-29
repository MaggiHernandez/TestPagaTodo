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

        [HttpPost("AddEmployee")]
        public ActionResult<ModelResponse> AddEmployee(Employee employee)
        {
            return _contenedorTrabajo.Employee.AddEmployee(employee);
        }

        [HttpGet("GetEmploye")]
        public ActionResult<ModelResponse> GetEmployee(int employeId)
        {
            return  _contenedorTrabajo.Employee.GetEmployee(employeId);
        }

        [HttpDelete("DeleteEmployee")]
        public ActionResult<ModelResponse> DeleteEmployee(int employeId)
        {
            return _contenedorTrabajo.Employee.DeleteEmployee(employeId);
        }

        [HttpPatch("UpdateEmployee")]
        public ActionResult<ModelResponse> UpdateEmployee(Employee employee)
        {
            return _contenedorTrabajo.Employee.UpdateEmployee(employee);
        }
    }
}
