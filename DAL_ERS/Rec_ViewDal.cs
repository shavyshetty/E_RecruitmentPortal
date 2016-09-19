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
    public class viewdal
    {
        
        string c = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();

        public List<int> piddropdal()
        {
            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<int> dum = new List<int>();
            string queryStr = "Select distinct(Rec_PlacementConsultantID) from E_Data_Recruitment where Rec_IsDeleted=0 order by Rec_PlacementConsultantID";
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

        public List<Data_Vacancy> viewdisdal(string pid, string txt)
        {
            SqlConnection con = new SqlConnection(c);
            con.Open();
            List<Data_Vacancy> dum = new List<Data_Vacancy>();
            string queryStr = "";
            SqlCommand com;
            if (pid != "" && txt != "")
            {
                queryStr = "Select * from E_Data_Vacancy where Vac_IsDeleted=0  and Vac_RecruitmentRequestID in (select Rec_RecruitmentRequestID from E_Data_Recruitment where Rec_PlacementConsultantID=@pid and Rec_RequesteddDate between '" + txt + " 00:00:00.000' AND '" + txt + " 23:59:59.999')";
                com = new SqlCommand(queryStr, con);
                com.Parameters.Add("@pid", SqlDbType.Int).Value = pid;
                //com.Parameters.Add("@dt", SqlDbType.Date).Value = txt;

            }
            else if (pid != "" && txt == "")
            {
                queryStr = "Select * from E_Data_Vacancy where Vac_IsDeleted=0  and Vac_RecruitmentRequestID in (select Rec_RecruitmentRequestID from E_Data_Recruitment where Rec_PlacementConsultantID=@pid)";
                com = new SqlCommand(queryStr, con);
                com.Parameters.Add("@pid", SqlDbType.Int).Value = pid;
                //com.Parameters.Add("@dt", SqlDbType.Date).Value = txt;
            }
            else
            {
                queryStr = "Select * from E_Data_Vacancy where Vac_IsDeleted=0  and Vac_RecruitmentRequestID in (select Rec_RecruitmentRequestID from E_Data_Recruitment where  Rec_RequesteddDate between '" + txt + " 00:00:00.000' AND '" + txt + " 23:59:59.999')";
                com = new SqlCommand(queryStr, con);
                //com.Parameters.Add("@pid", SqlDbType.Int).Value = pid;
                //com.Parameters.Add("@dt", SqlDbType.Date).Value = txt;
            }

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
    }
}
