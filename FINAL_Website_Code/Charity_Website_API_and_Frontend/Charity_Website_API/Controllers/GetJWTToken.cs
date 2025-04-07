using Charity_Website_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Charity_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetJWTToken : ControllerBase
    {
        DBCNhom1 dbc;
        IConfiguration cfg;

        public GetJWTToken(DBCNhom1 _dbc, IConfiguration _config)
        {
            dbc = _dbc;
            cfg = _config;
        }

        [HttpGet]
        [Route("Login")]

        public IActionResult Login(string uid, string pwd)
        {
            // check đăng nhập, nếu thành công thì tạo token, nếu không thành công thì thôi
            var user = dbc.TblUsers.FirstOrDefault(u => u.UEmail == uid && u.UPassword == pwd);

            if (user != null)
            {
                // Login sucessful
                return GetKey(uid, user.URole, user.UUserId);
            }

            return Ok(new
            {
                code = 101,
                msg = "Tên đăng nhập hoặc mật khẩu không đúng",
                token = "",
                quyen = ""
            });
        }

        [HttpGet]
        [Route("GetKey")]

        public IActionResult GetKey(string uid, string quyen, string userId)
        {
            // check đăng nhập, nếu thành công thì tạo token, nếu không thành công thì thôi


            // end login?

            var jwtHandle = new JwtSecurityTokenHandler();
            var key = cfg["AppSettings:SecretKey"];
            var keybytes = Encoding.UTF8.GetBytes(key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, uid),
                new Claim("tokenid", Guid.NewGuid().ToString()),
                new Claim("group_permission", quyen)
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtHandle.CreateToken(tokenDescription);
            return Ok(new
            {
                code = 100,
                msg = "Đăng nhập thành công",
                token = jwtHandle.WriteToken(token),
                quyen = quyen,
                userId = userId,  // Trả về userId
            });
        }

        [HttpGet]
        [Route("GetKeyAdmin")]

        public IActionResult GetKeyAdmin(string uid)
        {
            var jwtHandle = new JwtSecurityTokenHandler();
            var key = cfg["AppSettings:SecretKey"];
            var keybytes = Encoding.UTF8.GetBytes(key);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, uid),
                    new Claim("tokenid", Guid.NewGuid().ToString()),
                    new Claim("group_permission","Admin"),
            };
            //new Claim(JwtRegisteredClaimNames.Sub, "user123"),
            //new Claim(JwtRegisteredClaimNames.Email, "user@example.com"),
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            var tokenDescrition = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtHandle.CreateToken(tokenDescrition);
            return Ok(new
            {
                code = 100,
                msg = "Đăng nhập thành công",
                token = jwtHandle.WriteToken(token)
            });
        }

        [HttpGet]
        [Route("GetKeyCustomer")]

        public IActionResult GetKeyCustomer(string uid, string pwd)
        {
            var jwtHandle = new JwtSecurityTokenHandler();
            var key = cfg["AppSettings:SecretKey"];
            var keybytes = Encoding.UTF8.GetBytes(key);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, uid),
                    new Claim("tokenid", Guid.NewGuid().ToString()),
                    new Claim("group_permission","Customer"),
            };
            //new Claim(JwtRegisteredClaimNames.Sub, "user123"),
            //new Claim(JwtRegisteredClaimNames.Email, "user@example.com"),
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            var tokenDescrition = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtHandle.CreateToken(tokenDescrition);
            return Ok(new
            {
                code = 100,
                msg = "Đăng nhập thành công",
                token = jwtHandle.WriteToken(token)
            });
        }

        [HttpGet]
        [Route("GetKeyVolunteer")]

        public IActionResult GetKeyVolunteer(string uid, string pwd)
        {
            var jwtHandle = new JwtSecurityTokenHandler();
            var key = cfg["AppSettings:SecretKey"];
            var keybytes = Encoding.UTF8.GetBytes(key);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, uid),
                    new Claim("tokenid", Guid.NewGuid().ToString()),
                    new Claim("group_permission","Volunteer"),
            };
            //new Claim(JwtRegisteredClaimNames.Sub, "user123"),
            //new Claim(JwtRegisteredClaimNames.Email, "user@example.com"),
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            var tokenDescrition = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtHandle.CreateToken(tokenDescrition);
            return Ok(new
            {
                code = 100,
                msg = "Đăng nhập thành công",
                token = jwtHandle.WriteToken(token)
            });
        }
    }
}
