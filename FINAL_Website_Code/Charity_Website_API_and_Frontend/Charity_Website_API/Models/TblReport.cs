using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_reports")]
public partial class TblReport
{
    [Key]
    [Column("r_report_id")]
    [StringLength(255)]
    public string RReportId { get; set; } = null!;

    [Column("r_campaign_id")]
    [StringLength(255)]
    public string? RCampaignId { get; set; }

    [Column("r_report_details")]
    [StringLength(255)]
    public string RReportDetails { get; set; } = null!;

    [Column("r_created_at", TypeName = "datetime")]
    public DateTime? RCreatedAt { get; set; }

    [ForeignKey("RCampaignId")]
    [InverseProperty("TblReports")]
    public virtual TblCampaign? RCampaign { get; set; }
}
