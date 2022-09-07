using Bussiness.Abstract;
using Bussiness.Cons;
using Core.Uti.Results;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class TeacherManage : ITeacherService
    {

        ITeacherDal _iTeacherdal;

        public TeacherManage(ITeacherDal iTeacherdal)
        {
            _iTeacherdal = iTeacherdal;
        }

        public IResults Add(Teacher teach)
        {
            _iTeacherdal.Add(teach);
            return new SuccesResult(Message.AllGot);

        }

        public IResults Delete(List<Teacher> list1, Teacher pro)
        {
            bool k = false;
            foreach (Teacher p in list1)
            {
                if (p.TeacherId == pro.TeacherId)
                {
                    k = true;
                }
            }

            if (k)
            {

                _iTeacherdal.Delete(pro);
                return new SuccesResult(Message.AllGot);


            }
            else
            {

                return new ErrorResult(Message.Invalid);
            }

        }

        public IDataResult<List<Teacher>> GetA()
        {
            bool b = true;
            if (b)
            {
                return new SuccessDataResult<List<Teacher>>(_iTeacherdal.GetA(), Message.AllGot);

            }
            else
            {
                return new ErrorDataResult<List<Teacher>>(_iTeacherdal.GetA(), Message.No);
            }
        }

        public IDataResult<List<Teacher>> GetStuDetail(string Name)
        {
            var R = _iTeacherdal.GetA(p => p.TeacherName == Name);
            if (R == null)
            {
                return new ErrorDataResult<List<Teacher>>(Message.No);

            }
            else
            {
                return new SuccessDataResult<List<Teacher>>(R, Message.AllGot);


            }
        }
        public IDataResult<List<Teacher>> GetById(int m)
        {
            var R = _iTeacherdal.GetA(p => p.TeacherId == m);
            if (R == null)
            {
                return new ErrorDataResult<List<Teacher>>(Message.No);
            }
            else
            {
                return new SuccessDataResult<List<Teacher>>(R, Message.AllGot);


            }
        }

        public IDataResult<List<Teacher>> GetTeacherDetail()
        {
            throw new NotImplementedException();
        }


        IDataResult<Teacher> ITeacherService.GetById(int id)
        {
            throw new NotImplementedException();
        }
        public IDataResult<List<Class>> GetByTeacherName(string name)
        {
            return new SuccessDataResult<List<Class>>(_iTeacherdal.GetClassDet(name), Message.AllGot);

        }

        public IResults Register(Teacher stu)
        {
            _iTeacherdal.Add(stu);
            return new SuccesResult(Message.AllGot);
        }

        public IDataResult<string> Login(Teacher stu)
        {
            var m = _iTeacherdal.Login(stu);
            Console.WriteLine(m);
            return new SuccessDataResult<string>(m, Message.AllGot);
        }
    }
}
