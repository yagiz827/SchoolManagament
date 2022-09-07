using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class classDto:IDto
    {

        public string DtoClassName { get; set; }

        public string DtoTeachernName { get; set; }

    }
}
