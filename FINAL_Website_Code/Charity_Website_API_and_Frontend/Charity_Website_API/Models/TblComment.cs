using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_comments")]
public partial class TblComment
{
    [Key]
    [Column("cmt_comment_id")]
    [StringLength(255)]
    public string CmtCommentId { get; set; } = null!;

    [Column("cmt_user_id")]
    [StringLength(255)]
    public string? CmtUserId { get; set; }

    [Column("cmt_campaign_id")]
    [StringLength(255)]
    public string? CmtCampaignId { get; set; }

    [Column("cmt_content")]
    [StringLength(255)]
    public string CmtContent { get; set; } = null!;

    [Column("cmt_created_at", TypeName = "datetime")]
    public DateTime? CmtCreatedAt { get; set; }

    [ForeignKey("CmtCampaignId")]
    [InverseProperty("TblComments")]
    public virtual TblCampaign? CmtCampaign { get; set; }

    [ForeignKey("CmtUserId")]
    [InverseProperty("TblComments")]
    public virtual TblUser? CmtUser { get; set; }
}
