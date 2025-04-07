using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_users")]
[Index("UEmail", Name = "UQ__tbl_user__3DF9EF2202F1B1A8", IsUnique = true)]
public partial class TblUser
{
    [Key]
    [Column("u_user_id")]
    [StringLength(255)]
    public string UUserId { get; set; } = null!;

    [Column("u_name")]
    [StringLength(255)]
    public string UName { get; set; } = null!;

    [Column("u_email")]
    [StringLength(255)]
    public string UEmail { get; set; } = null!;

    [Column("u_password")]
    [StringLength(255)]
    public string UPassword { get; set; } = null!;

    [Column("u_role")]
    [StringLength(255)]
    public string URole { get; set; } = null!;

    [Column("u_created_at", TypeName = "datetime")]
    public DateTime? UCreatedAt { get; set; }

    [Column("u_phone")]
    [StringLength(255)]
    public string? UPhone { get; set; }

    [Column("u_address")]
    [StringLength(255)]
    public string? UAddress { get; set; }

    [InverseProperty("AAdmin")]
    public virtual ICollection<TblAdminLog> TblAdminLogs { get; set; } = new List<TblAdminLog>();

    [InverseProperty("COrganization")]
    public virtual ICollection<TblCampaign> TblCampaigns { get; set; } = new List<TblCampaign>();

    [InverseProperty("CmtUser")]
    public virtual ICollection<TblComment> TblComments { get; set; } = new List<TblComment>();

    [InverseProperty("DUser")]
    public virtual ICollection<TblDonation> TblDonations { get; set; } = new List<TblDonation>();

    [InverseProperty("TAssignedToNavigation")]
    public virtual ICollection<TblTask> TblTasks { get; set; } = new List<TblTask>();

    [InverseProperty("VUser")]
    public virtual ICollection<TblVolunteer> TblVolunteers { get; set; } = new List<TblVolunteer>();
}
