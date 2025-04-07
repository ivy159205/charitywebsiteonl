using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_campaign_media")]
public partial class TblCampaignMedium
{
    [Key]
    [Column("m_media_id")]
    [StringLength(255)]
    public string MMediaId { get; set; } = null!;

    [Column("m_campaign_id")]
    [StringLength(255)]
    public string? MCampaignId { get; set; }

    [Column("m_media_type")]
    [StringLength(255)]
    public string? MMediaType { get; set; }

    [Column("m_media_url")]
    [StringLength(255)]
    public string MMediaUrl { get; set; } = null!;

    [Column("m_created_at", TypeName = "datetime")]
    public DateTime? MCreatedAt { get; set; }

    [ForeignKey("MCampaignId")]
    [InverseProperty("TblCampaignMedia")]
    public virtual TblCampaign? MCampaign { get; set; }
}
