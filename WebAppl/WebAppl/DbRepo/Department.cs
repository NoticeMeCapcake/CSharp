using System;
using System.Collections.Generic;

namespace WebAppl.DbRepo;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
