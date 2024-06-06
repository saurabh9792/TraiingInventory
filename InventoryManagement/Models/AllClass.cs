using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace InventoryManagement.Models
{
	public class AllClass
	{

        public int CategoryId { get; set; }

		public string CategoryName { get; set; }
		public string CategoryCode { get; set; }
		public string CategoryDesc { get; set; }
		public string IsActive { get; set; }

		public string IsDeleted { get; set; }

		public string CategoryImg { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }


        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

		public IFormFile CateImg { get; set;}

        public List<AllClass> GetAllCategory()
        {
            List<AllClass> categoryList = new List<AllClass>();
            DataLayer dl = new DataLayer();
            DataTable dt = dl.GetAllCategoryLis();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    categoryList.Add(new AllClass
                    {
                        CategoryId = Convert.ToInt32(row["CategoryId"]),
                        CategoryName = row["CategoryName"].ToString()
                    });
                }
            }

            return categoryList;
        }
    }
}
