using P3AddNewFunctionalityDotNetCore.Models.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Models
{
    public class Cart : ICart
    {
        private readonly List<CartLine> _cartLines;

        public Cart()
        {
            _cartLines = new List<CartLine>();
        }

        public void AddItem(Product product, int quantity)
        {
            CartLine line = _cartLines.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line == null)
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) => _cartLines.RemoveAll(l => l.Product.Id == product.Id);

        public double GetTotalValue()
        {
            double total = 0.0;
            if (Lines != null)
            {
                foreach (CartLine cartLine in Lines)
                {

                    total += (cartLine.Product.Price * cartLine.Quantity);                                    

                }

                return total;
            }
            else
            {
                return total;
            }
        }

        public string TotalPriceMoney() { return CultureInfo.CurrentCulture.Name == "fr" ? GetTotalValue().ToString("C").Replace("¤", "€") : GetTotalValue().ToString("C").Replace("¤", "$"); }

        public double GetAverageValue()
        {
            if (Lines != null && _cartLines.Count > 0)
            {
                int totalQuantity = 0;
                foreach (CartLine cartLine in Lines)
                {
                    totalQuantity += cartLine.Quantity;
                }
                return GetTotalValue() / totalQuantity;
            }
            else
            {
                return 0.0;
            }
        }
        public string AveragePriceMoney() { return CultureInfo.CurrentCulture.Name == "fr" ? GetAverageValue().ToString("C").Replace("¤", "€") : GetAverageValue().ToString("C").Replace("¤", "$"); }

        public void Clear() => _cartLines.Clear();

        public IEnumerable<CartLine> Lines => _cartLines;
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
