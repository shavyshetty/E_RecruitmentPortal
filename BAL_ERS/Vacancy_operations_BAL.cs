using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using DAL_ERS;
using System.Data.SqlClient;
using System.Collections;

namespace BAL_ERS
{
    public class Vacancy_operations_BAL
    {
        Emp_Operations_DAL d = new Emp_Operations_DAL();
        public int createvacancy(VacancyRequestBO vcbo, int id, string desg)
        {

            SqlConnection conn = d.logindal();
            SqlCommand cmd = null;
            if (desg == "ASE")
            {
                cmd = new SqlCommand("insert into E_Data_Vacancy(Vac_NoOfPositions,Vac_Skills,Vac_Experience,Vac_Location,Vac_Business_Domain,Vac_RequiredByDate,Vac_Status,Vac_RecruitmentRequestID,Vac_ApprovalStatus,Vac_IsDeleted) values('" + Convert.ToInt32(vcbo.NoOfPosition) + "','" + Convert.ToString(vcbo.skills) + "','" + Convert.ToInt32(vcbo.Experience) + "','" + Convert.ToString(vcbo.Location) + "','" + Convert.ToString(vcbo.Domain) + "','" + Convert.ToDateTime(vcbo.RequireByDate) + "','Open',Null,'Pending',0)", conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else
                if (desg == "UH")
                {
                    cmd = new SqlCommand("insert into E_Data_Vacancy(Vac_NoOfPositions,Vac_Skills,Vac_Experience,Vac_Location,Vac_Business_Domain,Vac_RequiredByDate,Vac_Status,Vac_RecruitmentRequestID,Vac_ApprovalStatus,Vac_IsDeleted) values('" + Convert.ToInt32(vcbo.NoOfPosition) + "','" + Convert.ToString(vcbo.skills) + "','" + Convert.ToInt32(vcbo.Experience) + "','" + Convert.ToString(vcbo.Location) + "','" + Convert.ToString(vcbo.Domain) + "','" + Convert.ToDateTime(vcbo.RequireByDate) + "','Open',Null,'Approved',0)", conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            string n = "select max(Vac_VacancyId) from E_Data_Vacancy ";
            cmd = new SqlCommand(n, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int x = 0;
            while (dr.Read())
            {
                x = (int)dr[0];
            }
            dr.Close();
            cmd.Dispose();
            cmd = new SqlCommand("Insert into E_Link_Vacancy_request values (" + x + "," + id + ")", conn);
         
            return cmd.ExecuteNonQuery();
            //conn.Close();


        }

        public List<DisplayBO> DispList(int empid, string desg)
        {

            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("select VR_VacancyRequestID from E_Link_Vacancy_request where VR_EmployeeID='" + empid + "'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<int> vacancy_id = new List<int>();
            while (dr.Read())
            {
                int a = (int)dr[0];
                vacancy_id.Add(a);
            }
            cmd.Dispose();
            dr.Close();
            string vacancy_idstring = "";
            vacancy_idstring = vacancy_id[0] + "";
            for (int i = 0; i < vacancy_id.Count; i++)
            {
                vacancy_idstring = vacancy_idstring + "," + vacancy_id[i];
            }
            if (desg == "ASE")
            {
                string query = "select * from E_Data_Vacancy where Vac_IsDeleted <> 1 and Vac_ApprovalStatus = 'Pending' and Vac_VacancyID IN (" + vacancy_idstring + ")";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr2 = cmd.ExecuteReader();
            List<DisplayBO> vrlist = new List<DisplayBO>();
                while (dr2.Read())
                {
                    DisplayBO vcbo = new DisplayBO();
                    vcbo.Vac_VacancyID = (int)dr2[0];

                    vcbo.NoOfPosition = (int)dr2[1];
                    vcbo.skills = Convert.ToString(dr2[2]);
                    vcbo.Experience = Convert.ToInt32(dr2[3]);
                    vcbo.Location = Convert.ToString(dr2[4]);
                    vcbo.Domain = Convert.ToString(dr2[5]);
                    vcbo.RequireByDate = Convert.ToDateTime(dr2[6]);

                    vrlist.Add(vcbo);
                }
                conn.Close();
                return vrlist;
            }

               
          
    
            else if (desg == "UH")
            {
                string query = "select * from E_Data_Vacancy where Vac_IsDeleted <> 1 and Vac_ApprovalStatus = 'Approved' and Vac_VacancyID IN (" + vacancy_idstring + ")";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr2 = cmd.ExecuteReader();
            
                //    dr2.Close();

                //    cmd.Dispose();
                //}
                //}            //for (int i = 1; i < vacancy_id.Count; i++)
                ////{
                ////    vacancy_idstring = vacancy_idstring + "," + vacancy_id[i];
                ////}
                //string query1 = "select * from E_Data_Vacancy where Vac_IsDeleted <> 1 and Vac_ApprovalStatus = 'Approved' and Vac_VacancyID IN (" + vacancy_idstring + ")";
                //cmd = new SqlCommand(query1, conn);
                //SqlDataReader dr1 = cmd.ExecuteReader();
                List<DisplayBO> vrlist = new List<DisplayBO>();
                while (dr2.Read())
                {
                    DisplayBO vcbo = new DisplayBO();
                    vcbo.Vac_VacancyID = (int)dr2[0];

                    vcbo.NoOfPosition = (int)dr2[1];
                    vcbo.skills = Convert.ToString(dr2[2]);
                    vcbo.Experience = Convert.ToInt32(dr2[3]);
                    vcbo.Location = Convert.ToString(dr2[4]);
                    vcbo.Domain = Convert.ToString(dr2[5]);
                    vcbo.RequireByDate = Convert.ToDateTime(dr2[6]);

                    vrlist.Add(vcbo);
                }
                conn.Close();
                return vrlist;
            }
            return null;
        }

        public List<DeleteVacancyBO> delete(int Vac_VacancyID)
        {
            List<DeleteVacancyBO> dellist = new List<DeleteVacancyBO>();

            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("select Vac_VacancyID,Vac_NoOfPositions,Vac_Skills,Vac_Experience,Vac_Location,Vac_Business_Domain,Vac_RequiredByDate,Vac_Status,Vac_ApprovalStatus,Vac_IsDeleted from E_Data_Vacancy where Vac_VacancyID='" + Vac_VacancyID + "'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DeleteVacancyBO e = new DeleteVacancyBO();
                e.Vac_VacancyID = Convert.ToInt32(dr[0]);
                e.Vac_NoOfPosition = Convert.ToInt32(dr[1]);
                e.Vac_skill = Convert.ToString(dr[2]);
                e.Vac_Experience = Convert.ToInt32(dr[3]);
                e.Vac_location = Convert.ToString(dr[4]);
                e.Vac_BusinessDomain = Convert.ToString(dr[5]);
                e.Vac_RequiredByDate = dr[6].ToString();
                e.Vac_Status = Convert.ToString(dr[7]);

                e.Vac_ApprovalStatus = dr[9].ToString();
                //e.Vac_isdeleted = Convert.ToInt32(dr[10]);

                dellist.Add(e);

            }
            conn.Close();
            return dellist;

        }
        public void del(int id)
        {

            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("update E_Data_Vacancy set Vac_IsDeleted=1 where Vac_VacancyID='" + id + "'", conn);
            cmd.ExecuteNonQuery();


        }

        public List<ApproveVacancyBO> approve(int emp_id)
        {

            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("select Emp_EmployeeID from E_Data_Employee where Emp_UnitHeadID=" + emp_id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<int> empidlist = new List<int>();
            while (dr.Read())
            {
                int temp = (int)dr[0];
                empidlist.Add(temp);
            }
            cmd.Dispose();
            dr.Close();
            string empidstring = "";
            if (empidlist.Count > 0)
            {
                empidstring = empidlist[0] + "";
                for (int i = 1; i < empidlist.Count; i++)
                {
                    empidstring = empidstring + "," + empidlist[i];
                }

                string qry = "select VR_VacancyRequestID from E_Link_Vacancy_request where VR_EmployeeID IN (" + empidstring + ")";
                cmd = new SqlCommand(qry, conn);
                dr = cmd.ExecuteReader();
                List<int> vacancy_list = new List<int>();
                while (dr.Read())
                {
                    int temp = (int)dr[0];
                    vacancy_list.Add(temp);
                }
                cmd.Dispose();
                dr.Close();
                string vacid_string = "";
                if (vacancy_list.Count > 0)
                {
                    vacid_string = vacancy_list[0] + "";
                    for (int i = 1; i < vacancy_list.Count; i++)
                    {
                        vacid_string = vacid_string + "," + vacancy_list[i];
                    }

                    string query = "select * from E_Data_Vacancy where Vac_IsDeleted <> 1 and Vac_ApprovalStatus = 'Pending' and Vac_VacancyID IN (" + vacid_string + ")";
                    cmd = new SqlCommand(query, conn);
                    dr = cmd.ExecuteReader();
                    List<ApproveVacancyBO> vbolist = new List<ApproveVacancyBO>();
                    while (dr.Read())
                    {
                        ApproveVacancyBO vbo = new ApproveVacancyBO();
                        vbo.Vac_VacancyID = (int)dr[0];
                        vbo.Vac_NoOfPosition = (int)dr[1];
                        vbo.Vac_skill = dr[2].ToString();
                        vbo.Vac_Experience = (int)dr[3];
                        vbo.Vac_location = dr[4].ToString();
                        vbo.Vac_BusinessDomain = dr[5].ToString();
                        vbo.Vac_RequiredByDate = dr[6].ToString(); ;
                        vbo.Vac_Status = dr[7].ToString();
                        vbolist.Add(vbo);
                    }
                    conn.Close();
                    return vbolist;
                }
                else
                    conn.Close();
                    return null;
            }
            conn.Close();
            return null;





        }

        public List<ApproveVacancyBO> returnRecord(int id)
        {
            List<ApproveVacancyBO> vbolist = new List<ApproveVacancyBO>();
            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("select * from E_Data_Vacancy where Vac_VacancyID=" + id, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ApproveVacancyBO vbo = new ApproveVacancyBO();
                vbo.Vac_VacancyID = (int)dr[0];
                vbo.Vac_NoOfPosition = (int)dr[1];
                vbo.Vac_skill = dr[2].ToString();
                vbo.Vac_Experience = (int)dr[3];
                vbo.Vac_location = dr[4].ToString();
                vbo.Vac_BusinessDomain = dr[5].ToString();
                vbo.Vac_RequiredByDate = dr[6].ToString(); ;
                vbo.Vac_Status = dr[7].ToString();
                vbolist.Add(vbo);
            }
            conn.Close();
            return vbolist;
        }

        public void appr(int id)
        {

            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("update E_Data_Vacancy set Vac_ApprovalStatus='Approved' where Vac_VacancyID='" + id + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void editBAL(VacancyRequestBO b1, int vacancyID)
        {
            d.editDAL(b1, vacancyID);
        }

    }
}
