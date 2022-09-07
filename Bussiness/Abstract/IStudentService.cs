﻿using Core.Uti.Hashing;
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
        IDataResult<List<string>> GetClassDetail(Student teach);
        IDataResult <List<StudentClass>> AddClasses(string grade,string s ,List<Student> stu,StudentClass classes);

        IResults Add(Student teach);
        IResults Gpa(Student teach);
        IDataResult<List<Student>> GetByName(string id);
        IResults Delete(List<Student> list1, Student pro);

    }
}
