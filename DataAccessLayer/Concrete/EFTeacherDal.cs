using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
    public class EFTeacherDal: EfEntityRepository<Teacher, Database>, ITeacherDal
    {
        public List<Class> GetClassDet(string TeacherName)
        {
            using (var db = new Database())
            {

                var id = db.Teachers.Where(x => x.TeacherName == TeacherName).SelectMany(x => x.Classes).ToList();
                return id;

                //var m =  from p in db.Classes
                //         join c in id
                //         on p.TeacherId equals id.TeacherId
                //var R=from p in db.Students
                //      join c in db.Classes
                //      on p.
            }
        }

        public string Login(Teacher teacher)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, teacher.TeacherName),
                new Claim(ClaimTypes.Role,"Teacher")
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

    }
}
