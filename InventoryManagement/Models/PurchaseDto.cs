namespace InventoryManagement.Models
{
	public class PurchaseDto
	{
        public int PurchaseId { get; set; }
        public string SuplierName { get; set; }
        public string BillNo { get; set; }
        public string BillDeatils { get; set; }
        public string GSTN  { get; set; }
        public string MobileNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string DiscountPrice { get; set; }
        public string GrandTotal { get; set; }

        public List<PurchaseItem> PurachaseItem { get; set; }=new List<PurchaseItem>();
        public string CreatedBy { get;  set; }
    }



    public class PurchaseItem
    {

        public int PurchaseItemId { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public decimal purchaseprice { get; set; }
        public decimal totalcosst { get; set; }
    }



}
