namespace SalesProcessor.Models
{
    public class Customer : Person
    {
        public string Cnpj { get; set; }

        public string BusinessArea { get; set; }
    }
}
