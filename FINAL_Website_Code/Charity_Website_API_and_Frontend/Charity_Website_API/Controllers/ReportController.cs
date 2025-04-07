using Charity_Website_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharityWEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        DBCNhom1 dbc;
        public ReportController(DBCNhom1 db)
        {
            dbc = db;
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(dbc.TblReports.ToList());
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(string r_report_id, string r_campaign_id, string r_report_details)
        {
            if (string.IsNullOrEmpty(r_report_id) || string.IsNullOrEmpty(r_campaign_id) || string.IsNullOrEmpty(r_report_details))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var report = new TblReport
            {
                RReportId = r_report_id,
                RCampaignId = r_campaign_id,
                RReportDetails = r_report_details,
                RCreatedAt = DateTime.Now
            };

            dbc.TblReports.Add(report);
            dbc.SaveChanges();
            return Ok(report);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(string r_report_id, string r_campaign_id, string r_report_details)
        {
            if (string.IsNullOrEmpty(r_report_id))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var existingReport = dbc.TblReports.Find(r_report_id);
            if (existingReport == null)
            {
                return NotFound(new { message = "Không tìm thấy báo cáo" });
            }

            existingReport.RCampaignId = r_campaign_id;
            existingReport.RReportDetails = r_report_details;

            dbc.TblReports.Update(existingReport);
            dbc.SaveChanges();
            return Ok(existingReport);
        }


        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string r_report_id)
        {
            if (string.IsNullOrEmpty(r_report_id))
            {
                return BadRequest(new { message = "ID không hợp lệ" });
            }
            var xoaReport = dbc.TblReports.Find(r_report_id);

            if (xoaReport == null)
            {
                return NotFound(new { message = "Không tìm thấy báo cáo" });
            }

            dbc.TblReports.Remove(xoaReport);
            dbc.SaveChanges();
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
