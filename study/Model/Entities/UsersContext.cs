using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace study.Model.Entities;

public partial class UsersContext : DbContext
{
    public UsersContext()
    {
    }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07ABA6BD4D");

            entity.ToTable("userprofile");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4F3A7703F").IsUnique();

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
