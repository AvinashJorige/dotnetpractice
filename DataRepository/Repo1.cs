using System.Data;
using System.Data.SqlClient;

namespace DataRepository
{
    public class Repo1
    {
        private readonly SqlConnection _conn = null;
        public Repo1()
        {
            this._conn = new SqlConnection("Data Source=DESKTOP-13LL2HE\\SQLSERVER;Initial Catalog=Practice;Persist Security Info=True;User ID=sa;Password=jorige");
        }

        public bool ValidateUserLogin(string UserId, string Password)
        {
            DataTable dt = new DataTable();
            bool flag = false;

            if(!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Password))
            {
                return flag;
            }           

            SqlCommand sqlCommand = new SqlCommand(@"select isnull(COUNT(id),0) from tbl_Emp_Password where EmpId in (
                                                    select EmpId from Practice..Tbl_Emp_Master_M where LeavingDate is null and IsIILEmp = 1 and Status = 1)
                                                    and EmpId = '" + UserId + "' and Password = '" + Password +"'");
            _conn.Open();
            SqlDataReader sqlData = sqlCommand.ExecuteReader();
            dt.Load(sqlData);

            if(dt != null && dt.Rows.Count > 0)
            {
                if(dt.Rows[0][0].ToString() == "0")
                {
                    return flag;
                }                
            }

            return true;
        }
    }
}
