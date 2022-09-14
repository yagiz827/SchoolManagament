using Core.Uti.Hashing;
using Core.Uti.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IStudentService
    {
        IDataResult<List<Student>> GetA();
        IResults Register(Student stu);
        IDataResult<string> Login(Student stu);
        IDataResult<string> Pass(Student stu);
        IDataResult <string[,]> GetSuchedule(List<Suchedule> stu);
        
        IDataResult<List<string>> GetClassDetail(Student teach);
        IDataResult <StudentClass> AddClasses(string grade,Student s ,StudentClass classes,Class cass);
      
        IResults Add(Student teach);
        IResults Gpa(Student teach);
        IDataResult<List<Student>> GetByName(string id);
        IResults Delete(List<Student> list1, Student pro);

    }
}
