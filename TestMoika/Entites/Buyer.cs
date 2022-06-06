namespace TestMoika.Entites
{
    public class Buyer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
