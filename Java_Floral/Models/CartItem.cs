
namespace Java_Floral.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal price { get; set; }
        public string Image { get; set; }

        public int Quantity{ get; set; }


        //_____ yaa nhin lgain gaa too ---> voo  ---> nullaa ka a-----. Error daa gaa
        public CartItem()
        {

        }

        public CartItem(Products product)
        {
            ProductId = product.id;
            ProductName = product.Ptitle;
            price = product.Price;
            Image = product.PImage;

            //____ Extraa work ___
            Quantity = 1;
        }
    }
}
