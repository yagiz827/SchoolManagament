using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class StudentClass : Ient

    {
        public string Grade { get; set; }
        public int StudentId {get ; set; }
        public Student student { get; set; }
        
        public int ClassId { get; set; }
        public Class Class { get; set; }    



    }
}
