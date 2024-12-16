using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JournyTask.Models;

public partial class FCarePlus3Context : DbContext
{
    public FCarePlus3Context()
    {
    }

    public FCarePlus3Context(DbContextOptions<FCarePlus3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountsChart> AccountsCharts { get; set; }

    public virtual DbSet<JournalDetail> JournalDetails { get; set; }

    public virtual DbSet<JournalHeader> JournalHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=fCarePlus3;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountsChart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3214EC2748817F42");

            entity.ToTable("AccountsChart");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AllowEntry).HasColumnName("Allow_Entry");
            entity.Property(e => e.BranchId).HasColumnName("Branch_ID");
            entity.Property(e => e.ChartLevelDepth).HasColumnName("Chart_Level_Depth");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.FkCostCenterTypeId).HasColumnName("FK_Cost_Center_Type_ID");
            entity.Property(e => e.FkTransactionTypeId).HasColumnName("FK_Transaction_Type_ID");
            entity.Property(e => e.FkWorkFieldsId).HasColumnName("FK_Work_Fields_ID");
            entity.Property(e => e.IsActive).HasColumnName("Is_Active");
            entity.Property(e => e.NameAr)
                .HasMaxLength(150)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("NameEN");
            entity.Property(e => e.NoOfChilds).HasColumnName("noOfChilds");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrgId).HasColumnName("Org_ID");
            entity.Property(e => e.ParentId).HasColumnName("Parent_ID");
            entity.Property(e => e.ParentNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Parent_Number");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<JournalDetail>(entity =>
        {
            entity.ToTable("JournalDetail");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.JournalDetails)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JournalDetail_AccountsChart");

            entity.HasOne(d => d.JournalHeader).WithMany(p => p.JournalDetails)
                .HasForeignKey(d => d.JournalHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JournalDetail_JournalHeader");
        });

        modelBuilder.Entity<JournalHeader>(entity =>
        {
            entity.ToTable("JournalHeader");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
