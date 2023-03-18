using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppl.DbRepo;

public partial class WebApiContext : DbContext
{
    public WebApiContext()
    {
    }

    public WebApiContext(DbContextOptions<WebApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        // => optionsBuilder.UseMySql("server=localhost;user id=root;password=Enppr3033;database=webapidb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentName).HasMaxLength(500);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.DepartmentId, "DepartmentId");

            entity.Property(e => e.EmployeeName).HasMaxLength(500);
            entity.Property(e => e.Photo).HasColumnType("blob");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employee_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
