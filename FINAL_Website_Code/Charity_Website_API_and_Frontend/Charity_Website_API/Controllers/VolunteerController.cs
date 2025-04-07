using Charity_Website_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        DBCNhom1 db1;
        public VolunteerController(DBCNhom1 db)
        {
            db1 = db;
        }

        [HttpGet]
        [Route("Getlist")]
        public IActionResult Getlist()
        {
            return Ok(db1.TblVolunteers.ToList());
        }

        string TaoVolunteerID()
        {
            string id = "";
            var cmd = db1.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_volunteer_get_id";
            cmd.Connection.Open();
            id = cmd.ExecuteScalar().ToString();

            //var kq = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(kq);
            //id = dt.Rows[0][0].ToString();
            return id;
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(string volunteerId, string userId, string campaignId, string status)
        {
            // tao log_id khi ko goi them truc  => ""
            if (volunteerId == "null") volunteerId = TaoVolunteerID();

            var volunteer = new TblVolunteer
            {
                VVolunteerId = volunteerId,
                VUserId = userId,
                VCampaignId = campaignId,
                VStatus = status,
                VAppliedAt = DateTime.Now
            };

            db1.TblVolunteers.Add(volunteer);
            db1.SaveChanges();
            return Ok(volunteer);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(string volunteerId, string userId, string campaignId, string status)
        {
            if (string.IsNullOrEmpty(volunteerId))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var existingVolunteer = db1.TblVolunteers.Find(volunteerId);
            if (existingVolunteer == null)
            {
                return NotFound(new { message = "Không tìm thấy tình nguyện viên" });
            }

            existingVolunteer.VUserId = userId;
            existingVolunteer.VCampaignId = campaignId;
            existingVolunteer.VStatus = status;

            db1.TblVolunteers.Update(existingVolunteer);
            db1.SaveChanges();
            return Ok(existingVolunteer);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string volunteerId)
        {
            if (string.IsNullOrEmpty(volunteerId))
            {
                return BadRequest(new { message = "ID không hợp lệ" });
            }

            var xoaVolunteer = db1.TblVolunteers.Find(volunteerId);
            if (xoaVolunteer == null)
            {
                return NotFound(new { message = "Không tìm thấy tình nguyện viên" });
            }

            db1.TblVolunteers.Remove(xoaVolunteer);
            db1.SaveChanges();
            return Ok(new { message = "Xóa thành công" });
        }

        [HttpGet]
        [Route("TopDonors")]
        public IActionResult GetTopDonors(int top = 3)
        {
            var topDonors = db1.TblDonations
                .GroupBy(d => d.DUserId)
                .Select(group => new
                {
                    UserId = group.Key,
                    TotalDonatedAmount = group.Sum(d => d.DAmount),
                    UserInfo = db1.TblUsers
                        .Where(u => u.UUserId == group.Key)
                        .Select(u => new { u.UName, u.UEmail })
                        .FirstOrDefault()
                })
                .OrderByDescending(d => d.TotalDonatedAmount)
                .Take(top)
                .ToList();

            return Ok(topDonors);
        }
    }
}