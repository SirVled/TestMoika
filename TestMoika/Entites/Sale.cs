
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestMoika.Entites
{
    public class Sale
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата осуществления продажи
        /// </summary>
        public DateTime Date { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Время осуществления продажи
        /// </summary>
        public TimeSpan Time { get; set; }

        [ForeignKey(nameof(SalesPoint))]
        public int SalesPointId { get; set; }
       //ublic SalesPoint SalesPoint { get; set; }

        [ForeignKey(nameof(Buyer))]
        public int? BuyerId { get; set; } = null;

        public ICollection<SalesData> SalesData { get; set; }

        public double TotalAmount { get; set; }

    }
}
