using System.ComponentModel.DataAnnotations.Schema;

namespace TestMoika.Entites
{
    public class SalesData
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public double PropductIdAmount { get; set; }

        [ForeignKey(nameof(Sale))]
        public int SaleId { get; set; }
    }
}
