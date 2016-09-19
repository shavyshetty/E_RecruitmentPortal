using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Configuration;

namespace DAL_ERS
{
    public class LoginDAL
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        SqlConnection con;

        public LoginBO LoginCheckDAL(LoginBO lb)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from E_Data_login where DL_LoginID='" + lb.Login_ID + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                LoginBO lbovalues = new LoginBO();

                lbovalues.Login_ID = (int)dr[0];
                lbovalues.Login_Password = dr[1].ToString();
                lbovalues.Login_Designation = Convert.ToString(dr[2]);
                lbovalues.Login_Role = dr[3].ToString();
                return lbovalues;
            }
            con.Close();
            return null;
        }

        public void reset_password_dal(LoginBO lb)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_Login set DL_Password = '" + lb.Login_Password + "' Where DL_LoginID=" + lb.Login_ID + "", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public string chk_change_password_dal(LoginBO lb)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select DL_Password from E_Data_login where DL_LoginID='" + lb.Login_ID + "'", con);
            string cur_db_pass = Convert.ToString(cmd.ExecuteScalar());
            return cur_db_pass;


        }

        public void change_password_dal(LoginBO lb)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_Login set DL_Password = '" + lb.Login_Password + "' Where DL_LoginID=" + lb.Login_ID + "", con);
            cmd.ExecuteNonQuery();
            con.Close();


        }
    }
}
