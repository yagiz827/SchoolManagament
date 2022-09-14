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
        StudentClass AddClasses(string grade, Student d,StudentClass classes, Class cass);
        List<string> GetClassDetail(Student stu);
        string[,] GetSuchedules(List<Suchedule> stu);

        double Gpa(Student stu);

        string Login(Student stu); 
        string Pass(Student stu); 

    }
}
