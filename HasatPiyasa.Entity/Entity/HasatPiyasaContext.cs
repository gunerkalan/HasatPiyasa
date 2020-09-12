using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HasatPiyasa.Entity.Entity
{
    public partial class HasatPiyasaContext : DbContext
    {
        public HasatPiyasaContext()
        {
        }

        public HasatPiyasaContext(DbContextOptions<HasatPiyasaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bolges> Bolges { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<DataInputs> DataInputs { get; set; }
        public virtual DbSet<EmteaGroups> EmteaGroups { get; set; }
        public virtual DbSet<EmteaTypeGroups> EmteaTypeGroups { get; set; }
        public virtual DbSet<EmteaTypes> EmteaTypes { get; set; }
        public virtual DbSet<Emteas> Emteas { get; set; }
        public virtual DbSet<FormDataInput> FormDataInput { get; set; }
        public virtual DbSet<SubeCities> SubeCities { get; set; }
        public virtual DbSet<Subes> Subes { get; set; }
        public virtual DbSet<Tuiks> Tuiks { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("data source =10.0.0.127 ;initial catalog=HasatPiyasa;persist security info=true; user id=sa;password=262835Gg.");

              //  optionsBuilder.UseSqlServer("data source =GUNER-PC\\SQLEXPRESS ;initial catalog=HasatPiyasa;persist security info=true; user id=sa;password=262835Gg");

                optionsBuilder.UseSqlServer("data source =. ;initial catalog=HasatPiyasa;persist security info=true; user id=sa;password=123321");
                //optionsBuilder.UseSqlServer("data source =.\\SQLEXPRESS ;initial catalog=HasatPiyasa;persist security info=true; user id=sa;password=123321");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bolges>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);













































































































































































































            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Claims>(entity =>
            {
                entity.Property(e => e.ClaimName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DataInputs>(entity =>
            {
                entity.HasIndex(e => e.AddUserId);

                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.EmteaTypeId);

                entity.HasIndex(e => e.SubeId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.HasOne(d => d.AddUser)
                    .WithMany(p => p.DataInputsAddUser)
                    .HasForeignKey(d => d.AddUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataInputs_Users");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DataInputs)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataInputs_Cities");

                entity.HasOne(d => d.EmteaType)
                    .WithMany(p => p.DataInputs)
                    .HasForeignKey(d => d.EmteaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataInputs_EmteaTypes");

                entity.HasOne(d => d.FormDataInput)
                    .WithMany(p => p.DataInputs)
                    .HasForeignKey(d => d.FormDataInputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataInputs_FormDataInput");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.DataInputs)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataInputs_Subes");

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.DataInputsUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId)
                    .HasConstraintName("FK_DataInputs_Users1");
            });

            modelBuilder.Entity<EmteaGroups>(entity =>
            {
                entity.HasIndex(e => e.EmteaId);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Emtea)
                    .WithMany(p => p.EmteaGroups)
                    .HasForeignKey(d => d.EmteaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmteaGroups_Emteas");
            });

            modelBuilder.Entity<EmteaTypeGroups>(entity =>
            {
                entity.HasIndex(e => e.EmteaTypeId);

                entity.Property(e => e.EmteaTypeGroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EmteaType)
                    .WithMany(p => p.EmteaTypeGroups)
                    .HasForeignKey(d => d.EmteaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmteaTypeGroups_EmteaTypes");
            });

            modelBuilder.Entity<EmteaTypes>(entity =>
            {
                entity.HasIndex(e => e.EmteaGroupId)
                    .HasName("IX_EmteaTypes_EmteaId");

                entity.Property(e => e.EmteaTypeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EmteaGroup)
                    .WithMany(p => p.EmteaTypes)
                    .HasForeignKey(d => d.EmteaGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmteaTypes_EmteaGroups");
            });

            modelBuilder.Entity<Emteas>(entity =>
            {
                entity.Property(e => e.EmteaCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmteaName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SubeCities>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.SubeCities)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubeCities_Cities");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.SubeCities)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubeCities_Subes");
            });

            modelBuilder.Entity<Subes>(entity =>
            {
                entity.HasIndex(e => e.BolgeId);

                entity.Property(e => e.SubeKod)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.SubeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Bolge)
                    .WithMany(p => p.Subes)
                    .HasForeignKey(d => d.BolgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subes_Bolges");
            });

            modelBuilder.Entity<Tuiks>(entity =>
            {
                entity.HasIndex(e => e.AddUserId);

                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.EmteaTypeId);

                entity.HasIndex(e => e.SubeId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.HasOne(d => d.AddUser)
                    .WithMany(p => p.TuiksAddUser)
                    .HasForeignKey(d => d.AddUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tuiks_Users");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Tuiks)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Tuiks_Cities");

                entity.HasOne(d => d.EmteaType)
                    .WithMany(p => p.Tuiks)
                    .HasForeignKey(d => d.EmteaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tuiks_EmteaTypes");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Tuiks)
                    .HasForeignKey(d => d.SubeId)
                    .HasConstraintName("FK_Tuiks_Subes");

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.TuiksUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId)
                    .HasConstraintName("FK_Tuiks_Users1");
            });

            modelBuilder.Entity<UserClaims>(entity =>
            {
                entity.HasIndex(e => e.ClaimId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClaims_Claims");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClaims_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.SubeId);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
