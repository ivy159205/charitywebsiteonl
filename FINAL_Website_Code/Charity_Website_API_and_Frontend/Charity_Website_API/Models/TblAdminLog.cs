using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_admin_logs")]
public partial class TblAdminLog
{
    [Key]
    [Column("a_log_id")]
    [StringLength(255)]
    public string ALogId { get; set; } = null!;

    [Column("a_admin_id")]
    [StringLength(255)]
    public string? AAdminId { get; set; }

    [Column("a_action")]
    [StringLength(255)]
    public string AAction { get; set; } = null!;

    [Column("a_action_date", TypeName = "datetime")]
    public DateTime? AActionDate { get; set; }

    [ForeignKey("AAdminId")]
    [InverseProperty("TblAdminLogs")]
    public virtual TblUser? AAdmin { get; set; }
}
