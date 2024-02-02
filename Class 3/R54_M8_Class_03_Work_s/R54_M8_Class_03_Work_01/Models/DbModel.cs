using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace R54_M8_Class_03_Work_01.Models
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Teacher : Employee
    {
        public string Subject { get; set; }
        //public DateTime JoinDate { get; set; }
    }
    public class RegularEmployee:Employee
    {
        public string Post { get; set; }
    }
    public class TestDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<RegularEmployee> RegularEmployees { get; set;}
    }
}