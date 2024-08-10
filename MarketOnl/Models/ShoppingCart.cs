namespace MarketOnl.Models
{
    public class ShoppingCart
    {
        // Đây là kiểu của thuộc tính Items, nó là một danh sách các đối tượng ShoppingCartItem.
        public List<ShoppingCartItem> Items { get; set; }


        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }



        public void AddToCart(ShoppingCartItem item, int Quatity)
        {
            var checkExist = Items.FirstOrDefault(x => x.productId == item.productId);
            if (checkExist != null)
            {
                checkExist.Quantity += Quatity;
                checkExist.TotalPrice = checkExist.Price * checkExist.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }


          
        public decimal GetTotal()
        {
            return Items.Sum(x => x.TotalPrice);
        }

        public decimal GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }





    }
}
