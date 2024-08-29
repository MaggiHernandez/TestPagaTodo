using System;
using System.Collections.Generic;

namespace CRUDExa.Models.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public decimal? Salary { get; set; }
}
