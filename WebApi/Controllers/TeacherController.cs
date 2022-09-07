using Bussiness.Abstract;
using Bussiness.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        ITeacherService _TeacherService;

        public TeachersController(ITeacherService TeacherService)
        {
            _TeacherService = TeacherService;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(teacherRegDto reg)
        {
            Teacher _teach= new Teacher();
            Hashing.Hashing.hash(reg.DtoPassword, out byte[] PasHash, out byte[] PasSalt);

            _teach.TeacherName = reg.Teachername;
            _teach.PasswordSalt = PasSalt;
            _teach.PasswordHash = PasHash;
            var r = _TeacherService.Add(_teach);

            if (r.Succes)
            {
                return Ok(r);
            }
            return BadRequest(r);




        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(teacherRegDto reg)
        {
            List<Teacher> teachers = _TeacherService.GetA().Data;

            var a = from cust in teachers
                    where cust.TeacherName == reg.Teachername
                    select cust;
            if (a.Count() > 0)
            {

                foreach (var teacher in a)
                {
                    if (Hashing.Hashing.VerifyPass(reg.DtoPassword, teacher.PasswordHash, teacher.PasswordSalt))
                    {
                        var m = _TeacherService.Login(teacher);
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
       
        [HttpGet("Get All")]

        //public List<Teacher> Get()
        //{
        //    ITeacherService _TeacherService = new TeacherMan(new EfTeacherDal());

        //    var R = _TeacherService.GetA();
        //    return R.Data;
        //}
        //sam as
        //public List<Teacher> Get()
        //{
        //    return _TeacherService.GetA().Data;
        //}
        public IActionResult Get()
        {
            var R = _TeacherService.GetA();
            if (R.Succes)
            {
                return Ok(R.Data);
            }
            else
            {
                return BadRequest(R);
            }
        }
        [HttpGet]
        [Route("GetByTeacherName/{name}")]
        public IActionResult GetByTeacherName(string name)
        {

            var R = _TeacherService.GetByTeacherName(name);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);

        }

        [HttpGet("Get Via Name")]
        public IActionResult Get(int Name)
        {

            var R = _TeacherService.GetById(Name);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);

        }
        
        [HttpPost("Add Teacher")]
        public IActionResult Post(teacherDto DtoTeacher)
        {
            Teacher _Teacher = new Teacher
            {
                TeacherName = DtoTeacher.DtoTeacherName,
            };
            var R = _TeacherService.Add(_Teacher);

            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);
        }
        [HttpPost("Delete Teacher")]
        public IActionResult Del(Teacher pro)
        {

            List<Teacher> Teachers = _TeacherService.GetA().Data;
            var R = _TeacherService.Delete(Teachers, pro);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);
        }



    }
}



