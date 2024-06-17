
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagement.Models
{
    public class DataLayer
    {
        string Connectionstring = "";
        public DataLayer()
        {
            Connectionstring = "Data Source=LAPTOP-ETEAB50Q;Initial Catalog=Inventory;Integrated Security=True;";
        }

        #region common method

        private DataTable ExecuteProcedureToGetDataTable(string procedure, SqlParameter[] sp)
        {
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand sqlCmd = new SqlCommand(procedure, con);
                DataTable dt = new DataTable();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                if (sp != null && sp.Length > 0)
                {
                    sqlCmd.Parameters.AddRange(sp);
                }
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                return dt;
            }
        }



        private bool ExcuteProcudureToInsert(string procedure, SqlParameter[] sp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstring))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(procedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (sp != null && sp.Length > 0)
                    {
                        cmd.Parameters.AddRange(sp);
                    }
                    int n = cmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception ex)
            {

                return false;
            }


        }



        #endregion



        public bool InsertCategory(AllClass obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_InsertCAtegory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryDesc", obj.CategoryDesc);
                    cmd.Parameters.AddWithValue("@CategoryCode", obj.CategoryCode);
                    cmd.Parameters.AddWithValue("@CategoryImg", obj.CategoryImg == null ? "" : obj.CategoryImg);
                    int n = cmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }



                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public DataTable GetLoginDetails(Login model)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Login", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", model.UserName);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ChangePasswords(ChangePasswordDto obj)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(Connectionstring))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Sp_ChnagePassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", obj.userId);
                    cmd.Parameters.AddWithValue("@OLDPass", obj.OLDPassword);
                    cmd.Parameters.AddWithValue("@newPass", obj.NewPassword == null ? "" : obj.NewPassword);
                    cmd.Parameters.AddWithValue("@comfirmPass", obj.ConfirmPassword);
                    int n = cmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public DataTable GetAllCategoryLis()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {

                string sql = "select CategoryId,CategoryName from tbl_Category where IsActive=1 and IsDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;

            }



        }

        public bool InsertSubCategory(SubCategory model)
        {
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_InsertSubCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubCatName", model.SubCcategoryName);
                cmd.Parameters.AddWithValue("@SubCatDec", model.SubCcategoryDesc);
                cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                cmd.Parameters.AddWithValue("@SubcateImagePath", model.SubCcategoryImg == null ? "" : model.SubCcategoryImg);
                cmd.Parameters.AddWithValue("@CreatedBy", model.SubCcategoryImg == null ? "" : model.SubCcategoryImg);
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public DataTable GetSubCategoryList()
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(Connectionstring))
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_GetSubCategoryDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetSubCategoryListByCateId(string cateId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstring))
                {

                    string sql = "select SubCateId,SubCatName from tbl_SubCategory where IsActive=1 and IsDeleted=0 and CategoryId='" + cateId + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public bool AddBrand(Brand model)
        {
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_InserBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BrandName", model.BrandName);
                cmd.Parameters.AddWithValue("@BrandDesc", model.BrandName);
                cmd.Parameters.AddWithValue("@BrandImg", model.BrandImagePath == null ? "" : model.BrandImagePath);
                cmd.Parameters.AddWithValue("@CretetdBy", model.CreatedBy);
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
        }

        public DataTable GetBrandDetailsLis()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(Connectionstring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_GetBrandDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;


                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetBrandDetailsById(string brandId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_getBrandById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BrandId", Convert.ToInt32(brandId));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

        public bool EditBrand(Brand model)
        {
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BrandId", model.BrandId);
                cmd.Parameters.AddWithValue("@BrandName", model.BrandName);
                cmd.Parameters.AddWithValue("@BrandDesc", model.BrandDescription);
                cmd.Parameters.AddWithValue("@BrandImg", model.BrandImagePath == null ? "" : model.BrandImagePath);
                cmd.Parameters.AddWithValue("@UpdatedBy", model.UpdatedBy);
                int n = cmd.ExecuteNonQuery();
                con.Close();
                if (n > 0)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
        }

        public bool InsertPurachseData(PurchaseDto model)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlParameter[] sp = new SqlParameter[6];
                sp[0] = new SqlParameter("@SuplierName", model.SuplierName == null ? "" : model.SuplierName);
                sp[1] = new SqlParameter("@BillNo", model.BillNo == null ? "" : model.BillNo);
                sp[2] = new SqlParameter("@BillDeatils", model.BillDeatils == null ? "" : model.BillDeatils);
                sp[3] = new SqlParameter("@MobileNo", model.MobileNo == null ? "" : model.MobileNo);
                sp[4] = new SqlParameter("@PurchaseDate", model.PurchaseDate == null ? "" : model.PurchaseDate);
                sp[5] = new SqlParameter("@CreadteBy", model.CreatedBy == null ? "" : model.CreatedBy);
                dt = ExecuteProcedureToGetDataTable("sp_insertPurachase", sp);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var purchaseId = dt.Rows[0]["id"];

                    if (model.PurchaseItem.Count > 0)
                    {
                        foreach (var item in model.PurchaseItem)
                        {
                            SqlParameter[] sp1 = new SqlParameter[4];
                            sp1[0] = new SqlParameter("@PurchaseId", purchaseId);
                            sp1[1] = new SqlParameter("@ItemName", item.itemName==null?"":item.itemName);
                            sp1[2] = new SqlParameter("@Qunatity", item.quantity == null ? "" : item.quantity);                          
                            sp1[3] = new SqlParameter("@PurchaseAmount", item.purchaseprice==null?"":item.purchaseprice);

                            bool status = ExcuteProcudureToInsert("sp_purchaseItem", sp1);
                            if (status)
                            {
                                return true;
                            }
                        }
                    }
                }

                return true;


            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
    }
}

