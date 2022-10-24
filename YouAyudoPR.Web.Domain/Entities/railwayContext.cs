using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YouAyudoPR.Web.Domain.Entities
{
    public partial class railwayContext : DbContext
    {
        public railwayContext()
        {
        }

        public railwayContext(DbContextOptions<railwayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activitylog> Activitylogs { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=containers-us-west-102.railway.app;Database=railway;Username=postgres;Password=21kqmcrl1m5nekTxFgJD;Port=7771");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("timescaledb");

            modelBuilder.Entity<Activitylog>(entity =>
            {
                entity.ToTable("activitylogs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdat)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdat");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Hoursvolunteered).HasColumnName("hoursvolunteered");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedat)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedat");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Activitylogs)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_event");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Activitylogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Createdat)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdat");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Enddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("enddate");

                entity.Property(e => e.Isactive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isactive");

                entity.Property(e => e.Isdeleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isdeleted");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.Startdate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("startdate");

                entity.Property(e => e.Websiteurl).HasColumnName("websiteurl");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_category");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organization");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("members");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organization");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("organizations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Isactive)
                    .IsRequired()
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Initial).HasColumnName("initial");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Resetpassword).HasColumnName("resetpassword");

                entity.Property(e => e.Secondlastname).HasColumnName("secondlastname");
            });

            modelBuilder.HasSequence("chunk_constraint_name", "_timescaledb_catalog");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
