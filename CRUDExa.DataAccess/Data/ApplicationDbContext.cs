//using System;
//using System.Collections.Generic;
//using CRUDExa.Models.Models;
//using Microsoft.EntityFrameworkCore;

//namespace CRUDExa.Models;

//public partial class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext()
//    {
//    }

//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Employee> Employees { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Employee>(entity =>
//        {
//            entity.ToTable("Employee");

//            entity.Property(e => e.FirstName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Salary).HasColumnType("money");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
