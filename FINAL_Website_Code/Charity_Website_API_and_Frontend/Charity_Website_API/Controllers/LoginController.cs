using Microsoft.AspNetCore.Mvc;
using Charity_Website_API.Models;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DBCNhom1 _context;

        public LoginController(DBCNhom1 context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Email và Password không được để trống" });
            }

            var user = _context.TblUsers.FirstOrDefault(u => u.UEmail == email && u.UPassword == password);
            if (user == null)
            {
                return Unauthorized(new { message = "Thông tin đăng nhập không hợp lệ" });
            }

            return Ok(new
            {
                message = "Đăng nhập thành công",
                userId = user.UUserId,
                username = user.UName,
                email = user.UEmail,
                role = user.URole // Trả về role để frontend xử lý điều hướng
            });
        }
    }
}
