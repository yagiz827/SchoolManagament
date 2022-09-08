using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStudentDal: IEntityRepository<Student>
    {
        List<StudentClass> AddClasses(string grade, string d,List<Student> stu,StudentClass classes);
        List<string> GetClassDetail(Student stu);

        double Gpa(Student stu);

        string Login(Student stu); 
        string Pass(Student stu); 

    }
}
