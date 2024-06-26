namespace WebApplicationn.Models
{
    public class Cart
    {
        public int productNo { get; set; }
        public string productName { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public int quantity { get; set; }
        public Int64 price { get; set; }
        public Int64 finalPrice { get; set; }
        public DateTime Date { get; set; }

    }


}
