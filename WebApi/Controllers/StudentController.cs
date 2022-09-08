using Bussiness.Abstract;
using Bussiness.Concrete;
using Castle.Core.Resource;
using DataAccessLayer.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using WebAPI.Hashing;

namespace WebAPI.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase

    {
        IStudentService _StudentService;
        IUserService _UserService;

        public StudentsController(IStudentService StudentService, IUserService userService)
        {
            _StudentService = StudentService;
            _UserService= userService;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(studentRegDto reg)
        {
            Student _student = new Student();
            Hashing.Hashing.hash(reg.DtoPassword, out byte[] PasHash, out byte[] PasSalt);

            _student.StudentName = reg.DtoStudentName;
            _student.PasswordSalt = PasSalt;
            _student.PasswordHash = PasHash;
            var r = _StudentService.Add(_student);

            if (r.Succes)
            {
                return Ok(r);
            }
            return BadRequest(r);




        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(studentRegDto reg)
        {
            List<Student> Students = _StudentService.GetA().Data;

            var a = from cust in Students
                    where cust.StudentName == reg.DtoStudentName
                    select cust;
            if (a.Count() > 0)
            {
                foreach (var student in a)
                {
                    if (Hashing.Hashing.VerifyPass(reg.DtoPassword, student.PasswordHash, student.PasswordSalt))
                    {
                        var m = _StudentService.Login(student);
                        return Ok(m);
                    }
                }
                return BadRequest();



            }
            else
            {
                return BadRequest();

            }





        }
        
        [Authorize]


        [HttpGet]

        //public List<Student> Get()
        //{
        //    IStudentService _StudentService = new StudentMan(new EfStudentDal());

        //    var R = _StudentService.GetA();
        //    return R.Data;
        //}
        //sam as
        //public List<Student> Get()
        //{
        //    return _StudentService.GetA().Data;
        //}
        [Route("get all"), Authorize(Roles = "Teacher")]

        public IActionResult Get()
        {
            var R = _StudentService.GetA();
            if (R.Succes)
            {
                return Ok(R.Data);
            }
            else
            {
                return BadRequest(R);
            }
        }


        
        [HttpGet("{name}"),Authorize]
        public IActionResult Get(string name)
        {

            var R = _StudentService.GetByName(name);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);

        }
        [HttpPost]
        [Route("GetClasses"), Authorize(Roles = "Teacher,Student")]
        public IActionResult GetDetail(studentDetDto studentDetDto)
        {

            List<Student> Students = _StudentService.GetA().Data;

            var a = from stu in Students
                    where stu.StudentName == studentDetDto.DtoStudentName
                    select stu;
            foreach (var student in a)
            {
                Console.WriteLine(_UserService.GetRole());
                if (_UserService.GetRole() == "Teacher")
                {
                    var R = _StudentService.GetClassDetail(student);
                    if (R.Succes)
                    {
                        return Ok(R);

                    }
                    return BadRequest(R);

                }
                else
                {
                    if ((student.StudentId).ToString() == _UserService.GetUserId())
                    {
                        var R = _StudentService.GetClassDetail(student);
                        if (R.Succes)
                        {
                            return Ok(R);

                        }
                        return BadRequest(R);

                    }
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("CheckifPass"), Authorize(Roles = "Teacher,Student")]
        public IActionResult Pass(string name)
        {

            List<Student> Students = _StudentService.GetA().Data;

            var a = from stu in Students
                    where stu.StudentName == name
                    select stu;
            foreach (var student in a)
            {
                Console.WriteLine(_UserService.GetRole());
                if (_UserService.GetRole() == "Teacher")
                {
                    var R = _StudentService.Pass(student);
                    if (R.Succes)
                    {
                        return Ok(R);

                    }
                    return BadRequest(R);

                }
                else
                {
                    if ((student.StudentId).ToString() == _UserService.GetUserId())
                    {
                        var R = _StudentService.Pass(student);
                        if (R.Succes)
                        {
                            return Ok(R);

                        }
                        return BadRequest(R);

                    }
                }
            }
            return BadRequest();
        }
        
        
        [HttpPost]
        [Route("LearnGPA"), Authorize(Roles = "Teacher,Student")]
        public IActionResult Gpa(studentDetDto DtoStudent)
        {
            List<Student> Students = _StudentService.GetA().Data;

            var a = from cust in Students
                    where cust.StudentName == DtoStudent.DtoStudentName
                    select cust;
            foreach (var student in a)
            {
                var R = _StudentService.Gpa(student);
                if (R.Succes)
                {
                    return Ok(R);
                    break;
                }
                return BadRequest(R);
                break;
            }
            return BadRequest();

        }

        
        [HttpPost]
        [Route("AddCourses")]

        public IActionResult AddClass( string grade,String Name,StudentClassDto stc)
        {
            List<Student> Students = _StudentService.GetA().Data;
            StudentClass d = new StudentClass();


            using (var db = new Database())
            {
                foreach (var cass in db.Classes)
                {
                    if (cass.ClassName == stc.ClassName)
                    {
                        d.ClassId = cass.ClassId;
                
                    }
                }  
            }
            var R = _StudentService.AddClasses(grade,Name,Students, d);

            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);
        }
        
        [HttpDelete]
        public IActionResult Delete(Student pro)
        {

            List<Student> Students = _StudentService.GetA().Data;
            var R = _StudentService.Delete(Students, pro);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);
        }



    }
}



