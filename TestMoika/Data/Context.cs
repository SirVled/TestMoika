using Microsoft.EntityFrameworkCore;
using TestMoika.Entites;

namespace TestMoika.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesData> SalesDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Заполнение начальными значениями бд

            var productsArray = new Product[]
            {
                new Product { Id = 1, Name = "BMW X8", Price = 8500000 },
                new Product { Id = 2, Name = "Lada Priora", Price = 450000 },
                new Product { Id = 3, Name = "Audi A6", Price = 4000000},
                new Product { Id = 4, Name = "Buganka", Price = 108000.87 }
            };


            var providedProductArray = new ProvidedProduct[]
            {
                new ProvidedProduct { Id = 1, SalesPointId = 1, ProductId = 2, ProductQuantity = 3 },
                new ProvidedProduct { Id = 2, SalesPointId = 2, ProductId = 1, ProductQuantity = 2 },
                new ProvidedProduct { Id = 3, SalesPointId = 1, ProductId = 3, ProductQuantity = 10 },
                new ProvidedProduct { Id = 4, SalesPointId = 1, ProductId = 4, ProductQuantity = 101 }
            };

            var salesPointArray = new SalesPoint[]
            {
                new SalesPoint { Id = 1, Name = "Метро ВДНХ" },
                new SalesPoint { Id = 2, Name = "Москва, улица Барклая, 6с5" }
            };

            var salesDataArray = new SalesData[]
            {
                new SalesData { Id = 1, SaleId = 1, ProductId = 2, ProductQuantity = 2, PropductIdAmount = productsArray[1].Price * 2 },
                new SalesData { Id = 2, SaleId = 2, ProductId = 1, ProductQuantity = 1, PropductIdAmount = productsArray[0].Price  },
                new SalesData { Id = 3, SaleId = 1, ProductId = 3, ProductQuantity = 5, PropductIdAmount = productsArray[2].Price * 5 },
                new SalesData { Id = 4, SaleId = 2, ProductId = 4, ProductQuantity = 10, PropductIdAmount = productsArray[3].Price * 10 }
            };

            var buyerArray = new Buyer[]
            {
                new Buyer { Id = 1, Name = "Вася Пупкин" },
                new Buyer { Id = 2, Name = "Максим Соколов" }
            };

            var salesArray = new Sale[]
            {
                new Sale
                {
                    Id = 1,
                    Date = DateTime.Now.AddDays(-2),
                    Time = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2)),
                    SalesPointId = 2,
                    BuyerId = 1,
                    TotalAmount = salesDataArray[0].PropductIdAmount + salesDataArray[3].PropductIdAmount,
                },
                new Sale
                {
                    Id = 2,
                    Date = DateTime.Now,
                    Time = DateTime.Now.TimeOfDay,
                    SalesPointId = 1,
                    BuyerId = 2,
                    TotalAmount = salesDataArray[1].PropductIdAmount + salesDataArray[2].PropductIdAmount,
                }
            };

            modelBuilder.Entity<Product>().HasData(productsArray);
            modelBuilder.Entity<ProvidedProduct>().HasData(providedProductArray);
            modelBuilder.Entity<SalesPoint>().HasData(salesPointArray);
            modelBuilder.Entity<SalesData>().HasData(salesDataArray);
            modelBuilder.Entity<Sale>().HasData(salesArray);
            modelBuilder.Entity<Buyer>().HasData(buyerArray);

            #endregion
            base.OnModelCreating(modelBuilder);           
        }

    }
}
