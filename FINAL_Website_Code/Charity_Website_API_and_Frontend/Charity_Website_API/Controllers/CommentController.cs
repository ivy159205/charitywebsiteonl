using Charity_Website_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharityWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        DBCNhom1 dbc;
        public CommentController(DBCNhom1 db)
        {
            dbc = db;
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(dbc.TblComments.ToList());
        }

        [HttpPost]
        [Route("/Comment/Insert")]
        public IActionResult Thembinhluan(string cmt_comment_id, string cmt_user_id, string cmt_campaign_id, string cmt_content, DateTime cmt_created_at)
        {
            TblComment cmt = new TblComment();
            cmt.CmtCommentId = cmt_comment_id;
            cmt.CmtUserId = cmt_user_id;
            cmt.CmtCampaignId = cmt_campaign_id;
            cmt.CmtContent = cmt_content;
            cmt.CmtCreatedAt = cmt_created_at;
            dbc.TblComments.Add(cmt);
            dbc.SaveChanges();
            return Ok(new { cmt });
        }

        [HttpPost]
        [Route("/Comment/Update")]
        public IActionResult Capnhatbinhluan(string cmt_comment_id, string cmt_user_id, string cmt_campaign_id, string cmt_content, DateTime cmt_created_at)
        {
            TblComment cmt = new TblComment();
            cmt.CmtCommentId = cmt_comment_id;
            cmt.CmtUserId = cmt_user_id;
            cmt.CmtCampaignId = cmt_campaign_id;
            cmt.CmtContent = cmt_content;
            cmt.CmtCreatedAt = cmt_created_at;
            dbc.TblComments.Update(cmt);
            dbc.SaveChanges();
            return Ok(new { cmt });
        }

        [HttpPost]
        [Route("/Comment/Delete")]
        public IActionResult Xoabinhluan(string cmt_comment_id)
        {
            TblComment cmt = new TblComment();
            cmt.CmtCommentId = cmt_comment_id;
            dbc.TblComments.Remove(cmt);
            dbc.SaveChanges();
            return Ok(new { cmt });
        }

    }
}
