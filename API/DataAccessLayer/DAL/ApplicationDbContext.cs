using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<TbAdmin> TbAdmins { get; set; }

        public virtual DbSet<TbAnswer> TbAnswers { get; set; }

        public virtual DbSet<TbCategory> TbCategories { get; set; }

        public virtual DbSet<TbConsultant> TbConsultants { get; set; }

        public virtual DbSet<TbConsultantCertification> TbConsultantCertifications { get; set; }

        public virtual DbSet<TbConsultantLanguage> TbConsultantLanguages { get; set; }

        public virtual DbSet<TbDiscussionForum> TbDiscussionForums { get; set; }

        public virtual DbSet<TbEquipment> TbEquipments { get; set; }

        public virtual DbSet<TbFarmer> TbFarmers { get; set; }

        public virtual DbSet<TbFarmerCrop> TbFarmerCrops { get; set; }

        public virtual DbSet<TbFarmerEquipment> TbFarmerEquipments { get; set; }

        public virtual DbSet<TbFarmerLibraryResource> TbFarmerLibraryResources { get; set; }

        public virtual DbSet<TbForumPost> TbForumPosts { get; set; }

        public virtual DbSet<TbPayment> TbPayments { get; set; }

        public virtual DbSet<TbPaymentGateway> TbPaymentGateways { get; set; }

        public virtual DbSet<TbQuery> TbQueries { get; set; }

        public virtual DbSet<TbRating> TbRatings { get; set; }

        public virtual DbSet<TbRental> TbRentals { get; set; }

        public virtual DbSet<TbRole> TbRoles { get; set; }

        public virtual DbSet<TbTempAnswer> TbTempAnswers { get; set; }

        public virtual DbSet<TbTempQuery> TbTempQueries { get; set; }

        public virtual DbSet<TbUser> TbUsers { get; set; }

        public virtual DbSet<VwGetAllAnswersWhereConsultantexperienceGreaterThan5> VwGetAllAnswersWhereConsultantexperienceGreaterThan5s { get; set; }

        public virtual DbSet<VwGetSpecificFarmerQuery> VwGetSpecificFarmerQueries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId).HasName("PK__tb_Admin__43AA4141AED7FC14");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User).WithMany(p => p.TbAdmins).HasConstraintName("FK__tb_Admin__user_i__31EC6D26");
            });

            modelBuilder.Entity<TbAnswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId).HasName("PK__tb_Answe__337243184129FA66");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Consultant).WithMany(p => p.TbAnswers).HasConstraintName("FK__tb_Answer__consu__6FE99F9F");

                entity.HasOne(d => d.Query).WithMany(p => p.TbAnswers).HasConstraintName("FK__tb_Answer__query__6EF57B66");
            });

            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__tb_Categ__D54EE9B4AB68E74D");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TbConsultant>(entity =>
            {
                entity.HasKey(e => e.ConsultantId).HasName("PK__tb_Consu__680695C429C52BF8");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.SubscriptionExpiry).HasDefaultValueSql("(NULL)");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User).WithMany(p => p.TbConsultants).HasConstraintName("FK__tb_Consul__user___3E52440B");
            });

            modelBuilder.Entity<TbConsultantCertification>(entity =>
            {
                entity.HasKey(e => e.CertificationNumber).HasName("PK__tb_Consu__1B9BADCFB5DE8E3F");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Consultant).WithMany(p => p.TbConsultantCertifications).HasConstraintName("FK__tb_Consul__consu__6A30C649");
            });

            modelBuilder.Entity<TbConsultantLanguage>(entity =>
            {
                entity.HasKey(e => new { e.ConsultantId, e.ConsultantLanguage }).HasName("PK__tb_Consu__1CDB456F20A417C8");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Consultant).WithMany(p => p.TbConsultantLanguages)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Consul__consu__656C112C");
            });

            modelBuilder.Entity<TbDiscussionForum>(entity =>
            {
                entity.HasKey(e => e.ForumId).HasName("PK__tb_Discu__69A2FA58D8A3C19D");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Farmer).WithMany(p => p.TbDiscussionForums)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Discus__farme__74AE54BC");
            });

            modelBuilder.Entity<TbEquipment>(entity =>
            {
                entity.HasKey(e => e.EquipmentId).HasName("PK__tb_Equip__197068AF0D467F85");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TbFarmer>(entity =>
            {
                entity.HasKey(e => e.FarmerId).HasName("PK__tb_Farme__C615582529A2E6B3");

                entity.Property(e => e.FarmingExperience).HasDefaultValue(0);
                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.MembershipExpiry).HasDefaultValueSql("(NULL)");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User).WithMany(p => p.TbFarmers).HasConstraintName("FK__tb_Farmer__user___38996AB5");
            });

            modelBuilder.Entity<TbFarmerCrop>(entity =>
            {
                entity.HasKey(e => new { e.FarmarId, e.Crop }).HasName("PK__tb_Farme__1B9206E9E8719B1C");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Farmar).WithMany(p => p.TbFarmerCrops)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Farmer__updat__52593CB8");
            });

            modelBuilder.Entity<TbFarmerEquipment>(entity =>
            {
                entity.HasKey(e => new { e.EquipmentId, e.FarmerId }).HasName("PK__tb_Farme__55113D2DAF1F1003");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Equipment).WithMany(p => p.TbFarmerEquipments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Farmer__updat__47DBAE45");

                entity.HasOne(d => d.Farmer).WithMany(p => p.TbFarmerEquipments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Farmer__farme__48CFD27E");
            });

            modelBuilder.Entity<TbFarmerLibraryResource>(entity =>
            {
                entity.HasKey(e => new { e.FarmerId, e.FarmerResource }).HasName("PK__tb_Farme__31BED6777574C161");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Farmer).WithMany(p => p.TbFarmerLibraryResources)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Farmer__farme__571DF1D5");
            });

            modelBuilder.Entity<TbForumPost>(entity =>
            {
                entity.HasKey(e => e.PostId).HasName("PK__tb_Forum__3ED78766E2F60448");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Farmer).WithMany(p => p.TbForumPosts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_ForumP__farme__7A672E12");

                entity.HasOne(d => d.Forum).WithMany(p => p.TbForumPosts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_ForumP__forum__797309D9");
            });

            modelBuilder.Entity<TbPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId).HasName("PK__tb_Payme__ED1FC9EA361258DA");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User).WithMany(p => p.TbPayments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Paymen__user___06CD04F7");
            });

            modelBuilder.Entity<TbPaymentGateway>(entity =>
            {
                entity.HasKey(e => e.GatewayId).HasName("PK__tb_Payme__0AF5B00BB068C095");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TbQuery>(entity =>
            {
                entity.HasKey(e => e.QueryId).HasName("PK__tb_Query__E793E349F0263CA1");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category).WithMany(p => p.TbQueries).HasConstraintName("FK__tb_Query__catego__60A75C0F");

                entity.HasOne(d => d.Farmer).WithMany(p => p.TbQueries)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Query__farmer__5FB337D6");
            });

            modelBuilder.Entity<TbRating>(entity =>
            {
                entity.HasKey(e => e.RatingId).HasName("PK__tb_Ratin__D35B278BBBC733EA");

                entity.HasOne(d => d.Query).WithMany(p => p.TbRatings)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Rating__query__7D439ABD");

                entity.HasOne(d => d.User).WithMany(p => p.TbRatings)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Rating__user___7E37BEF6");
            });

            modelBuilder.Entity<TbRental>(entity =>
            {
                entity.HasKey(e => e.RentalId).HasName("PK__tb_Renta__67DB611B512A52C5");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Equipment).WithMany(p => p.TbRentals)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Rental__updat__4D94879B");
            });

            modelBuilder.Entity<TbRole>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__tb_Role__760965CCC0372B9F");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TbTempAnswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId).HasName("PK__tb_TempA__33724318221596F2");
            });

            modelBuilder.Entity<TbTempQuery>(entity =>
            {
                entity.HasKey(e => e.QueryId).HasName("PK__tb_TempQ__E793E349F1D5D6DB");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__tb_Users__B9BE370F99E55C94");

                entity.Property(e => e.InsertedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Role).WithMany(p => p.TbUsers).HasConstraintName("FK__tb_Users__role_i__2D27B809");
            });

            modelBuilder.Entity<VwGetAllAnswersWhereConsultantexperienceGreaterThan5>(entity =>
            {
                entity.ToView("VW_Get_AllAnswersWhereConsultantexperienceGreaterThan5");
            });

            modelBuilder.Entity<VwGetSpecificFarmerQuery>(entity =>
            {
                entity.ToView("VW_Get_SpecificFarmerQueries");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
