using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using BO_ERS;

namespace DAL_ERS
{
    public class Emp_Operations_DAL
    {
        string consrting = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        public SqlConnection logindal()
        {
            SqlConnection con = new SqlConnection(consrting);
            con.Open();
            return con;
        }

        public void editDAL(VacancyRequestBO b1, int vacancyID)
        {
            SqlConnection con = new SqlConnection(consrting);
            con.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_vacancy set vac_noofpositions = '" + b1.NoOfPosition + "' , vac_skills = '" + b1.skills + "' , vac_experience = '" + b1.Experience + "' , vac_location = '" + b1.Location + "' , vac_business_domain = '" + b1.Domain + "' , vac_requiredBydate = '" + b1.RequireByDate + "' where vac_vacancyID = '" + vacancyID + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
