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
   
    public class RegLogController : ControllerBase
    {


        private readonly IHttpContextAccessor _httpContextAccessor;
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegLogDto reg)
        {
            if (reg.Method == "Student")

            {
                //IStudentService _StudentService;
                StudentsController h = new StudentsController( new StudentManage(new EFStudentDal()), new UserService(_httpContextAccessor));
                studentRegDto m= new studentRegDto();
                m.DtoStudentName = reg.Name;
                m.DtoPassword = reg.Password;
                return h.Register(m);
                
            }
            else
            {
                TeachersController u = new TeachersController(new TeacherManage(new EFTeacherDal()));
                teacherRegDto y= new teacherRegDto();
                y.Teachername=reg.Name;
                y.DtoPassword= reg.Password;
                return u.Register(y);
            }

            
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(RegLogDto reg)
        {
            if(reg.Method == "Student")
            {
                StudentsController h = new StudentsController(new StudentManage(new EFStudentDal()), new UserService(_httpContextAccessor));
                studentRegDto m = new studentRegDto();
                m.DtoStudentName = reg.Name;
                m.DtoPassword = reg.Password;
                return h.Login(m);
            }
            else
            {
                TeachersController u = new TeachersController(new TeacherManage(new EFTeacherDal()));
                teacherRegDto y = new teacherRegDto();
                y.Teachername = reg.Name;
                y.DtoPassword = reg.Password;
                return u.Login(y);
            }
        }
    }
}



