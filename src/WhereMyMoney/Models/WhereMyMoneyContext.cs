using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WhereMyMoney.Models
{
    public partial class WhereMyMoneyContext : DbContext
    {
        //Scaffold-DbContext "Server=WIN-01\WIN_01_SQL;Database=WhereMyMoney;User Id=sa;Password=R6zntmydkm2?" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/MockData

        public WhereMyMoneyContext(DbContextOptions<WhereMyMoneyContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbl_Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbl_Category)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tbl_Category_Tbl_User");
            });

            modelBuilder.Entity<Tbl_Currency>(entity =>
            {
                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CurrencyShortName)
                    .IsRequired()
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.Description).HasColumnType("ntext");
            });

            modelBuilder.Entity<Tbl_Trace>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.TraceDate).HasColumnType("date");

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tbl_Trace)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tbl_Trace_Tbl_Category");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Tbl_Trace)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tbl_Trace_Tbl_Currency");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbl_Trace)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tbl_Trace_Tbl_User");
            });

            modelBuilder.Entity<Tbl_User>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });
        }

        public virtual DbSet<Tbl_Category> Tbl_Category { get; set; }
        public virtual DbSet<Tbl_Currency> Tbl_Currency { get; set; }
        public virtual DbSet<Tbl_Trace> Tbl_Trace { get; set; }
        public virtual DbSet<Tbl_User> Tbl_User { get; set; }
    }
}