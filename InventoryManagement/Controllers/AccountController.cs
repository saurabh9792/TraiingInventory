using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventoryManagement.Controllers
{
    public class AccountController : Controller
    {

        DataLayer dl = new DataLayer();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            try
            {

                if (model != null)
                {
                    DataTable dt = dl.GetLoginDetails(model);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string pass = dt.Rows[0]["password"].ToString();
                        string RoleId = Convert.ToString(dt.Rows[0]["RoleId"].ToString());
                        string username = dt.Rows[0]["UserName"].ToString();
                        string UserId = dt.Rows[0]["UserId"].ToString();



                        if (pass == model.Password)
                        {

                            HttpContext.Session.SetString("Username", username);
                            HttpContext.Session.SetString("RoleID", RoleId);
                            HttpContext.Session.SetString("UserdId", UserId);


                            if (RoleId == "1")
                            {

                                return RedirectToAction("Index", "Dashboard");

                            }
                        }
                        else
                        {
                            ViewBag.msg = "Invalid UserName or Password";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Invalid UserName or Password";
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }




       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
			{
				Console.WriteLine("Session cleared successfully.");
			}
			else
			{
				Console.WriteLine("Session clearing failed.");
			}

			return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult ChangePasswod()
        {

            return View();
        }

		[HttpPost]
		public IActionResult ChangePasswod(string oldPass,string confirmPass)
		{
			ChangePasswordDto obj = new ChangePasswordDto();
            string msg = "";
            if (oldPass!=null)
            {
                obj.OLDPassword = oldPass;
            }
			if (confirmPass != null)
			{
				obj.ConfirmPassword = confirmPass;
			}
			obj.userId = HttpContext.Session.GetString("UserdId");
            bool status=dl.ChangePasswords(obj);
            if (status==true)
            {
                msg = "Password Changes Sucessfully"
;            }
            else
            {
                msg = "Some thing went wrong";

			}


			return Json(msg);
		}




	}
}
