using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Models;

public partial class DBCNhom1 : DbContext
{
    public DBCNhom1()
    {
    }

    public DBCNhom1(DbContextOptions<DBCNhom1> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdminLog> TblAdminLogs { get; set; }

    public virtual DbSet<TblCampaign> TblCampaigns { get; set; }

    public virtual DbSet<TblCampaignMedium> TblCampaignMedia { get; set; }

    public virtual DbSet<TblComment> TblComments { get; set; }

    public virtual DbSet<TblDonation> TblDonations { get; set; }

    public virtual DbSet<TblReport> TblReports { get; set; }

    public virtual DbSet<TblTask> TblTasks { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblVolunteer> TblVolunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=cmcsv.ric.vn,10000;Initial Catalog=N10_NHOM1;Persist Security Info=True;User ID=cmcsv;Password=cM!@#2025;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAdminLog>(entity =>
        {
            entity.HasKey(e => e.ALogId).HasName("PK__tbl_admi__FF914AC42FCCC76C");

            entity.HasOne(d => d.AAdmin).WithMany(p => p.TblAdminLogs).HasConstraintName("FK__tbl_admin__a_adm__6EF57B66");
        });

        modelBuilder.Entity<TblCampaign>(entity =>
        {
            entity.HasKey(e => e.CCampaignId).HasName("PK__tbl_camp__3A9FAB6287877878");

            entity.HasOne(d => d.COrganization).WithMany(p => p.TblCampaigns).HasConstraintName("FK__tbl_campa__c_org__5DCAEF64");
        });

        modelBuilder.Entity<TblCampaignMedium>(entity =>
        {
            entity.HasKey(e => e.MMediaId).HasName("PK__tbl_camp__DBB56E4C8A2BD0CD");

            entity.HasOne(d => d.MCampaign).WithMany(p => p.TblCampaignMedia).HasConstraintName("FK__tbl_campa__m_cam__75A278F5");
        });

        modelBuilder.Entity<TblComment>(entity =>
        {
            entity.HasKey(e => e.CmtCommentId).HasName("PK__tbl_comm__5EEB6D2C29A3EDAD");

            entity.HasOne(d => d.CmtCampaign).WithMany(p => p.TblComments).HasConstraintName("FK__tbl_comme__cmt_c__72C60C4A");

            entity.HasOne(d => d.CmtUser).WithMany(p => p.TblComments).HasConstraintName("FK__tbl_comme__cmt_u__71D1E811");
        });

        modelBuilder.Entity<TblDonation>(entity =>
        {
            entity.HasKey(e => e.DDonationId).HasName("PK__tbl_dona__701C2DC20FF11BCC");

            entity.HasOne(d => d.DCampaign).WithMany(p => p.TblDonations).HasConstraintName("FK__tbl_donat__d_cam__797309D9");

            entity.HasOne(d => d.DUser).WithMany(p => p.TblDonations).HasConstraintName("FK__tbl_donat__d_use__787EE5A0");
        });

        modelBuilder.Entity<TblReport>(entity =>
        {
            entity.HasKey(e => e.RReportId).HasName("PK__tbl_repo__72A998F5E6DEAFDD");

            entity.HasOne(d => d.RCampaign).WithMany(p => p.TblReports).HasConstraintName("FK__tbl_repor__r_cam__6C190EBB");
        });

        modelBuilder.Entity<TblTask>(entity =>
        {
            entity.HasKey(e => e.TTaskId).HasName("PK__tbl_task__469F1A392387CFEC");

            entity.HasOne(d => d.TAssignedToNavigation).WithMany(p => p.TblTasks).HasConstraintName("FK__tbl_tasks__t_ass__693CA210");

            entity.HasOne(d => d.TCampaign).WithMany(p => p.TblTasks).HasConstraintName("FK__tbl_tasks__t_cam__68487DD7");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UUserId).HasName("PK__tbl_user__986AC5AD51319FAE");
        });

        modelBuilder.Entity<TblVolunteer>(entity =>
        {
            entity.HasKey(e => e.VVolunteerId).HasName("PK__tbl_volu__B342DCB883C01BDA");

            entity.HasOne(d => d.VCampaign).WithMany(p => p.TblVolunteers).HasConstraintName("FK__tbl_volun__v_cam__656C112C");

            entity.HasOne(d => d.VUser).WithMany(p => p.TblVolunteers).HasConstraintName("FK__tbl_volun__v_use__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
