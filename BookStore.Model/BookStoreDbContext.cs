using BookStore.Model.Models;
using System.Data.Entity;

namespace BookStore.Model
{
    internal class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext() : base("BookStoreConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Banner> Banners { set; get; }       
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }       
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }      
        public DbSet<Slide> Slides { set; get; }      
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}