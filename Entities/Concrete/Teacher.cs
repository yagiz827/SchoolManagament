using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Teacher:Ient
    {
        public string TeacherName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int TeacherId { get; set; }
       // public List<Class> ClassList { get; set; }

        public ICollection<Class> Classes{ get; set; }
    }
}
