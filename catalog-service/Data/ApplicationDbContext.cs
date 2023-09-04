using catalog_service.Model;
using Microsoft.EntityFrameworkCore;

namespace catalog_service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Category>()
                .HasData(GetCategories());

            builder.Entity<Product>()
                .HasData(GetProducts());
        }

        private Category[] GetCategories()
        {
            return new Category[]
            {
                new Category
                {
                    Name = "Alkohol",
                    VatRate = 19,
                    Id = Guid.Parse("c42d3be1-7684-4f0d-ba9a-f011981f1da2")
                },
                new Category
                {
                    Name = "Soft",
                    VatRate = 19,
                    Id = Guid.Parse("5bcc6ae7-a565-4a42-9995-ce657a4b6293")
                },
                new Category
                {
                    Name = "Zusatz",
                    VatRate = 7,
                    Id = Guid.Parse("708c04fd-4242-4e00-8035-2a966f45f69d")
                }
            };
        }

        private Product[] GetProducts()
        {
            return new Product[]
            {
                new Product
                {
                    Name = "Gin",
                    Description = "Gin",
                    Price = 18.5M,
                    Stock = 5,
                    CategoryId = Guid.Parse("c42d3be1-7684-4f0d-ba9a-f011981f1da2"),
                    Id = Guid.Parse("984688cc-509d-401b-98e0-fe41efdb397f")
                },
                new Product
                {
                    Name = "Jagdfürst",
                    Description = "Kostengünstige Alternative zu Jägermeister",
                    Price = 5.99M,
                    Stock = 21,
                    CategoryId = Guid.Parse("c42d3be1-7684-4f0d-ba9a-f011981f1da2"),
                    Id = Guid.Parse("d5c62730-c3d7-4c5c-9818-ded8318ba7e8")
                },
                new Product
                {
                    Name = "Bilig Energy",
                    Description = "Auch Energon genannt",
                    Price = 0.95M,
                    Stock = 12,
                    CategoryId = Guid.Parse("5bcc6ae7-a565-4a42-9995-ce657a4b6293"),
                    Id = Guid.Parse("32d8205f-6340-45f7-9da2-754b9fd4becf")
                },
                new Product
                {
                    Name = "Sauerkirsch",
                    Description = "Nektar",
                    Price = 1.15M,
                    Stock = 25,
                    CategoryId = Guid.Parse("5bcc6ae7-a565-4a42-9995-ce657a4b6293"),
                    Id = Guid.Parse("5d495be8-9c44-4680-9352-4f30e4b49f5b")
                },
            };
        }


    }
}
