using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAL_ERS
{
    public class createdal
    {

        string c = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();

        public List<string> locdropdal()
        {

            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<string> dum = new List<string>();
            string queryStr = "Select distinct(Vac_Location) from E_Data_Vacancy where Vac_IsDeleted=0 and Vac_ApprovalStatus='Approved' and Vac_RecruitmentRequestID is null order by Vac_Location";
            SqlCommand com = new SqlCommand(queryStr, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                string a = dr.GetString(0);

                //d.Vac_Location = dr.GetString(4);
                dum.Add(a);

            }
            con.Close();
            return dum;

        }

        public List<Data_Vacancy> displayvacdal(string loc)
        {

            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<Data_Vacancy> dum = new List<Data_Vacancy>();
            string queryStr = "Select * from E_Data_Vacancy where Vac_IsDeleted=0 and Vac_ApprovalStatus='Approved' and Vac_RecruitmentRequestID is null and Vac_Location='" + loc + "'";
            SqlCommand com = new SqlCommand(queryStr, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Data_Vacancy d = new Data_Vacancy();
                d.Vac_VacancyID = dr.GetInt32(0);
                d.Vac_NoOfPositions = dr.GetInt32(1);
                d.Vac_Skills = dr.GetString(2);
                d.Vac_Experience = dr.GetInt32(3);
                d.Vac_Location = dr.GetString(4);
                d.Vac_Business_Domain = dr.GetString(5);
                d.Vac_RequiredByDate = dr.GetDateTime(6);

                dum.Add(d);

            }
            con.Close();
            return dum;

        }

        public void insertdal(string[] items, string plist)
        {
            Data_Recruitment r = new Data_Recruitment();
            SqlConnection con = new SqlConnection(c);
            con.Open();
            string queryStr = "insert into E_Data_Recruitment(Rec_PlacementConsultantID,Rec_RequesteddDate) values(@plist,getdate())";
            SqlCommand com = new SqlCommand(queryStr, con);
            com.Parameters.Add("@plist", SqlDbType.Int).Value = plist;
            com.ExecuteNonQuery();
            com.Dispose();

            //con.Open();
            string queryStr2 = "select max(Rec_RecruitmentRequestID) from E_Data_Recruitment where Rec_IsDeleted=0";
            SqlCommand com2 = new SqlCommand(queryStr2, con);
            int recid = (int)com2.ExecuteScalar();
            com2.Dispose();

            foreach (string s in items)
            {
                //con.Open();
                string queryStr3 = "update E_Data_Vacancy set Vac_RecruitmentRequestID=@rid where Vac_VacancyID=@vacid";
                SqlCommand com3 = new SqlCommand(queryStr3, con);
                com3.Parameters.Add("@rid", SqlDbType.Int).Value = recid;
                com3.Parameters.Add("@vacid", SqlDbType.Int).Value = s;
                com3.ExecuteNonQuery();
                com3.Dispose();
            }
            con.Close();




        }

        public List<int> pciddropdal(string loc)
        {
            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<int> dum = new List<int>();
            string queryStr = "Select * from E_Data_Placement_Consultant where PC_Location='" + loc + "'and PC_IsDeleted=0 order by PC_PlacementConsultantID";
            SqlCommand com = new SqlCommand(queryStr, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Data_Placement_Consultant d = new Data_Placement_Consultant();
                d.PC_PlacementConsultantID = dr.GetInt32(0);

                dum.Add(d.PC_PlacementConsultantID);

            }
            con.Close();
            return dum;

        }
    }
}
