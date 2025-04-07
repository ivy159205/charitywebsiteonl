using Charity_Website_API.Controllers;
using Charity_Website_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityProgram3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly DBCNhom1 dbc;
        public CampaignController(DBCNhom1 context)
        {
            dbc = context; // Inject DbContext thông qua Dependency Injection
        }
        [HttpGet]
        [Route("GetList")]
        public IActionResult GetUser()
        {
            return Ok(dbc.TblCampaigns.ToList());
        }
        [HttpGet]
        [Route("GetbyID")]
        public async Task<IActionResult> GetCampaignById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Campaign ID is required.");
            }

            var campaign = await dbc.TblCampaigns
                .Include(c => c.TblCampaignMedia) // Lấy danh sách ảnh
                .FirstOrDefaultAsync(c => c.CCampaignId == id);

            if (campaign == null)
            {
                return NotFound($"Campaign with ID '{id}' not found.");
            }

            var result = new
            {
                campaign.CCampaignId,
                campaign.CTitle,
                campaign.CDescription,
                campaign.CGoalAmount,
                campaign.CCollectedAmount,
                campaign.CStartDate,
                campaign.CEndDate,
                campaign.CStatus,
                campaign.CCreatedAt,
                campaign.CCategory,
                Media = campaign.TblCampaignMedia.Select(m => new
                {
                    m.MMediaId,
                    m.MMediaUrl
                })
            };

            return Ok(result);
        }
        [HttpGet("SearchCampaigns")]
        public IActionResult SearchCampaigns(string? campaignId, string? title, string? status, string? organizationId)
        {
            var campaigns = dbc.TblCampaigns.AsQueryable();

            if (!string.IsNullOrWhiteSpace(campaignId))
                campaigns = campaigns.Where(c => c.CCampaignId == campaignId);

            if (!string.IsNullOrWhiteSpace(title))
                campaigns = campaigns.Where(c => c.CTitle.Contains(title));

            if (!string.IsNullOrWhiteSpace(status))
                campaigns = campaigns.Where(c => c.CStatus == status);

            if (!string.IsNullOrWhiteSpace(organizationId))
                campaigns = campaigns.Where(c => c.COrganizationId == organizationId);

            var result = campaigns.ToList();

            if (result.Count == 0)
                return NotFound("No campaigns found.");

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult AddCampaign(string cam_id, string org_id, string cam_title, string cam_description, decimal goal_ammount, DateOnly start_date, DateOnly end_date, string cam_status, DateTime cam_creation_date, string category)
        {
            Charity_Website_API.Models.TblCampaign c1 = new Charity_Website_API.Models.TblCampaign();
            c1.CCampaignId = cam_id;
            c1.COrganizationId = org_id;
            c1.CTitle = cam_title;
            c1.CDescription = cam_description;
            c1.CGoalAmount = goal_ammount;
            c1.CStartDate = start_date;
            c1.CEndDate = end_date;
            c1.CStatus = cam_status;
            c1.CCreatedAt = cam_creation_date;
            c1.CCategory = category;
            dbc.TblCampaigns.Add(c1);

            // goi them log o day
            AdminLog log = new AdminLog(dbc);
            log.AddAdminLog("", "", $"Thêm mới chiến dịch {cam_id}");

            dbc.SaveChanges();
            return Ok(dbc.TblCampaigns.ToList());
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateCampaign(string cam_id, string org_id, string cam_title, string cam_description, decimal goal_ammount, DateOnly start_date, DateOnly end_date, string cam_status, DateTime cam_creation_date, string category)
        {
            if (string.IsNullOrWhiteSpace(cam_id))
            {
                return BadRequest("Campaign ID is required.");
            }
            var c1 = dbc.TblCampaigns.FirstOrDefault(c => c.CCampaignId == cam_id);

            if (c1 == null)
            {
                return NotFound("Campaign not found.");
            }
            c1.CCampaignId = cam_id;
            c1.COrganizationId = org_id;
            c1.CTitle = cam_title;
            c1.CDescription = cam_description;
            c1.CGoalAmount = goal_ammount;
            c1.CStartDate = start_date;
            c1.CEndDate = end_date;
            c1.CStatus = cam_status;
            c1.CCreatedAt = cam_creation_date;
            c1.CCategory = category;
            dbc.TblCampaigns.Update(c1);

            // goi them log o day
            AdminLog log = new AdminLog(dbc);
            log.AddAdminLog("", "", $"Sửa đổi chiến dịch {cam_id}");

            dbc.SaveChanges();
            return Ok(dbc.TblCampaigns.ToList());
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteCampaign(string cam_id)
        {
            var c1 = dbc.TblCampaigns.FirstOrDefault(c => c.CCampaignId == cam_id);
            if (c1 == null)
            {
                return NotFound("Campaign not found.");
            }
            dbc.TblCampaigns.Remove(c1);

            // goi them log o day
            AdminLog log = new AdminLog(dbc);
            log.AddAdminLog("", "", $"Loại bỏ chiến dịch {cam_id}");

            dbc.SaveChanges();
            return Ok(dbc.TblCampaigns.ToList());
        }
    }
}
