using Core.Uti.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IClassService
    {
        IDataResult<List<Class>> GetA();
        IResults Add(Class teach);
        IDataResult<List<Class>> GetByName(string id);
        IResults Delete(List<Class> list1, Class pro);
    }
}
