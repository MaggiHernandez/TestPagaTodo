using CRUDExa.DataAccess.Data;
using CRUDExa.DataAccess.Data.Employee;
using CRUDExa.DataAccess.Repository.Employee;
using CRUDExa.Persistencia.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExa.Persistencia.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;

        public ContenedorTrabajo(ApplicationDbContext context)
        {
            _context = context;
            Employee = new EmployeeRepository(_context, this);
        }

        /// <summary>
        /// Implementación del método Dispose para liberar los recursos del contexto.
        /// </summary>
        public IEmployeeRepository Employee { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Método para guardar los cambios en el contexto de la base de datos.
        /// </summary>
        public void save()
        {
            _context.SaveChanges();
        }
    }
}
