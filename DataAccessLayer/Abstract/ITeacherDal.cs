using Core.DataAccess;
using Core.Entities;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITeacherDal: IEntityRepository<Teacher>
    {
        List<Class> GetClassDet(string TeacherName);
        string Login(Teacher stu);

    }
}
