using System.Security.Claims;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetRole()
        {
            
            var m=_httpContextAccessor.HttpContext.User.Claims;
            var t = m.ElementAt(1);
            return t.Value.ToString();
           
            
        }

        public string GetUserId()
        {
            string h=" ";
            if (_httpContextAccessor.HttpContext != null)
            {
                h=_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Spn);
                if (h==null)
                {
                    Console.WriteLine("emtpyyyyyy");
                }
                
            }
            return h;
        }

        public string GetUserName()
        {
            string g = " ";
            if(_httpContextAccessor.HttpContext != null)
            {
                g=_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return g;
        }
    }
}
