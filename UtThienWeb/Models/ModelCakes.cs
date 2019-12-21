
namespace UtThienWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity;
    public partial class ModelCakes : DbContext
    {
        public ModelCakes()
            : base("name=ModelCakes")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseCatalog> CourseCatalogs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCatalog> NewsCatalogs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.AccountPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.CourseDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.ImageTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseCatalog>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CourseCatalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.Menu2)
                .HasForeignKey(e => e.MenuSubLi);

            modelBuilder.Entity<News>()
                .Property(e => e.NewsContent)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.News)
                .HasForeignKey(e => e.ImageTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NewsCatalog>()
                .HasMany(e => e.News)
                .WithOptional(e => e.NewsCatalog)
                .HasForeignKey(e => e.NewsTypeId);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.SlideImages)
                .IsFixedLength();

            modelBuilder.Entity<Teacher>()
                .Property(e => e.TeacherDescription)
                .IsUnicode(false);
        }
    }
}
