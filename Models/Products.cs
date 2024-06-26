namespace WebApplicationn.Models
{
    public class Products
    {
        public int productNo { get; set; }
        public string productName { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        //public int quantity { get; set; }
        public Int64 price { get; set; }
        //public Int64 finalPrice { get; set; }
        //public DateTime Date { get; set; }
        public string description { get; set; } = string.Empty;
        public int stars { get; set; }
        public int reviewCount { get; set; }

    }
}