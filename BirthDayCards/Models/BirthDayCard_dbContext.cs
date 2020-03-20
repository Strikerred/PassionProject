using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BirthDayCards.Models
{
    public partial class BirthDayCard_dbContext : DbContext
    {
        public BirthDayCard_dbContext()
        {
        }

        public BirthDayCard_dbContext(DbContextOptions<BirthDayCard_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BdEvent> BdEvent { get; set; }
        public virtual DbSet<BdayPerson> BdayPerson { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<PersonAccount> PersonAccount { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TemplateBdayPerson> TemplateBdayPerson { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<BdEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__BD_Event__7944C810C083E71A");

                entity.ToTable("BD_Event");

                entity.Property(e => e.BdayId).HasColumnName("BDayId");

                entity.Property(e => e.Eaddress)
                    .IsRequired()
                    .HasColumnName("EAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ecity)
                    .IsRequired()
                    .HasColumnName("ECity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Edate)
                    .HasColumnName("EDate")
                    .HasColumnType("date");

                entity.Property(e => e.Eemail)
                    .IsRequired()
                    .HasColumnName("EEmail")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ename)
                    .IsRequired()
                    .HasColumnName("EName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EphoneNumber)
                    .IsRequired()
                    .HasColumnName("EPhoneNumber")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EpostalCode)
                    .IsRequired()
                    .HasColumnName("EPostalCode")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Eprovince)
                    .IsRequired()
                    .HasColumnName("EProvince")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Etime)
                    .IsRequired()
                    .HasColumnName("ETime")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bday)
                    .WithMany(p => p.BdEvent)
                    .HasForeignKey(d => d.BdayId)
                    .HasConstraintName("FK__BD_Event__BDayId__04E4BC85");
            });

            modelBuilder.Entity<BdayPerson>(entity =>
            {
                entity.HasKey(e => e.BdayId)
                    .HasName("PK__BDayPers__E6AFE14EAA92BBC1");

                entity.ToTable("BDayPerson");

                entity.Property(e => e.BdayId).HasColumnName("BDayId");

                entity.Property(e => e.BdComingAge)
                    .IsRequired()
                    .HasColumnName("Bd_Coming_Age")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BdFirstName)
                    .IsRequired()
                    .HasColumnName("BD_FirstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BdLastName)
                    .IsRequired()
                    .HasColumnName("BD_LastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__Payments__9B556A38FA26ACDE");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Payments__UserNa__123EB7A3");
            });

            modelBuilder.Entity<PersonAccount>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__Person_A__AA2FFBE54A5DD26E");

                entity.ToTable("Person_Account");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.PersonAccount)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Person_Ac__UserN__0F624AF8");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Roles__8AFACE1A6D7D1B97");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateUrl)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateBdayPerson>(entity =>
            {
                entity.HasKey(e => e.TempBday)
                    .HasName("PK__Template__971E5D9344A9C485");

                entity.ToTable("Template_BdayPerson");

                entity.Property(e => e.TempBday).HasColumnName("Temp_Bday");

                entity.Property(e => e.BdayId).HasColumnName("BDayId");

                entity.HasOne(d => d.Bday)
                    .WithMany(p => p.TemplateBdayPerson)
                    .HasForeignKey(d => d.BdayId)
                    .HasConstraintName("FK__Template___BDayI__07C12930");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.TemplateBdayPerson)
                    .HasForeignKey(d => d.TemplateId)
                    .HasConstraintName("FK__Template___Templ__08B54D69");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F28457AFADA90D");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__75A278F5");
            });
        }
    }
}
