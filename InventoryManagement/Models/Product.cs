using System.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

        public IFormFile ProductImg { get; set; }


       
    }
}
