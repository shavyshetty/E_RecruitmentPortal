using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MvcApplication1.Models;
using DAL_ERS;
using BO_ERS;

namespace MvcBAL
{
   public class deleteApprovedVacancy
    {

       Emp_Operations_DAL d = new Emp_Operations_DAL();
        public List<VacancyBO> deleteApprovedVacancyDispList(int empid)
        {
            try
            {
                SqlConnection conn = d.logindal();
                SqlCommand cmd = new SqlCommand("select VR_VacancyRequestID from E_Link_Vacancy_request where VR_EmployeeID=" + empid, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<int> vacancyid = new List<int>();
                while (dr.Read())
                {
                    int temp = (int)dr[0];
                    vacancyid.Add(temp);
                }
                cmd.Dispose();
                dr.Dispose();
                string vacancyidstring = "";
                if (vacancyid.Count > 0)
                {
                    vacancyidstring = vacancyid[0] + "";
                    for (int i = 1; i < vacancyid.Count; i++)
                    {
                        vacancyidstring = vacancyidstring + "," + vacancyid[i];
                    }
                    string query = "select * from E_Data_Vacancy where Vac_IsDeleted <> 1 and Vac_ApprovalStatus = 'Approved' and Vac_Hasappliedfordeletion <> 1 and Vac_VacancyID IN (" + vacancyidstring + ")";
                    cmd = new SqlCommand(query, conn);
                    dr = cmd.ExecuteReader();
                    List<VacancyBO> vbolist = new List<VacancyBO>();
                    while (dr.Read())
                    {
                        VacancyBO vbo = new VacancyBO();
                        vbo.id = (int)dr[0];
                        vbo.noof_vacancies = (int)dr[1];
                        vbo.skills = dr[2].ToString();
                        vbo.experience = (int)dr[3];
                        vbo.location = dr[4].ToString();
                        vbo.domain = dr[5].ToString();
                        vbo.date = dr[6].ToString();
                        vbo.date = vbo.date.Substring(0, 9);
                        vbo.status = dr[7].ToString();
                        vbolist.Add(vbo);
                    }
                    return vbolist;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }


        public void deleteUpdateRecord(int id)
        {
            try
            {
               
                SqlConnection conn = d.logindal();
                SqlCommand cmd = new SqlCommand("update E_Data_Vacancy set Vac_Hasappliedfordeletion =1 where Vac_VacancyID =" + id, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }



        public List<VacancyBO> deleteRequestEmployee(int empid)
        {
            try
            {
                SqlConnection conn = d.logindal();
                SqlCommand cmd = new SqlCommand("select Emp_EmployeeID from E_Data_Employee where Emp_UnitHeadID=" + empid, conn);
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
                    List<int> vacancyid = new List<int>();
                    while (dr.Read())
                    {
                        int temp = (int)dr[0];
                        vacancyid.Add(temp);
                    }
                    cmd.Dispose();
                    dr.Close();

                string vacancyidstring = "";
                if (vacancyid.Count > 0)
                {
                    vacancyidstring = vacancyid[0] + "";
                    for (int i = 1; i < vacancyid.Count; i++)
                    {
                        vacancyidstring = vacancyidstring + "," + vacancyid[i];
                    }
                    string query = "select * from E_Data_Vacancy where Vac_IsDeleted <> 1 and Vac_ApprovalStatus = 'Approved' and Vac_Hasappliedfordeletion = 1 and Vac_VacancyID IN (" + vacancyidstring + ")";
                    cmd = new SqlCommand(query, conn);
                    dr = cmd.ExecuteReader();
                    List<VacancyBO> vbolist = new List<VacancyBO>();
                    while (dr.Read())
                    {
                        VacancyBO vbo = new VacancyBO();
                        vbo.id = (int)dr[0];
                        vbo.noof_vacancies = (int)dr[1];
                        vbo.skills = dr[2].ToString();
                        vbo.experience = (int)dr[3];
                        vbo.location = dr[4].ToString();
                        vbo.domain = dr[5].ToString();
                        vbo.date = dr[6].ToString();
                        vbo.date = vbo.date.Substring(0, 9);
                        vbo.status = dr[7].ToString();
                        vbolist.Add(vbo);
                    }
                    return vbolist;
                }
                else
                    return null;
              }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
     
        }


        public void deleteRequest(int id)
        {

            SqlConnection conn = d.logindal();
            SqlCommand cmd = new SqlCommand("update E_Data_Vacancy set Vac_IsDeleted =1 where Vac_VacancyID =" + id, conn);
            cmd.ExecuteNonQuery();
        }

   }
}

