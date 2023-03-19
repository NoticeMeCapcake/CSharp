using System;
using System.Collections.Generic;

namespace WebAppl.DbRepo;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public int Age { get; set; }

    public byte[]? Photo { get; set; }
    
    public string DateOfJoining { get; set; } 

    public virtual Department? Department { get; set; }
}
