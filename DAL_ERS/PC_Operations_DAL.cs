using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL_ERS
{
    public class PC_Operations_DAL
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        
        public int add_pc_dal(PC_BO b)
        {
            int pw1;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd_demo1 = new SqlCommand("select max(PC_PlacementConsultantID) from E_Data_Placement_Consultant ", con);
            if (cmd_demo1.ExecuteScalar() is DBNull)
            { 
                pw1=0;
            }
            else  pw1 = Convert.ToInt32(cmd_demo1.ExecuteScalar()) + 1;
            string pw2 = b.pc_name.ToString().Substring(0, 2);
            string pwd = string.Concat(pw1, pw2);
            SqlCommand cmd = new SqlCommand("insert into E_Data_Placement_Consultant values('" + b.pc_name + "','" + pwd + "','" + b.pc_location + "','" + b.pc_email + "',0)", con);
            int n = cmd.ExecuteNonQuery();
            con.Close();
            return n;

        }
        public List<PC_BO> dal_pc_view()
        {

            List<PC_BO> b = new List<PC_BO>();

            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from E_Data_Placement_Consultant where PC_IsDeleted=0", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                PC_BO bo = new PC_BO();
                bo.pc_id = Convert.ToInt32(dr[0]);
                bo.pc_name = Convert.ToString(dr[1]);
                bo.pc_password = Convert.ToString(dr[2]);
                bo.pc_location = Convert.ToString(dr[3]);
                bo.pc_email = Convert.ToString(dr[4]);
                b.Add(bo);
            }
            con.Close();
            return b;
        }
        public void edit_pc_DAL(int id, PC_BO b)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_Placement_Consultant set PC_PlacementConsultantName = '" + b.pc_name + "' , PC_Location = '" + b.pc_location + "',PC_Email='" + b.pc_email + "' Where PC_PlacementConsultantID=" + id + "", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void del_pc_dal(int id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_Placement_Consultant set PC_IsDeleted=1 Where PC_PlacementConsultantID=" + id + "", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //public int p_id;
        //public void editshow_pc_dal1(int id)
        //{
        //   p_id=id;
        //}
        //public List<PC_BO> editshow_pc_dal()
        //{
        //    SqlConnection con = new SqlConnection(ConnectionString);
        //    con.Open();
        //    List<PC_BO> b = new List<PC_BO>();
        //    SqlCommand cmd = new SqlCommand("select * from E_Data_Placement_Consultant where PC_IsDeleted=0 and PC_PlacementConsultantID=" + id + "", con);
        //    SqlDataReader dr = cmd.ExecuteReader();
            
        //    while (dr.Read())
        //    {
        //        PC_BO bo = new PC_BO();   
        //        bo.pc_id = Convert.ToInt32(dr[0]);
        //        bo.pc_name = Convert.ToString(dr[1]);
        //        bo.pc_password = Convert.ToString(dr[2]);
        //        bo.pc_location = Convert.ToString(dr[3]);
        //        bo.pc_email = Convert.ToString(dr[4]);
        //        b.Add(bo);
        //    }
        //    con.Close();
        //    return b;
        //}

    }
}
