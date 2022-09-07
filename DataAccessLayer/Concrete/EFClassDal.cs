using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
     public class EFClassDal: EfEntityRepository<Class,Database>,IClassDal
    {

    }
}
