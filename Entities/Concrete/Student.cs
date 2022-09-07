using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Student : Ient
    {
        public int StudentId {get ; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string StudentName { get; set; }
        public bool CanGrad{ get; set; }
        public int Grade { get; set; }
        public List<StudentClass> Stu { get; set; }



    }
}
