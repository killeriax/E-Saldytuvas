namespace E_Saldytuvas.Server.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public Measure Measure { get; set; }
    }
}
