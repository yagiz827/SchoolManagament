using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Class:Ient
    {
        public string ClassName { get; set; }
        public int ClassId { get; set; }
        public Teacher Teachers { get; set; }

        public int TeacherId { get; set; }
        public List<StudentClass> Cla { get; set; }

    }
}
