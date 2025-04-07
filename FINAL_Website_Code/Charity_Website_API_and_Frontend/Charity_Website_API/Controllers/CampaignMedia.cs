using Charity_Website_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Charity_Website_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignMedia : ControllerBase
    {
        private readonly DBCNhom1 dbc;
        private readonly IWebHostEnvironment _environment;

        public CampaignMedia(DBCNhom1 dbc, IWebHostEnvironment environment)
        {
            this.dbc = dbc;
            _environment = environment;
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(dbc.TblCampaignMedia.ToList());
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult AddCampaignMed(string m_media_id, string m_campaign_id, string m_media_type, string m_media_url)
        {
            TblCampaignMedium camMed = new TblCampaignMedium();
            camMed.MMediaId = m_media_id;
            camMed.MCampaignId = m_campaign_id;
            camMed.MMediaType = m_media_type;
            camMed.MMediaUrl = m_media_url;
            camMed.MCreatedAt = DateTime.Now;
            dbc.TblCampaignMedia.Add(camMed);
            dbc.SaveChanges();
            return Ok("Insert Campaign Media ID: " + m_media_id + " successfully!");
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateCampaignMed(string m_media_id, string m_campaign_id, string m_media_type, string m_media_url)
        {
            var existingCampaignMed = dbc.TblCampaignMedia.Find(m_media_id);
            if (existingCampaignMed == null)
            {
                return NotFound($"Campaign Media with ID {m_media_id} not found");
            }

            existingCampaignMed.MCampaignId = m_campaign_id;
            existingCampaignMed.MMediaType = m_media_type;
            existingCampaignMed.MMediaUrl = m_media_url;
            existingCampaignMed.MCreatedAt = DateTime.Now;
            dbc.TblCampaignMedia.Update(existingCampaignMed);
            dbc.SaveChanges();
            return Ok("Update CampaignMedia ID " + m_media_id + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteCampaignMed(string m_media_id)
        {
            var campaignMed = dbc.TblCampaignMedia.Find(m_media_id);
            if (campaignMed == null)
            {
                return NotFound($"Campaign Media with ID {m_media_id} not found");
            }

            try
            {
                // Delete the file if it exists (only if the URL is a local file path)
                if (!string.IsNullOrEmpty(campaignMed.MMediaUrl) && campaignMed.MMediaUrl.StartsWith("/uploads/"))
                {
                    string fileName = Path.GetFileName(campaignMed.MMediaUrl.Replace("/uploads/", ""));
                    string filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                dbc.TblCampaignMedia.Remove(campaignMed);
                dbc.SaveChanges();
                return Ok("Delete CampaignMedia ID " + m_media_id + " successfully!");
            }
            catch (Exception ex)
            {
                // Just delete the database record if file deletion fails
                dbc.TblCampaignMedia.Remove(campaignMed);
                dbc.SaveChanges();
                return Ok($"Deleted database record for ID {m_media_id}, but could not delete file: {ex.Message}");
            }
        }

        // Exclude this endpoint from Swagger documentation
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("UploadMedia")]
        public IActionResult UploadMedia([FromForm] IFormFile file, [FromForm] string mediaId, [FromForm] string campaignId, [FromForm] string mediaType)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            try
            {
                // Định nghĩa thư mục mới
                string projectRoot = Path.Combine(_environment.ContentRootPath, "..", "charifit-master-updated_by_huy", "charifit-master", "img");

                // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
                if (!Directory.Exists(projectRoot))
                {
                    Directory.CreateDirectory(projectRoot);
                }

                // Tạo tên file duy nhất
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(projectRoot, uniqueFileName);

                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Đường dẫn lưu vào DB (có thể thay đổi nếu cần)
                string mediaUrl = $"/img/{uniqueFileName}";

                var existingMedia = dbc.TblCampaignMedia.Find(mediaId);
                if (existingMedia != null)
                {
                    // Xóa file cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(existingMedia.MMediaUrl) && existingMedia.MMediaUrl.StartsWith("/img/"))
                    {
                        string oldFileName = Path.GetFileName(existingMedia.MMediaUrl.Replace("/img/", ""));
                        string oldFilePath = Path.Combine(projectRoot, oldFileName);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Cập nhật thông tin file mới
                    existingMedia.MCampaignId = campaignId;
                    existingMedia.MMediaType = mediaType;
                    existingMedia.MMediaUrl = mediaUrl;
                    existingMedia.MCreatedAt = DateTime.Now;
                    dbc.TblCampaignMedia.Update(existingMedia);
                }
                else
                {
                    // Tạo mới bản ghi
                    TblCampaignMedium camMed = new TblCampaignMedium
                    {
                        MMediaId = mediaId,
                        MCampaignId = campaignId,
                        MMediaType = mediaType,
                        MMediaUrl = mediaUrl,
                        MCreatedAt = DateTime.Now
                    };
                    dbc.TblCampaignMedia.Add(camMed);
                }

                dbc.SaveChanges();

                return Ok(new { message = "File uploaded successfully", mediaUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-campaign/{campaignId}")]
        public async Task<IActionResult> GetMediaByCampaign(string campaignId)
        {
            var mediaList = await dbc.TblCampaignMedia
                .Where(m => m.MCampaignId == campaignId && m.MMediaType == "Ảnh")
                .Select(m => new
                {
                    m.MMediaId,
                    m.MMediaUrl
                }).ToListAsync();
            if (mediaList == null || !mediaList.Any())
            {
                return NotFound(new { message = "Không tìm thấy ảnh cho chiến dịch này." });
            }

            return Ok(mediaList);
        }

    }
}