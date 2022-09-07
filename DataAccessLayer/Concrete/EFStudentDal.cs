using Core.DataAccess.EntityFramework;
using Core.Uti.Results;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{

    public class EFStudentDal: EfEntityRepository<Student, Database>, IStudentDal
    {
       public List<StudentClass> AddClasses(string grade,string Name,List<Student> stu,StudentClass ClassTo)
        {
            StudentClass k=new StudentClass();
            List<StudentClass> studensts=new List<StudentClass>();

           
            foreach (Student student in stu)
            {
                if (student.StudentName == Name)
                {


                    k.StudentId = student.StudentId;
                    using (var db = new Database())
                    {
                        foreach(var item in db.Classes)
                        {
                            if (item.ClassId== ClassTo.ClassId)
                            {
                                k.ClassId = item.ClassId;

                                k.Grade = grade;

                                db.studentClasses.Add(k);
                                break;


                            }

                        }
                        db.SaveChanges();
                        break;
                    }

                }

            }
            return studensts;            
        }

        public List<string> GetClassDetail(Student stu)
        {
            List<StudentClass> studensts = new List<StudentClass>();
            List<string> ba=new List<string>();
            using (var db = new Database())
            {
                var a = db.studentClasses.Where(x => x.StudentId == stu.StudentId).Select(x => x.ClassId).ToList();
                foreach (var item in a)
                {
                    var t = db.Classes.FirstOrDefault(_=> _.ClassId == item);
                 
                    ba.Add(t.ClassName);

                }
                return ba;
            }
        }

        public double Gpa(Student stu)
        {
            double sum=0;
            Dictionary<string, int> Gpaa =new Dictionary<string, int>();

            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            Gpaa.Add("A", 4);
            Gpaa.Add("B", 3);
            Gpaa.Add("C", 2);
            Gpaa.Add("D", 1);

            using (var db = new Database())
            {
                var a=db.studentClasses.Where(x=>x.StudentId== stu.StudentId).Select(x=>x.Grade).ToList();
                foreach(var item in a)
                {

                    var t=item.ToUpper();
                    sum+=Gpaa[t];
                }
                sum =sum/a.Count();
            }
            
            return sum;
        }

        public string Login(Student stu)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, stu.StudentName),
                new Claim(ClaimTypes.Role,"Student"),
                new Claim(ClaimTypes.Spn,stu.StudentId.ToString()),
                
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Spider2000spider2000SPIDER2000"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(16),
                signingCredentials: cred
                ) ;
            
            var jwt= new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }
    }
}
