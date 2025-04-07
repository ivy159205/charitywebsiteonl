using Charity_Website_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharityWEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        DBCNhom1 dbc;
        public UserController(DBCNhom1 db)
        {
            dbc = db;
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(dbc.TblUsers.ToList());
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(string u_user_id, string u_name,string u_email, string u_password, string u_role,string u_phone,string u_address)
        {
            if (string.IsNullOrEmpty(u_user_id) || string.IsNullOrEmpty(u_name) || string.IsNullOrEmpty(u_password) || string.IsNullOrEmpty(u_role)
                || string.IsNullOrEmpty(u_phone) || string.IsNullOrEmpty(u_address))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var user = new TblUser
            {
                UUserId = u_user_id,
                UName = u_name,
                UEmail = u_email,
                UPassword = u_password,
                URole = u_role,
                UPhone = u_phone,
                UAddress = u_address,
                UCreatedAt = DateTime.Now
            };

            dbc.TblUsers.Add(user);
            dbc.SaveChanges();
            return Ok(user);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(string u_user_id, string u_name, string u_email, string u_password, string u_role, string u_phone, string u_address)
        {
            if (string.IsNullOrEmpty(u_user_id))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var existingUser = dbc.TblUsers.Find(u_user_id);
            if (existingUser == null)
            {
                return NotFound(new { message = "Không tìm thấy người dùng" });
            }

            existingUser.UName = u_name;
            existingUser.UEmail = u_email;
            existingUser.UPassword = u_password;
            existingUser.URole = u_role;
            existingUser.UPhone = u_phone;
            existingUser.UAddress = u_address;

            dbc.TblUsers.Update(existingUser);
            dbc.SaveChanges();
            return Ok(existingUser);
        }


        [HttpPost]
        [Route("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(string u_user_id)
        {
            if (string.IsNullOrEmpty(u_user_id))
            {
                return BadRequest(new { message = "ID không hợp lệ" });
            }
            var xoaUser = dbc.TblUsers.Find(u_user_id);

            if (xoaUser == null)
            {
                return NotFound(new { message = "Không tìm thấy người dùng" });
            }

            dbc.TblUsers.Remove(xoaUser);
            dbc.SaveChanges();
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
