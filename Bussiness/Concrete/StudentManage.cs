using Bussiness.Abstract;
using Bussiness.Cons;
using Core.Uti.Results;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class StudentManage : IStudentService
    {
        IStudentDal _istudentdal;

        public StudentManage(IStudentDal istudentdal)
        {
            _istudentdal = istudentdal;
        }

        public IResults Add(Student teach)
        {
            _istudentdal.Add(teach);
            return new SuccesResult(Message.AllGot);

        }

        public IResults Delete(List<Student> list1, Student pro)
        {

            bool k = false;
            foreach (Student p in list1)
            {
                if (p.StudentId == pro.StudentId)
                {
                    k = true;
                }
            }

            if (k)
            {

                _istudentdal.Delete(pro);
                return new SuccesResult(Message.AllGot);


            }
            else
            {

                return new ErrorResult(Message.Invalid);
            }

        }

        public IDataResult<List<Student>> GetA()
        {
            bool b = true;
            if (b)
            {
                return new SuccessDataResult<List<Student>>(_istudentdal.GetA(), Message.AllGot);

            }
            else
            {
                return new ErrorDataResult<List<Student>>(_istudentdal.GetA(), Message.No);
            }
        }

        public IDataResult<List<Student>> GetByName(string Name)
        {
            var R = _istudentdal.GetA(p => p.StudentName == Name);
            if (R == null)
            {
                return new ErrorDataResult<List<Student>>(Message.No);

            }
            else
            {
                return new SuccessDataResult<List<Student>>(R, Message.AllGot);


            }
        }
        public IDataResult<List<Student>> GetById(int m)
        {
            var R = _istudentdal.GetA(p => p.StudentId== m);
            if (R == null)
            {
                return new ErrorDataResult<List<Student>>(Message.No);
            }
            else
            {
                return new SuccessDataResult<List<Student>>(R, Message.AllGot);


            }
        }

        public IDataResult<List<string>> GetClassDetail(Student stu)
        {
            var R = _istudentdal.GetClassDetail(stu);
            return new SuccessDataResult<List<string>>(R, Message.AllGot);
        }

        public IDataResult <List<StudentClass>>AddClasses(string grade, String h,List<Student> stu,StudentClass classes)
        {

            var R = _istudentdal.AddClasses(grade,h,stu,classes);


            return new SuccessDataResult<List<StudentClass>>(R, Message.AllGot);
        }

        public IResults Gpa(Student teach)
        {
            var R = _istudentdal.Gpa(teach);
            return new SuccessDataResult<double>(R, Message.AllGot);
        }

        public IResults Register(Student stu)
        {
            _istudentdal.Add(stu);
            return new SuccesResult(Message.AllGot);

        }

        public IDataResult<string> Login(Student stu)
        {
            var m=_istudentdal.Login(stu);
            Console.WriteLine(m);
            return new SuccessDataResult<string>(m, Message.AllGot);
        }

        public IDataResult<string> Pass(Student stu)
        {
            var m = _istudentdal.Pass(stu);
            return new SuccessDataResult<string>(m, Message.AllGot);


        }
    }
}
