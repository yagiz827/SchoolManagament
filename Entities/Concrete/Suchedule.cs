using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Suchedule:Ient
    {
        public int SucheduleId { get; set; }
        public int StudentId { get; set; }
        public Student Students { get; set; }
        public int ArrayIndexX { get; set; }
        public int ArrayIndexY { get; set; }
        public bool Value { get; set; }
        public int ClaId { get; set; }


    }
}
