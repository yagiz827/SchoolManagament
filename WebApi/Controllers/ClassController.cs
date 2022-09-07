using Bussiness.Abstract;
using Bussiness.Concrete;
using DataAccessLayer.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ClassController : ControllerBase
    {

        IClassService _ClassService;
        IUserService _UserService;

        public ClassController(IClassService ClassService, IUserService userService)
        {
            _ClassService = ClassService;
            _UserService = userService;
        }


        [HttpGet("Get All"),Authorize(Roles = "Teacher,Student")]

        //public List<Class> Get()
        //{
        //    IClassService _ClassService = new ClassManage(new EFClassDal());

        //    var R = _ClassService.GetA();
        //    return R.Data;
        //}

        //public List<Class> Get()
        //{
        //    return _ClassService.GetA().Data;
        //}
        public IActionResult Get()
        {
            var R = _ClassService.GetA();
            if (R.Succes)
            {

                return Ok(R.Data);
            }
            else
            {
                return BadRequest(R);
            }
        }
        [HttpGet("Get Via Name")]
        public IActionResult Get(string Name)
        {

            var R = _ClassService.GetByName(Name);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);

        }

        
        [HttpPost("AddClass"),Authorize(Roles ="Teacher")]
        public IActionResult Post(classDto DtoClass)
        {
            using(var db=new Database())
            {
                var m = db.Teachers.FirstOrDefault(y => y.TeacherName==DtoClass.DtoTeachernName);

                Class _Class = new Class
                {
                    ClassName = DtoClass.DtoClassName,
                    TeacherId = m.TeacherId,
                };
                var R = _ClassService.Add(_Class);

                if (R.Succes)
                {
                    return Ok(R);
                }
                return BadRequest(R);
            }
            
        }
        [HttpPost("Delete Class"), Authorize(Roles = "Teacher")]
        public IActionResult Del(Class pro)
        {

            List<Class> Classs = _ClassService.GetA().Data;
            var R = _ClassService.Delete(Classs, pro);
            if (R.Succes)
            {
                return Ok(R);
            }
            return BadRequest(R);
        }



    }
}



