namespace SetupShop.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }

        public CartItem() { }

        public CartItem(Setup Product)
        {
            ProductId = Product.Id;
            ProductName = Product.Name;
            Price= Product.Price;
            Quantity = 1;
            Image = Product.Image;
        }
    }
}
