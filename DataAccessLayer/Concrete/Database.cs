using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Database:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder data)
        {
            data.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yagiz\\Desktop\\DBs\\SchoolApp.mdf;Integrated Security=True;Connect Timeout=30");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>(entity =>
           entity.HasMany(p => p.Classes).
           WithOne(p => p.Teachers).HasForeignKey(p => p.TeacherId));
            
           modelBuilder.Entity<Student>(entity =>
           entity.HasMany(p => p.suchedules).
           WithOne(p => p.Students).HasForeignKey(p => p.StudentId));

            modelBuilder.Entity<StudentClass>()
            .HasKey(x => new { x.StudentId, x.ClassId });


           
           
        }




        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students{ get; set; }
        public DbSet<Teacher> Teachers{ get; set; }
        public DbSet<StudentClass> studentClasses{ get; set; }
        public DbSet<Suchedule> suchedules{ get; set; }



    }
}
