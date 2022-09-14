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
    public interface ITeacherService
    {

        IDataResult<List<Teacher>> GetA();
        IResults Add(Teacher teach);
        IResults Register(Teacher stu);
        IDataResult<string> Login(Teacher stu);
        IDataResult<List<Class>> GetByTeacherName(string name);

        IResults Delete(List<Teacher> list1, Teacher pro);
    }
}
