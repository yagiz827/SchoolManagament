using Bussiness.Abstract;
using Bussiness.Cons;
using Core.Uti.Results;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class ClassManage:IClassService
    {
        IClassDal _iClassdal;

        public ClassManage(IClassDal iClassdal)
        {
            _iClassdal = iClassdal;
        }

        public IResults Add(Class teach)
        {
            _iClassdal.Add(teach);
            return new SuccesResult(Message.AllGot);

        }

        public IResults Delete(List<Class> list1, Class pro)
        {
            bool k = false;
            foreach (Class p in list1)
            {
                if (p.ClassId == pro.ClassId)
                {
                    k = true;
                }
            }

            if (k)
            {

                _iClassdal.Delete(pro);
                return new SuccesResult(Message.AllGot);


            }
            else
            {

                return new ErrorResult(Message.Invalid);
            }

        }

        public IDataResult<List<Class>> GetA()
        {
            bool b = true;
            if (b)
            {
                return new SuccessDataResult<List<Class>>(_iClassdal.GetA(), Message.AllGot);

            }
            else
            {
                return new ErrorDataResult<List<Class>>(_iClassdal.GetA(), Message.No);
            }
        }

        public IDataResult<List<Class>> GetByName(string Name)
        {
            var R = _iClassdal.GetA(p => p.ClassName == Name);
            if (R == null)
            {
                return new ErrorDataResult<List<Class>>(Message.No);

            }
            else
            {
                return new SuccessDataResult<List<Class>>(R, Message.AllGot);


            }
        }
        public IDataResult<List<Class>> GetById(int m)
        {
            var R = _iClassdal.GetA(p => p.ClassId == m);
            if (R == null)
            {
                return new ErrorDataResult<List<Class>>(Message.No);
            }
            else
            {
                return new SuccessDataResult<List<Class>>(R, Message.AllGot);


            }
        }



    }
}
