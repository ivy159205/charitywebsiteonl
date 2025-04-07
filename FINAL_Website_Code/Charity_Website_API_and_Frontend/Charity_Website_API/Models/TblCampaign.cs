using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_campaigns")]
public partial class TblCampaign
{
    [Key]
    [Column("c_campaign_id")]
    [StringLength(255)]
    public string CCampaignId { get; set; } = null!;

    [Column("c_organization_id")]
    [StringLength(255)]
    public string? COrganizationId { get; set; }

    [Column("c_title")]
    [StringLength(255)]
    public string CTitle { get; set; } = null!;

    [Column("c_description")]
    [StringLength(255)]
    public string? CDescription { get; set; }

    [Column("c_goal_amount", TypeName = "decimal(15, 2)")]
    public decimal? CGoalAmount { get; set; }

    [Column("c_collected_amount", TypeName = "decimal(15, 2)")]
    public decimal? CCollectedAmount { get; set; }

    [Column("c_start_date")]
    public DateOnly CStartDate { get; set; }

    [Column("c_end_date")]
    public DateOnly CEndDate { get; set; }

    [Column("c_status")]
    [StringLength(255)]
    public string? CStatus { get; set; }

    [Column("c_created_at", TypeName = "datetime")]
    public DateTime? CCreatedAt { get; set; }

    [Column("c_category")]
    [StringLength(255)]
    public string? CCategory { get; set; }

    [ForeignKey("COrganizationId")]
    [InverseProperty("TblCampaigns")]
    public virtual TblUser? COrganization { get; set; }

    [InverseProperty("MCampaign")]
    public virtual ICollection<TblCampaignMedium> TblCampaignMedia { get; set; } = new List<TblCampaignMedium>();

    [InverseProperty("CmtCampaign")]
    public virtual ICollection<TblComment> TblComments { get; set; } = new List<TblComment>();

    [InverseProperty("DCampaign")]
    public virtual ICollection<TblDonation> TblDonations { get; set; } = new List<TblDonation>();

    [InverseProperty("RCampaign")]
    public virtual ICollection<TblReport> TblReports { get; set; } = new List<TblReport>();

    [InverseProperty("TCampaign")]
    public virtual ICollection<TblTask> TblTasks { get; set; } = new List<TblTask>();

    [InverseProperty("VCampaign")]
    public virtual ICollection<TblVolunteer> TblVolunteers { get; set; } = new List<TblVolunteer>();
}
