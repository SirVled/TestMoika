using System.ComponentModel.DataAnnotations.Schema;

namespace TestMoika.Entites
{
    public class ProvidedProduct
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }    
        public int ProductQuantity { get; set; }

        [ForeignKey(nameof(SalesPoint))]
        public int SalesPointId { get; set; } 
    }
}
