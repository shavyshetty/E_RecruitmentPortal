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

    public class deletedal
    {
        string c = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        public List<int> reciddropdal()
        {
            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<int> dum = new List<int>();
            string queryStr = "Select distinct(Vac_RecruitmentRequestID) from E_Data_Vacancy where Vac_IsDeleted=0 and Vac_RecruitmentRequestID is not null order by Vac_RecruitmentRequestID";
            SqlCommand com = new SqlCommand(queryStr, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Data_Recruitment d = new Data_Recruitment();
                d.Rec_RecruitmentRequestID = dr.GetInt32(0);


                dum.Add(d.Rec_RecruitmentRequestID);

            }
            con.Close();
            return dum;

        }

        public List<Data_Vacancy> displaylistdal(string plist)
        {

            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<Data_Vacancy> dum = new List<Data_Vacancy>();
            string queryStr = "Select * from E_Data_Vacancy where Vac_IsDeleted=0  and Vac_RecruitmentRequestID=@plist";
            SqlCommand com = new SqlCommand(queryStr, con);
            com.Parameters.Add("@plist", SqlDbType.Int).Value = plist;
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
                //d.Vac_RequiredByDate = dr.GetString(6);
                d.Vac_RecruitmentRequestID = dr.GetInt32(8);
                dum.Add(d);

            }
            con.Close();
            return dum;

        }

        public void deleterecdal(string rlist)
        {

            SqlConnection con = new SqlConnection(c);
            con.Open();
            string queryStr = "update E_Data_Vacancy set Vac_IsDeleted=1 where Vac_RecruitmentRequestID=@rlist";
            SqlCommand com = new SqlCommand(queryStr, con);
            com.Parameters.Add("@rlist", SqlDbType.Int).Value = rlist;
            com.ExecuteNonQuery();
            com.Dispose();

            string queryStr2 = "update E_Data_Recruitment set Rec_IsDeleted=1 where Rec_RecruitmentRequestID=@rlist";
            SqlCommand com2 = new SqlCommand(queryStr2, con);
            com2.Parameters.Add("@rlist", SqlDbType.Int).Value = rlist;
            com2.ExecuteNonQuery();
            com2.Dispose();
            con.Close();
        }
    }

}
