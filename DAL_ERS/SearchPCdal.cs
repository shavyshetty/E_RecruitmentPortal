using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using BO_ERS;


namespace DAL_ERS
{
    public class SearchPCdal
    {
        string c = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        public List<PC_BO> searchdal(string txt)
        {
            List<PC_BO> pclist = new List<PC_BO>();
            SqlConnection con = new SqlConnection(c);
            con.Open();

            string queryStr = "Select * from E_Data_Placement_Consultant where PC_IsDeleted=0 and PC_PlacementConsultantID in(select PC_PlacementConsultantID from E_Data_Placement_Consultant where PC_PlacementConsultantName like  '%" + txt + "%' or PC_Location like '%" + txt + "%')";
            SqlCommand com = new SqlCommand(queryStr, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                PC_BO d = new PC_BO();
                d.pc_id = dr.GetInt32(0);
                d.pc_name = dr.GetString(1);
                d.pc_location = dr.GetString(3);
                d.pc_email = dr.GetString(4);
                pclist.Add(d);

            }
            con.Close();
            return pclist;

        }
    }
}
