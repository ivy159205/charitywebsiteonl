using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

[Table("tbl_donations")]
public partial class TblDonation
{
    [Key]
    [Column("d_donation_id")]
    [StringLength(255)]
    public string DDonationId { get; set; } = null!;

    [Column("d_user_id")]
    [StringLength(255)]
    public string? DUserId { get; set; }

    [Column("d_campaign_id")]
    [StringLength(255)]
    public string? DCampaignId { get; set; }

    [Column("d_name")]
    [StringLength(255)]
    public string? DName { get; set; }

    [Column("d_amount", TypeName = "decimal(10, 2)")]
    public decimal DAmount { get; set; }

    [Column("d_total_value", TypeName = "decimal(15, 2)")]
    public decimal? DTotalValue { get; set; }

    [Column("d_donation_date", TypeName = "datetime")]
    public DateTime? DDonationDate { get; set; }

    [ForeignKey("DCampaignId")]
    [InverseProperty("TblDonations")]
    public virtual TblCampaign? DCampaign { get; set; }

    [ForeignKey("DUserId")]
    [InverseProperty("TblDonations")]
    public virtual TblUser? DUser { get; set; }
}
