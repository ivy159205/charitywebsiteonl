using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_tasks")]
public partial class TblTask
{
    [Key]
    [Column("t_task_id")]
    [StringLength(255)]
    public string TTaskId { get; set; } = null!;

    [Column("t_campaign_id")]
    [StringLength(255)]
    public string? TCampaignId { get; set; }

    [Column("t_description")]
    [StringLength(255)]
    public string TDescription { get; set; } = null!;

    [Column("t_assigned_to")]
    [StringLength(255)]
    public string? TAssignedTo { get; set; }

    [Column("t_status")]
    [StringLength(255)]
    public string? TStatus { get; set; }

    [Column("t_created_at", TypeName = "datetime")]
    public DateTime? TCreatedAt { get; set; }

    [ForeignKey("TAssignedTo")]
    [InverseProperty("TblTasks")]
    public virtual TblUser? TAssignedToNavigation { get; set; }

    [ForeignKey("TCampaignId")]
    [InverseProperty("TblTasks")]
    public virtual TblCampaign? TCampaign { get; set; }
}
