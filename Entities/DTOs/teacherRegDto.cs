using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class teacherRegDto:IDto
    {
        public string Teachername { get; set; }
        public string DtoPassword { get; set; }

    }
}
