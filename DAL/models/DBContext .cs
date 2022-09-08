using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.models
{
    public partial class DBContext  : DbContext
    {
        public DBContext ()
        {
        }

        public DBContext (DbContextOptions<DBContext > options)
            : base(options)
        {
        }

        public virtual DbSet<CountingTbl> CountingTbls { get; set; }
        public virtual DbSet<KategoryTbl> KategoryTbls { get; set; }
        public virtual DbSet<PictureTbl> PictureTbls { get; set; }
        public virtual DbSet<UserTbl> UserTbls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Desktop\\projectKavLekav_shira\\DB_KAV_LEKAV\\DB.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CountingTbl>(entity =>
            {
                entity.HasKey(e => e.CodeCounting)
                    .HasName("PK__tmp_ms_x__37FCFE0B58E31FC8");

                entity.ToTable("Counting_TBL");

                entity.Property(e => e.CodeCounting).HasColumnName("codeCounting");

                entity.Property(e => e.NameCounting)
                    .HasMaxLength(30)
                    .HasColumnName("nameCounting")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<KategoryTbl>(entity =>
            {
                entity.HasKey(e => e.CodeKategory)
                    .HasName("PK__tmp_ms_x__A39F34E6437AEF5F");

                entity.ToTable("Kategory_TBL");

                entity.Property(e => e.NameKategory)
                    .HasMaxLength(30)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PictureTbl>(entity =>
            {
                entity.HasKey(e => e.CodePicture)
                    .HasName("PK__tmp_ms_x__E24E08C368D74FEC");

                entity.ToTable("Picture_TBL");

                entity.Property(e => e.CodeCounting).HasColumnName("codeCounting");

                entity.Property(e => e.RouteGoalPicture).HasMaxLength(50);

                entity.Property(e => e.RouteSoursePicture).HasMaxLength(50);

                entity.Property(e => e.UppDateToWeb).HasColumnType("datetime");

                entity.HasOne(d => d.CodeCountingNavigation)
                    .WithMany(p => p.PictureTbls)
                    .HasForeignKey(d => d.CodeCounting)
                    .HasConstraintName("FK_codeCounting");

                entity.HasOne(d => d.CodeKategoryNavigation)
                    .WithMany(p => p.PictureTbls)
                    .HasForeignKey(d => d.CodeKategory)
                    .HasConstraintName("FK_CodeKategory");

                entity.HasOne(d => d.CodeUserNavigation)
                    .WithMany(p => p.PictureTbls)
                    .HasForeignKey(d => d.CodeUser)
                    .HasConstraintName("FK_CodeUser");
            });

            modelBuilder.Entity<UserTbl>(entity =>
            {
                entity.HasKey(e => e.CodeUser)
                    .HasName("PK__tmp_ms_x__B2EF085BA544B9C3");

                entity.ToTable("User_TBL");

                entity.Property(e => e.UserClueForPass)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UserLastName)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.UserMail)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
