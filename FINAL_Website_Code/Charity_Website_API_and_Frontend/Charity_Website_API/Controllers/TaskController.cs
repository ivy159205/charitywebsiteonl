using Microsoft.AspNetCore.Mvc;
using Charity_Website_API.Models;
using System;
using System.Linq;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly DBCNhom1 _db;

        public TaskController(DBCNhom1 db)
        {
            _db = db;
        }

        // Lấy danh sách tất cả tasks
        [HttpGet]
        [Route("Getlist")]
        public IActionResult Getlist()
        {
            return Ok(_db.TblTasks.ToList());
        }

        // Thêm task mới
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(string taskId, string campaignId, string description, string assignedTo, string status)
        {
            if (string.IsNullOrEmpty(taskId) || string.IsNullOrEmpty(description))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var task = new TblTask
            {
                TTaskId = taskId,
                TCampaignId = campaignId,
                TDescription = description,
                TAssignedTo = assignedTo,
                TStatus = status,
                TCreatedAt = DateTime.Now
            };

            _db.TblTasks.Add(task);
            _db.SaveChanges();
            return Ok(task);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(string taskId, string campaignId, string description, string assignedTo, string status)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var existingTask = _db.TblTasks.Find(taskId);
            if (existingTask == null)
            {
                return NotFound(new { message = "Không tìm thấy task" });
            }

            // Cập nhật dữ liệu
            existingTask.TCampaignId = campaignId;
            existingTask.TDescription = description;
            existingTask.TAssignedTo = assignedTo;
            existingTask.TStatus = status;

            // Lưu thay đổi (không cần gọi Update)
            _db.SaveChanges();

            return Ok(existingTask);
        }

        // Xóa task
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                return BadRequest(new { message = "ID không hợp lệ" });
            }

            var taskToDelete = _db.TblTasks.Find(taskId);
            if (taskToDelete == null)
            {
                return NotFound(new { message = "Không tìm thấy task" });
            }

            _db.TblTasks.Remove(taskToDelete);
            _db.SaveChanges();
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
