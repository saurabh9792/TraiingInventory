namespace InventoryManagement.Models
{
	public class ChangePasswordDto
	{
        public string OLDPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
		public string? userId { get;  set; }
	}
}
