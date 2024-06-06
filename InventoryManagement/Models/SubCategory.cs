namespace InventoryManagement.Models
{
	public class SubCategory
	{
        public int SubCcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCcategoryName { get; set; }
        public string SubCcategoryDesc { get; set; }
        public string SubCcategoryImg { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set;}
        public DateTime CreatedOn { get; set;}


    }
}
