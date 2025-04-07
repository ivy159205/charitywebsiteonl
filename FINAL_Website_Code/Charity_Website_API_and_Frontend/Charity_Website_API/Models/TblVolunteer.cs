using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_volunteers")]
public partial class TblVolunteer
{
    [Key]
    [Column("v_volunteer_id")]
    [StringLength(255)]
    public string VVolunteerId { get; set; } = null!;

    [Column("v_user_id")]
    [StringLength(255)]
    public string? VUserId { get; set; }

    [Column("v_campaign_id")]
    [StringLength(255)]
    public string? VCampaignId { get; set; }

    [Column("v_status")]
    [StringLength(255)]
    public string? VStatus { get; set; }

    [Column("v_applied_at", TypeName = "datetime")]
    public DateTime? VAppliedAt { get; set; }

    [ForeignKey("VCampaignId")]
    [InverseProperty("TblVolunteers")]
    public virtual TblCampaign? VCampaign { get; set; }

    [ForeignKey("VUserId")]
    [InverseProperty("TblVolunteers")]
    public virtual TblUser? VUser { get; set; }
}
