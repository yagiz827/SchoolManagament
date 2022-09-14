using Castle.Core.Internal;
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


    public class EFStudentDal : EfEntityRepository<Student, Database>, IStudentDal
    {
        public void MakeFull(Student student, Class cass)
        {

            int count = 0;
            Dictionary<string, int> weekDays = new Dictionary<string, int>();

            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            weekDays.Add("Monday", 0);
            weekDays.Add("Tuesday", 1);
            weekDays.Add("Wednesday", 2);
            weekDays.Add("Thursday", 3);
            weekDays.Add("Friday", 4);

            Dictionary<string, int> Periods = new Dictionary<string, int>();

            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            Periods.Add("7:9", 0);
            Periods.Add("9:11", 1);
            Periods.Add("11:13", 2);
            Periods.Add("13:15", 3);
            Periods.Add("15:17", 4);
            Periods.Add("17:19", 5);
            Periods.Add("19:21", 6);
            var g = cass.Hour.Split("-");


            foreach (var k in cass.Weekday.Split("-"))
            {
                Suchedule stude = new Suchedule();
                stude.StudentId = student.StudentId;
                using (var db = new Database())
                {
                    stude.ArrayIndexX = weekDays[k];
                    stude.ArrayIndexY = Periods[g[count]];
                    stude.Value = true;
                    var y = db.Classes.Where(x => x.ClassName == cass.ClassName).SingleOrDefault().ClassId;
                    stude.ClaId = y;
                    count++;
                    db.suchedules.Add(stude);
                    db.SaveChanges();

                }

            }




        }
        public bool checkIfFree(Student student, Class cass)
        {
            int count = 0;
            int y = 0;


            Dictionary<string, int> weekDays = new Dictionary<string, int>();

            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            weekDays.Add("Monday", 0);
            weekDays.Add("Tuesday", 1);
            weekDays.Add("Wednesday", 2);
            weekDays.Add("Thursday", 3);
            weekDays.Add("Friday", 4);

            Dictionary<string, int> Periods = new Dictionary<string, int>();

            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            Periods.Add("7:9", 0);
            Periods.Add("9:11", 1);
            Periods.Add("11:13", 2);
            Periods.Add("13:15", 3);
            Periods.Add("15:17", 4);
            Periods.Add("17:19", 5);
            Periods.Add("19:21", 6);

            var g = cass.Hour.Split("-");
            foreach (var k in cass.Weekday.Split("-"))
            {
                Suchedule stude = new Suchedule();
                stude.StudentId = student.StudentId;
                stude.ArrayIndexX = weekDays[k];
                stude.ArrayIndexY = Periods[g[count]];
                count++;
                using (var db = new Database())
                {
                    var m = db.suchedules.Where(x => x.StudentId == stude.StudentId).Where(m => m.ArrayIndexX == stude.ArrayIndexX).Where(m => m.ArrayIndexY == stude.ArrayIndexY).Select(x => x.Value).ToList();

                    if (m.Count() > 0)
                    {
                        y++;
                    }
                }






            }
            if (y != 0) { return false; }

            return true;

        }
        public StudentClass AddClasses(string grade, Student stude, StudentClass ClassTo, Class cass)
        {
            using (var db = new Database())
            {
                if (checkIfFree(stude, cass))
                {
                    MakeFull(stude, cass);
                    ClassTo.StudentId = stude.StudentId;

                    ClassTo.Grade = grade;


                    db.studentClasses.Add(ClassTo);


                    db.SaveChanges();

                }
            }




            Console.WriteLine("full");

            return ClassTo;
        }

        public List<string> GetClassDetail(Student stu)
        {
            List<StudentClass> studensts = new List<StudentClass>();
            List<string> ba = new List<string>();
            using (var db = new Database())
            {
                var a = db.studentClasses.Where(x => x.StudentId == stu.StudentId).Select(x => x.ClassId).ToList();
                foreach (var item in a)
                {
                    var t = db.Classes.FirstOrDefault(_ => _.ClassId == item);

                    ba.Add(t.ClassName);

                }
                return ba;
            }
        }

        public double Gpa(Student stu)
        {
            double sum = 0;
            Dictionary<string, int> Gpaa = new Dictionary<string, int>();

            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            Gpaa.Add("A", 4);
            Gpaa.Add("B", 3);
            Gpaa.Add("C", 2);
            Gpaa.Add("D", 1);

            using (var db = new Database())
            {

                var a = db.studentClasses.Where(x => x.StudentId == stu.StudentId).Select(x => x.Grade).ToList();
                foreach (var item in a)
                {

                    var t = item.ToUpper();
                    sum += Gpaa[t];
                }
                sum = sum / a.Count();

                stu.Grade = sum;
                if (stu.Grade >= 2)
                {

                    stu.CanGrad = true;


                }
                else
                {
                    stu.CanGrad = false;

                }
                db.Update(stu);
                db.SaveChanges();
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
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string Pass(Student stu)
        {

            if (stu.CanGrad == true)
            {
                return "yeah passed " + stu.Grade.ToString();
            }
            return "noo";
        }



        public string[,] GetSuchedules(List<Suchedule> stu)
        {
            string m;
            string[,] suchedule = new string[6, 8];
            suchedule[1, 0] = "Monday"; suchedule[2, 0] = "Tuesday"; suchedule[3, 0] = "Wednesday"; suchedule[4, 0] = "Thursday"; suchedule[5, 0] = "Friday";
            suchedule[0, 1] = "7:9"; suchedule[0, 2] = "9:11"; suchedule[0, 3] = "11:13"; suchedule[0, 4] = "13:15"; suchedule[0, 5] = "15:17"; suchedule[0, 6] = "17:19"; suchedule[0, 7] = "19:21";
            foreach (var y in stu)
            {
                for (int i = 1; i < 6; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {

                        if (y.ArrayIndexX == i - 1 && y.ArrayIndexY == j - 1)
                        {
                            using (var db = new Database())
                            {
                                m = db.Classes.Where(x => x.ClassId == y.ClaId).SingleOrDefault().ClassName;
                            }
                            suchedule[i, j] = m;
                            break;
                        }
                        else if (suchedule[i, j] != "-" && suchedule[i, j] != null) { }
                        else
                        {
                            suchedule[i, j] = "-";

                        }

                    }

                }

            }
            //for (int i = 0; i < 6; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    { Console.WriteLine(suchedule[i,j]); }
            //    Console.WriteLine("////");
            //}
            return suchedule;

        }
    }
}