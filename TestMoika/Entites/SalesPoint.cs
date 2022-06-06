namespace TestMoika.Entites
{
    public class SalesPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; }
    }
}
