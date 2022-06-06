
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMoika.Entites
{
    public class Sale
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата осуществления продажи
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Время осуществления продажи
        /// </summary>
        public TimeSpan Time { get; set; }

        [ForeignKey(nameof(SalesPoint))]
        public int SalesPointId { get; set; }
        public SalesPoint SalesPoint { get; set; }

        public ICollection<SalesData> SalesData { get; set; }

        public double TotalAmount { get; set; }

    }
}
