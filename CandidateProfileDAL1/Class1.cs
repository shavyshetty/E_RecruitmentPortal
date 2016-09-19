using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateProfileBO1;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace CandidateProfileDAL1
{
    public class CandidateProfileDALClass
    {
        CandidateProfileBOClass can_bo = new CandidateProfileBOClass();
        string connection_string = "Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'";
        //string connection_string = "Data Source=ADMINSTRATOR-PC\\SQLEXPRESS2;Initial Catalog=H64;Integrated Security=True";



        //Populate Vacancies

        public List<int> populateVacancies(string Role, int ID, int Role_Factor)
        {
            List<int> vac_list = new List<int>();
            string disp_vac = "";

            switch (Role)
            {
                case "PC":
                    {
                        switch (Role_Factor)
                        {
                            case 1:
                                {
                                    disp_vac = "(select Vac_VacancyID from E_Data_Vacancy where  Vac_IsDeleted=0 and (datediff(day,getdate(),Vac_RequiredByDate)>21) and Vac_RecruitmentRequestID in(select Rec_RecruitmentRequestID from E_Data_Recruitment where Rec_PlacementConsultantID=" + ID + ")) ";
                                    break;
                                }

                            case 2:
                                {
                                    disp_vac = "(select Vac_VacancyID from E_Data_Vacancy where  Vac_IsDeleted=0 and Vac_RecruitmentRequestID in(select Rec_RecruitmentRequestID from E_Data_Recruitment where Rec_PlacementConsultantID=" + ID + ")) ";
                                    break;
                                }

                            case 3:
                                {
                                    disp_vac = "(select distinct(Can_VacancyID) from E_Data_Candidate_Profile where (Can_TestID is null or Can_TestID in( select Test_ID from E_Data_Test where datediff(day,getDate(),Test_Written)>5)) and Can_VacancyID in(select Vac_VacancyID from E_Data_Vacancy where  Vac_IsDeleted=0 and Vac_RecruitmentRequestID in(select Rec_RecruitmentRequestID from E_Data_Recruitment where Rec_PlacementConsultantID=" + ID + ")))";
                                    break;
                                }

                        }
                        break;
                    }
                case "HR": { disp_vac = "select Vac_VacancyID from E_Data_Vacancy where Vac_IsDeleted=0"; break; }//display hr

            }

            try
            {

                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                SqlCommand sq_com = new SqlCommand(disp_vac, sql_con);
                sq_com.CommandType = CommandType.Text;
                SqlDataReader rdr = sq_com.ExecuteReader();
                while (rdr.Read())
                {
                    vac_list.Add(Convert.ToInt32(rdr["Vac_VacancyID"]));
                }
                sql_con.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }

            return vac_list;
        }



        //Display Candidate List for Vacancy

        public List<CandidateProfileBOClass> displayCandidateProfile(string value, int Role)
        {

            List<CandidateProfileBOClass> can_list = new List<CandidateProfileBOClass>();
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string disp_query = "";
                switch (Role)
                {
                    //vacancy id
                    case 1:
                        {
                            disp_query = "select * from E_Data_Candidate_Profile where Can_VacancyID = @Can_VacancyID and Can_isDeleted=0";
                            SqlCommand sqcom = new SqlCommand(disp_query, sql_con);
                            sqcom.Parameters.Add("@Can_VacancyID", SqlDbType.Int).Value = Convert.ToInt32(value);
                            sqcom.CommandType = CommandType.Text;
                            SqlDataReader rdr = sqcom.ExecuteReader();
                            while (rdr.Read())
                            {
                                can_bo = new CandidateProfileBOClass();
                                can_bo.Can_VacancyID = Convert.ToInt32(rdr["Can_VacancyID"]);
                                can_bo.Can_CandidateID = Convert.ToInt32(rdr["Can_CandidateID"]);
                                can_bo.Can_CandidateName = Convert.ToString(rdr["Can_CandidateName"]);
                                string DOB_string = Convert.ToString(rdr["Can_DOB"]);
                                convertDate(ref DOB_string);
                                can_bo.Can_DOB = DOB_string;
                                can_bo.Can_Location = Convert.ToString(rdr["Can_Location"]);
                                can_bo.Can_Gender = Convert.ToString(rdr["Can_Gender"]);
                                can_bo.Can_Percentage10 = float.Parse(Convert.ToString((rdr["Can_Percentage10"])));
                                can_bo.Can_Percentage12 = float.Parse((Convert.ToString(rdr["Can_Percentage12"])));
                                can_bo.Can_YearsOfGapEducation = Convert.ToInt32(rdr["Can_YearsOfGapEducation"]);
                                can_bo.Can_YearsOfGapExperience = float.Parse(Convert.ToString(rdr["Can_YearsOfGapExperience"]));
                                can_bo.Can_ReasonsForGapEducation = Convert.ToString(rdr["Can_ReasonForGapEducation"]);
                                can_bo.Can_ReasonsForGapExperience = Convert.ToString(rdr["Can_ReasonForGapExperience"]);
                                can_bo.Can_Experience = Convert.ToInt32(rdr["Can_Experience"]);
                                can_bo.Can_ResumeFileName = Convert.ToString(rdr["Can_ResumeFile"]);
                                can_bo.Can_TestID = rdr["Can_TestID"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_TestID"]);

                                can_list.Add(can_bo);
                            }

                            break;
                        }
                    //name
                    case 2:
                        {
                            disp_query = "select * from E_Data_Candidate_Profile where Can_CandidateName like '%" + value + "%'";
                            SqlCommand sqcom = new SqlCommand(disp_query, sql_con);

                            sqcom.CommandType = CommandType.Text;
                            SqlDataReader rdr = sqcom.ExecuteReader();
                            while (rdr.Read())
                            {
                                can_bo = new CandidateProfileBOClass();
                                can_bo.Can_VacancyID = Convert.ToInt32(rdr["Can_VacancyID"]);
                                can_bo.Can_CandidateID = Convert.ToInt32(rdr["Can_CandidateID"]);
                                can_bo.Can_CandidateName = Convert.ToString(rdr["Can_CandidateName"]);
                                string DOB_string = Convert.ToString(rdr["Can_DOB"]);
                                convertDate(ref DOB_string);
                                can_bo.Can_DOB = DOB_string;
                                can_bo.Can_Location = Convert.ToString(rdr["Can_Location"]);
                                can_bo.Can_Gender = Convert.ToString(rdr["Can_Gender"]);
                                can_bo.Can_Percentage10 = float.Parse(Convert.ToString((rdr["Can_Percentage10"])));
                                can_bo.Can_Percentage12 = float.Parse((Convert.ToString(rdr["Can_Percentage12"])));
                                can_bo.Can_YearsOfGapEducation = Convert.ToInt32(rdr["Can_YearsOfGapEducation"]);
                                can_bo.Can_YearsOfGapExperience = float.Parse(Convert.ToString(rdr["Can_YearsOfGapExperience"]));
                                can_bo.Can_ReasonsForGapEducation = Convert.ToString(rdr["Can_ReasonForGapEducation"]);
                                can_bo.Can_ReasonsForGapExperience = Convert.ToString(rdr["Can_ReasonForGapExperience"]);
                                can_bo.Can_Experience = Convert.ToInt32(rdr["Can_Experience"]);
                                can_bo.Can_ResumeFileName = Convert.ToString(rdr["Can_ResumeFile"]);
                                can_bo.Can_TestID = rdr["Can_TestID"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_TestID"]);

                                can_list.Add(can_bo);
                            }
                            break;
                        }
                }



                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return can_list;
        }


        //Select Records via Edit

        public CandidateProfileBOClass editCandidateProfileRecord(int Can_CandidateID)
        {
            CandidateProfileBOClass can_bo = new CandidateProfileBOClass();
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string can_data = "select * from E_Data_Candidate_Profile where Can_CandidateID = @Can_CandidateID";
                SqlCommand sq_com = new SqlCommand(can_data, sql_con);
                sq_com.Parameters.Add("@Can_CandidateID", SqlDbType.Int).Value = Can_CandidateID;
                sq_com.CommandType = CommandType.Text;
                SqlDataReader rdr = sq_com.ExecuteReader();
                while (rdr.Read())
                {

                    can_bo.Can_VacancyID = Convert.ToInt32(rdr["Can_VacancyID"]);
                    can_bo.Can_CandidateID = Convert.ToInt32(rdr["Can_CandidateID"]);
                    can_bo.Can_CandidateName = Convert.ToString(rdr["Can_CandidateName"]);
                    string DOB_string = Convert.ToString(rdr["Can_DOB"]);
                    convertDate(ref DOB_string);
                    can_bo.Can_DOB = DOB_string;
                    can_bo.Can_Location = Convert.ToString(rdr["Can_Location"]);
                    can_bo.Can_Gender = Convert.ToString(rdr["Can_Gender"]);
                    can_bo.Can_Percentage10 = float.Parse(Convert.ToString((rdr["Can_Percentage10"])));
                    can_bo.Can_Percentage12 = float.Parse((Convert.ToString(rdr["Can_Percentage12"])));
                    can_bo.Can_YearsOfGapEducation = Convert.ToInt32(rdr["Can_YearsOfGapEducation"]);
                    can_bo.Can_YearsOfGapExperience = float.Parse((Convert.ToString(rdr["Can_YearsOfGapExperience"])));
                    can_bo.Can_ReasonsForGapEducation = Convert.ToString(rdr["Can_ReasonForGapEducation"]);
                    can_bo.Can_ReasonsForGapExperience = Convert.ToString(rdr["Can_ReasonForGapExperience"]);
                    can_bo.Can_Experience = float.Parse((Convert.ToString(rdr["Can_Experience"])));
                    can_bo.Can_TestID = Convert.ToInt32(rdr["Can_TestID"]);

                }
                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return can_bo;


        }



        //store edited details

        public void storeEditValues(CandidateProfileBOClass can_bo)
        {
            this.can_bo = can_bo;
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string update_can = "update E_Data_Candidate_Profile set Can_CandidateName=@Can_CandidateName  ,Can_Location=@Can_Location,Can_Percentage10=@Can_Percentage10, Can_Percentage12=@Can_Percentage12,Can_YearsOfGapEducation=@Can_YearsOfGapEducation,Can_ReasonForGapEducation=@Can_ReasonForGapEducation, Can_YearsOfGapExperience=@Can_YearsOfGapExperience,Can_ReasonForGapExperience=@Can_ReasonForGapExperience,Can_Experience=@Can_Experience where Can_CandidateID=@Can_CandidateID";
                SqlCommand sq_com = new SqlCommand(update_can, sql_con);
                sq_com.Parameters.Add("@Can_CandidateName", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateName;
                sq_com.Parameters.Add("@Can_CandidateID", SqlDbType.Int).Value = can_bo.Can_CandidateID;
                sq_com.Parameters.Add("@Can_Location", SqlDbType.VarChar, 50).Value = can_bo.Can_Location;
                sq_com.Parameters.Add("@Can_Gender", SqlDbType.VarChar, 1).Value = can_bo.Can_Gender;
                sq_com.Parameters.Add("@Can_Percentage10", SqlDbType.Decimal).Value = can_bo.Can_Percentage10;
                sq_com.Parameters.Add("@Can_Percentage12", SqlDbType.Decimal).Value = can_bo.Can_Percentage12;
                sq_com.Parameters.Add("@Can_Experience", SqlDbType.Decimal).Value = can_bo.Can_Experience;
                sq_com.Parameters.Add("@Can_YearsOfGapEducation", SqlDbType.Int).Value = can_bo.Can_YearsOfGapEducation;
                sq_com.Parameters.Add("@Can_YearsOfGapExperience", SqlDbType.Decimal).Value = can_bo.Can_YearsOfGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapExperience", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapEducation", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapEducation;
                sq_com.CommandType = CommandType.Text;
                int x = sq_com.ExecuteNonQuery();
                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return;

        }



        //delete the candidate profile

        public void deleteCandidateProifle(int Can_CandidateID)
        {
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string delete_can = "update E_Data_Candidate_Profile set Can_isDeleted=1 where Can_CandidateID=@Can_CandidateID";
                SqlCommand sq_com = new SqlCommand(delete_can, sql_con);
                sq_com.CommandType = CommandType.Text;
                sq_com.Parameters.Add("@Can_CandidateID", SqlDbType.Int).Value = Can_CandidateID;
                int x = sq_com.ExecuteNonQuery();
                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return;
        }



        //create candidate profile

        public int createCandidateProfile(CandidateProfileBOClass can_bo)
        {
            this.can_bo = can_bo;
            int cand_id = 0;
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string test_id = "select TI_TestID from  E_Data_Test_And_Interview where TI_VacancyID=@TI_VacancyID";
                SqlCommand sq_com = new SqlCommand(test_id, sql_con);
                sq_com.Parameters.Add("@TI_VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.CommandType = CommandType.Text;
                SqlDataReader rdr = sq_com.ExecuteReader();
                while (rdr.Read())
                {
                    can_bo.Can_TestID = Convert.ToInt32(rdr["TI_TestID"]);
                }
                sql_con.Close();



                sql_con.Open();
                string insert_can = "insert into E_Data_Candidate_Profile(Can_VacancyID,Can_CandidateName,Can_DOB,Can_Location,Can_Gender,Can_Percentage10, Can_Percentage12,Can_YearsOfGapEducation,Can_ReasonForGapEducation, Can_YearsOfGapExperience,Can_ReasonForGapExperience,Can_Experience,Can_ResumeFile,Can_TestID,Can_TestStatus)values(@Can_VacancyID,@Can_CandidateName,@Can_DOB,@Can_Location,@Can_Gender,@Can_Percentage10,@Can_Percentage12,@Can_YearsOfGapEducation,@Can_ReasonForGapEducation,@Can_YearsOfGapExperience,@Can_ReasonForGapExperience,@Can_Experience,@Can_ResumeFile,@Can_TestID,0)";
                sq_com = new SqlCommand(insert_can, sql_con);
                sq_com.Parameters.Add("@Can_VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.Parameters.Add("@Can_CandidateName", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateName;
                sq_com.Parameters.Add("@Can_DOB", SqlDbType.DateTime).Value = can_bo.Can_DOB;
                sq_com.Parameters.Add("@Can_Location", SqlDbType.VarChar, 50).Value = can_bo.Can_Location;
                sq_com.Parameters.Add("@Can_Gender", SqlDbType.VarChar, 50).Value = can_bo.Can_Gender;
                sq_com.Parameters.Add("@Can_Percentage10", SqlDbType.Decimal).Value = can_bo.Can_Percentage10;
                sq_com.Parameters.Add("@Can_Percentage12", SqlDbType.Decimal).Value = can_bo.Can_Percentage12;
                sq_com.Parameters.Add("@Can_Experience", SqlDbType.Decimal).Value = can_bo.Can_Experience;
                sq_com.Parameters.Add("@Can_YearsOfGapEducation", SqlDbType.Int).Value = can_bo.Can_YearsOfGapEducation;
                sq_com.Parameters.Add("@Can_YearsOfGapExperience", SqlDbType.Decimal).Value = can_bo.Can_YearsOfGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapExperience", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapEducation", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapEducation;
                sq_com.Parameters.Add("@Can_ResumeFile", SqlDbType.VarChar, 100).Value = can_bo.Can_ResumeFileName;
                sq_com.Parameters.Add("@Can_TestID", SqlDbType.VarChar, 100).Value = can_bo.Can_TestID;

                int x = sq_com.ExecuteNonQuery();

                sql_con.Close();

                sql_con.Open();
                string can_id = "select Can_CandidateID from E_Data_Candidate_Profile where Can_CandidateName=@Can_CandidateName";
                sq_com = new SqlCommand(can_id, sql_con);
                sq_com.Parameters.Add("@Can_CandidateName", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateName;
                sq_com.CommandType = CommandType.Text;
                rdr = sq_com.ExecuteReader();

                while (rdr.Read())
                {
                    cand_id = Convert.ToInt32(rdr["Can_CandidateID"]);
                }
                sql_con.Close();

                sql_con.Open();

                int cc = 0, cr = 0;//candidate count and candidate required
                string count_can = "select count(Can_CandidateID) as count_can from E_Data_Candidate_Profile where Can_VacancyID=@VacancyID";
                string count_req = " select Vac_NoOfPositions from E_Data_Vacancy where Vac_VacancyID=@VacancyID";
                sq_com = new SqlCommand(count_can, sql_con);
                sq_com.Parameters.Add("VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.CommandType = CommandType.Text;
                cc = (Int32)sq_com.ExecuteScalar();


                sql_con.Close();

                sql_con.Open();
                sq_com = new SqlCommand(count_req, sql_con);
                sq_com.Parameters.Add("VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.CommandType = CommandType.Text;
                cr = (Int32)sq_com.ExecuteScalar();

                sql_con.Close();

                sql_con.Open();

                if (cc == 1.5 * cr)
                {
                    string change_stat = "update E_Data_Vacancy set Vac_Status = 'Filled' where Vac_VacancyID=@VacancyID";
                    sq_com = new SqlCommand(change_stat, sql_con);
                    sq_com.Parameters.Add("@VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                    sq_com.CommandType = CommandType.Text;
                    x = sq_com.ExecuteNonQuery();
                }

                sql_con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return cand_id;
        }


        //display candidate details for HR
        public List<CandidateProfileBOClassHR> displayCandidateProfileHR(string value, int Role)
        {
            CandidateProfileBOClassHR can_bo_hr = new CandidateProfileBOClassHR();
            List<CandidateProfileBOClassHR> can_list = new List<CandidateProfileBOClassHR>();
            string disp_query = "";
            switch (Role)
            {
                case 1:
                    {

                        disp_query = "select * from E_Data_Candidate_Profile a, E_Data_Test_And_Interview b where a.Can_VacancyID=b.TI_VacancyID and a.Can_isDeleted=0 and Can_VacancyID=" + Convert.ToInt32(value);
                        break;
                    }
                case 2:
                    {
                        disp_query = "select * from E_Data_Candidate_Profile a, E_Data_Test_And_Interview b where a.Can_VacancyID=b.TI_VacancyID and a.Can_isDeleted=0 and Can_CandidateName like '%" + value + "%'";
                        break;
                    }

            }
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                SqlCommand sqcom = new SqlCommand(disp_query, sql_con);
                sqcom.CommandType = CommandType.Text;
                SqlDataReader rdr = sqcom.ExecuteReader();
                while (rdr.Read())
                {
                    can_bo_hr = new CandidateProfileBOClassHR();
                    can_bo_hr.Can_VacancyID = Convert.ToInt32(rdr["Can_VacancyID"]);
                    can_bo_hr.Can_CandidateID = Convert.ToInt32(rdr["Can_CandidateID"]);
                    can_bo_hr.Can_CandidateName = Convert.ToString(rdr["Can_CandidateName"]);
                    string DOB_string = Convert.ToString(rdr["Can_DOB"]);
                    convertDate(ref DOB_string);
                    can_bo_hr.Can_DOB = DOB_string;
                    can_bo_hr.Can_Location = Convert.ToString(rdr["Can_Location"]);
                    can_bo_hr.Can_Gender = Convert.ToString(rdr["Can_Gender"]);
                    can_bo_hr.Can_Percentage10 = float.Parse(Convert.ToString((rdr["Can_Percentage10"])));
                    can_bo_hr.Can_Percentage12 = float.Parse((Convert.ToString(rdr["Can_Percentage12"])));
                    can_bo_hr.Can_YearsOfGapEducation = Convert.ToInt32(rdr["Can_YearsOfGapEducation"]);
                    can_bo_hr.Can_YearsOfGapExperience = float.Parse(Convert.ToString(rdr["Can_YearsOfGapExperience"]));
                    can_bo_hr.Can_ReasonsForGapEducation = Convert.ToString(rdr["Can_ReasonForGapEducation"]);
                    can_bo_hr.Can_ReasonsForGapExperience = Convert.ToString(rdr["Can_ReasonForGapExperience"]);
                    can_bo_hr.Can_Experience = Convert.ToInt32(rdr["Can_Experience"]);
                    can_bo_hr.Can_ResumeFileName = Convert.ToString(rdr["Can_ResumeFile"]);
                    can_bo_hr.Can_TestID = rdr["Can_TestID"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_TestID"]);
                    can_bo_hr.Can_BGCTestID = rdr["Can_BGCTestID"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_BGCTestID"]);
                    can_bo_hr.Can_BGCTestStatus = rdr["Can_BCGTestStatus"] is DBNull ? false : (bool)rdr["Can_BCGTestStatus"];
                    can_bo_hr.Can_MedicalTestStatus = rdr["Can_MedicalTestStatus"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_MedicalTestStatus"]);
                    can_bo_hr.Can_Offer_Letter_Status = Convert.ToString(rdr["Offer_Letter_Status"]);
                    can_bo_hr.Can_TestStatus = rdr["Can_TestStatus"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_TestStatus"]);
                    can_bo_hr.HRInterviewDate = Convert.ToString(rdr["TI_HRInterviewDate"]);
                    can_bo_hr.TechnicalInterviewDate = Convert.ToString(rdr["TI_TechnicalInterviewDate"]);
                    can_bo_hr.WrittenTestDate = Convert.ToString(rdr["TI_WrittenTestDate"]);
                    can_list.Add(can_bo_hr);
                }

                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return can_list;
        }




        //convert to correct yyyy/mm/dd format

        public void convertDate(ref string date)
        {
            string[] dy_date = date.Split(' ');
            string[] date_part = dy_date[0].Split('/');
            date = date_part[2] + "/" + date_part[0] + "/" + date_part[1];
            return;

        }




    }

    //class1.cs file
    public class CandidateProfileDAL1Class
    {
        CandidateProfileBo1Class can_bo = new CandidateProfileBo1Class();

        //Populate Vacancies
        public IList<int> populateVacancies(int role)
        {
            IList<int> vac_list = new List<int>();
            string connection_string = "Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'";
            //string connection_string = "Data Source=ADMINSTRATOR-PC\\SQLEXPRESS2;Initial Catalog=H64;Integrated Security=True";
            SqlConnection sql_con = new SqlConnection(connection_string);
            sql_con.Open();
            string vac_id = "";
            switch (role)
            {
                case 0: { vac_id = "select Vac_VacancyID from E_Data_Vacancy where Vac_IsDeleted=0 and Vac_Status='open' and (datediff(day,getdate(),Vac_RequiredByDate)>21)"; break; } // create
                case 1: { vac_id = "select Vac_VacancyID from E_Data_Vacancy where Vac_IsDeleted=0 "; break; } // display pc
                case 2: { vac_id = "select Vac_VacancyID from E_Data_Vacancy where Vac_IsDeleted=0 And Vac_Status='filled'"; break; }//display hr
                case 3: { vac_id = "select Vac_VacancyID from E_Data_Vacancy where Vac_VacancyID in( select distinct(Can_VacancyID) from E_Data_Candidate_Profile where (Can_TestID is null or Can_TestID in( select Test_ID from E_Data_Test where datediff(day,getDate(),Test_Written)>5))) and Vac_IsDeleted=0"; break; }//edit

            }

            try
            {
                SqlCommand sq_com = new SqlCommand(vac_id, sql_con);
                sq_com.CommandType = CommandType.Text;
                SqlDataReader rdr = sq_com.ExecuteReader();
                while (rdr.Read())
                {
                    vac_list.Add(Convert.ToInt32(rdr["Vac_VacancyID"]));
                }
                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return vac_list;
        }


        //Create Profile
        public int createCandidateProfile1(CandidateProfileBo1Class can_bo)
        {
            this.can_bo = can_bo;
            int cand_id = 0;
            string connection_string = "Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'";
            //string connection_string = "Data Source=ADMINSTRATOR-PC\\SQLEXPRESS2;Initial Catalog=H64;Integrated Security=True";
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string insert_can = "insert into E_Data_Candidate_Profile(Can_VacancyID,Can_CandidateName,Can_DOB,Can_Location,Can_Gender,Can_Percentage10, Can_Percentage12,Can_YearsOfGapEducation,Can_ReasonForGapEducation, Can_YearsOfGapExperience,Can_ReasonForGapExperience,Can_Experience,Can_ResumeFile)values(@Can_VacancyID,@Can_CandidateName,@Can_DOB,@Can_Location,@Can_Gender,@Can_Percentage10,@Can_Percentage12,@Can_YearsOfGapEducation,@Can_ReasonForGapEducation,@Can_YearsOfGapExperience,@Can_ReasonForGapExperience,@Can_Experience,@Can_ResumeFile)";
                SqlCommand sq_com = new SqlCommand(insert_can, sql_con);
                sq_com.Parameters.Add("@Can_VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.Parameters.Add("@Can_CandidateName", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateName;
                sq_com.Parameters.Add("@Can_DOB", SqlDbType.DateTime).Value = can_bo.Can_DOB;
                sq_com.Parameters.Add("@Can_Location", SqlDbType.VarChar, 50).Value = can_bo.Can_Location;
                sq_com.Parameters.Add("@Can_Gender", SqlDbType.VarChar, 50).Value = can_bo.Can_Gender;
                sq_com.Parameters.Add("@Can_Percentage10", SqlDbType.Decimal).Value = can_bo.Can_Percentage10;
                sq_com.Parameters.Add("@Can_Percentage12", SqlDbType.Decimal).Value = can_bo.Can_Percentage12;
                sq_com.Parameters.Add("@Can_Experience", SqlDbType.Decimal).Value = can_bo.Can_Experience;
                sq_com.Parameters.Add("@Can_YearsOfGapEducation", SqlDbType.Int).Value = can_bo.Can_YearsOfGapEducation;
                sq_com.Parameters.Add("@Can_YearsOfGapExperience", SqlDbType.Int).Value = can_bo.Can_YearsOfGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapExperience", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapEducation", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapEducation;
                sq_com.Parameters.Add("@Can_ResumeFile", SqlDbType.VarChar, 100).Value = can_bo.Can_ResumeFile;
                int x = sq_com.ExecuteNonQuery();
                string can_id = "select Can_CandidateID from E_Data_Candidate_Profile where Can_CandidateName=@Can_CandidateName";
                sq_com = new SqlCommand(can_id, sql_con);
                sq_com.Parameters.Add("@Can_CandidateName", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateName;
                sq_com.CommandType = CommandType.Text;
                SqlDataReader rdr = sq_com.ExecuteReader();

                while (rdr.Read())
                {
                    cand_id = Convert.ToInt32(rdr["Can_CandidateID"]);
                }
                sql_con.Close();

                sql_con.Open();

                int cc = 0, cr = 1;
                string count_can = "select count(Can_CandidateID) as count_can from E_Data_Candidate_Profile where Can_VacancyID=@VacancyID";
                string count_req = " select Vac_NoOfPositions from E_Data_Vacancy where Vac_VacancyID=@VacancyID";
                sq_com = new SqlCommand(count_can, sql_con);
                sq_com.Parameters.Add("VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.CommandType = CommandType.Text;
                cc = (Int32)sq_com.ExecuteScalar();


                sql_con.Close();

                sql_con.Open();
                sq_com = new SqlCommand(count_req, sql_con);
                sq_com.Parameters.Add("VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                sq_com.CommandType = CommandType.Text;
                cr = (Int32)sq_com.ExecuteScalar();

                sql_con.Close();

                sql_con.Open();

                if (cc == cr)
                {
                    string change_stat = "update E_Data_Vacancy set Vac_Status = 'Filled' where Vac_VacancyID=@VacancyID";
                    sq_com = new SqlCommand(change_stat, sql_con);
                    sq_com.Parameters.Add("@VacancyID", SqlDbType.Int).Value = can_bo.Can_VacancyID;
                    sq_com.CommandType = CommandType.Text;
                    x = sq_com.ExecuteNonQuery();
                }

                sql_con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return cand_id;
        }


        // Display
        public List<CandidateProfileBo1Class> displayCandidateProfilePC(int vacancyid, int role)
        {
            string connection_string = "Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'";
            //string connection_string = "Data Source=ADMINSTRATOR-PC\\SQLEXPRESS2;Initial Catalog=H64;Integrated Security=True";
            List<CandidateProfileBo1Class> canlist = new List<CandidateProfileBo1Class>();
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();

                string candata = "select * from E_Data_Candidate_Profile where Can_VacancyID = @Can_VacancyID";
                string hrcandate = "select * from E_Data_Candidate_Profile a, E_Data_Test b where a.Can_VacancyID=b.Test_VacancyID";
                if (role == 1)
                {
                    SqlCommand sqcom = new SqlCommand(candata, sql_con);
                    sqcom.Parameters.Add("@Can_VacancyID", SqlDbType.Int).Value = vacancyid;
                    sqcom.CommandType = CommandType.Text;
                    SqlDataReader rdr = sqcom.ExecuteReader();
                    while (rdr.Read())
                    {
                        can_bo = new CandidateProfileBo1Class();
                        can_bo.Can_VacancyID = Convert.ToInt32(rdr["Can_VacancyID"]);
                        can_bo.Can_CandidateID = Convert.ToInt32(rdr["Can_CandidateID"]);
                        can_bo.Can_CandidateName = Convert.ToString(rdr["Can_CandidateName"]);
                        can_bo.Can_DOB = Convert.ToDateTime(rdr["Can_DOB"]);
                        can_bo.Can_Location = Convert.ToString(rdr["Can_Location"]);
                        can_bo.Can_Gender = Convert.ToString(rdr["Can_Gender"]);
                        can_bo.Can_Percentage10 = float.Parse(Convert.ToString((rdr["Can_Percentage10"])));
                        can_bo.Can_Percentage12 = float.Parse((Convert.ToString(rdr["Can_Percentage12"])));
                        can_bo.Can_YearsOfGapEducation = Convert.ToInt32(rdr["Can_YearsOfGapEducation"]);
                        can_bo.Can_YearsOfGapExperience = float.Parse((Convert.ToString(rdr["Can_YearsOfGapExperience"])));
                        can_bo.Can_ReasonsForGapEducation = Convert.ToString(rdr["Can_ReasonForGapEducation"]);
                        can_bo.Can_ReasonsForGapExperience = Convert.ToString(rdr["Can_ReasonForGapExperience"]);
                        can_bo.Can_Experience = Convert.ToInt32(rdr["Can_Experience"]);
                        canlist.Add(can_bo);
                    }
                }


                if (role == 2)
                {
                    SqlCommand sqcom = new SqlCommand(hrcandate, sql_con);
                    sqcom.Parameters.Add("@Can_VacancyID", SqlDbType.Int).Value = vacancyid;
                    sqcom.CommandType = CommandType.Text;
                    SqlDataReader rdr = sqcom.ExecuteReader();
                    while (rdr.Read())
                    {
                        can_bo = new CandidateProfileBo1Class();
                        can_bo.Can_VacancyID = Convert.ToInt32(rdr["a.Can_VacancyID"]);
                        can_bo.Can_CandidateID = Convert.ToInt32(rdr["a.Can_CandidateID"]);
                        can_bo.Can_CandidateName = Convert.ToString(rdr["a,Can_CandidateName"]);
                        can_bo.Can_DOB = Convert.ToDateTime(rdr["a.Can_DOB"]);
                        can_bo.Can_Location = Convert.ToString(rdr["a.Can_Location"]);
                        can_bo.Can_Gender = Convert.ToString(rdr["a.Can_Gender"]);
                        can_bo.Can_Percentage10 = float.Parse(Convert.ToString((rdr["a.Can_Percentage10"])));
                        can_bo.Can_Percentage12 = float.Parse((Convert.ToString(rdr["a.Can_Percentage12"])));
                        can_bo.Can_YearsOfGapEducation = Convert.ToInt32(rdr["a.Can_YearsOfGapEducation"]);
                        can_bo.Can_YearsOfGapExperience = float.Parse((Convert.ToString(rdr["a.Can_YearsOfGapExperience"])));
                        can_bo.Can_ReasonsForGapEducation = Convert.ToString(rdr["a.Can_ReasonForGapEducation"]);
                        can_bo.Can_ReasonsForGapExperience = Convert.ToString(rdr["a.Can_ReasonForGapExperience"]);
                        can_bo.Can_Experience = Convert.ToInt32(rdr["a.Can_Experience"]);
                        //can_bo.Can_BGCTestID = rdr["Can_BGCTestID"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_BGCTestID"]);
                        can_bo.Can_BGCTestStatus = rdr["a.Can_BCGTestStatus"] is DBNull ? false : (bool)rdr["a.Can_BCGTestStatus"];
                        can_bo.Can_MedicalTestStatus = rdr["a.Can_MedicalTestStatus"] is DBNull ? 0 : Convert.ToInt32(rdr["a.Can_MedicalTestStatus"]);
                        can_bo.Can_Offer_Letter_Status = rdr["a.Can_Offer_Letter_Status"] is DBNull ? false : (bool)rdr["a.Can_Offer_Letter_Status"];
                        can_bo.Can_TestID = rdr["a.Can_TestID"] is DBNull ? 0 : Convert.ToInt32(rdr["Can_TestStatus"]);
                        can_bo.Can_TestStatus = rdr["a.Can_TestStatus"] is DBNull ? 0 : Convert.ToInt32(rdr["a.Can_TestStatus"]);
                        can_bo.HRInterviewDate = Convert.ToString(rdr["b.Test_HR"]);
                        can_bo.TechnicalInterviewDate = Convert.ToString(rdr["b.Test_Technical"]);
                        can_bo.WrittenTestDate = Convert.ToString(rdr["b.Test_Written"]);


                        canlist.Add(can_bo);
                    }
                    sql_con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return canlist;
        }


        //Select Records via Edit
        public CandidateProfileBo1Class editCandidateProfiledisplay(int candidateid)
        {
            CandidateProfileBo1Class can_bo = new CandidateProfileBo1Class();
            string connection_string = "Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'";
            //string connection_string = "Data Source=ADMINSTRATOR-PC\\SQLEXPRESS2;Initial Catalog=H64;Integrated Security=True";
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string candata = "select * from E_Data_Candidate_Profile where Can_CandidateID = @Can_CandidateID";
                SqlCommand sqcom = new SqlCommand(candata, sql_con);
                sqcom.Parameters.Add("@Can_CandidateID", SqlDbType.Int).Value = candidateid;
                sqcom.CommandType = CommandType.Text;
                SqlDataReader rdr = sqcom.ExecuteReader();
                while (rdr.Read())
                {

                    can_bo.Can_VacancyID = Convert.ToInt32(rdr["Can_VacancyID"]);
                    can_bo.Can_CandidateID = Convert.ToInt32(rdr["Can_CandidateID"]);
                    can_bo.Can_CandidateName = Convert.ToString(rdr["Can_CandidateName"]);
                    can_bo.Can_DOB = Convert.ToDateTime(rdr["Can_DOB"]);
                    can_bo.Can_Location = Convert.ToString(rdr["Can_Location"]);
                    can_bo.Can_Gender = Convert.ToString(rdr["Can_Gender"]);
                    can_bo.Can_Percentage10 = float.Parse(Convert.ToString((rdr["Can_Percentage10"])));
                    can_bo.Can_Percentage12 = float.Parse((Convert.ToString(rdr["Can_Percentage12"])));
                    can_bo.Can_YearsOfGapEducation = Convert.ToInt32(rdr["Can_YearsOfGapEducation"]);
                    can_bo.Can_YearsOfGapExperience = float.Parse((Convert.ToString(rdr["Can_YearsOfGapExperience"])));
                    can_bo.Can_ReasonsForGapEducation = Convert.ToString(rdr["Can_ReasonForGapEducation"]);
                    can_bo.Can_ReasonsForGapExperience = Convert.ToString(rdr["Can_ReasonForGapExperience"]);
                    can_bo.Can_Experience = Convert.ToInt32(rdr["Can_Experience"]);

                }
                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return can_bo;
        }




        //Update Records via Edit
        public void editCandidateProfileupdate(CandidateProfileBo1Class can_bo)
        {
            this.can_bo = can_bo;
            string connection_string = "Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'";
            try
            {
                SqlConnection sql_con = new SqlConnection(connection_string);
                sql_con.Open();
                string update_can = "update E_Data_Candidate_Profile set Can_CandidateName=@Can_CandidateName  ,Can_Location=@Can_Location,Can_Percentage10=@Can_Percentage10, Can_Percentage12=@Can_Percentage12,Can_YearsOfGapEducation=@Can_YearsOfGapEducation,Can_ReasonForGapEducation=@Can_ReasonForGapEducation, Can_YearsOfGapExperience=@Can_YearsOfGapExperience,Can_ReasonForGapExperience=@Can_ReasonForGapExperience,Can_Experience=@Can_Experience where Can_CandidateID=@Can_CandidateID";
                SqlCommand sq_com = new SqlCommand(update_can, sql_con);

                sq_com.Parameters.Add("@Can_CandidateName", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateName;

                sq_com.Parameters.Add("@Can_Location", SqlDbType.VarChar, 50).Value = can_bo.Can_Location;
                sq_com.Parameters.Add("@Can_CandidateID", SqlDbType.VarChar, 50).Value = can_bo.Can_CandidateID;
                sq_com.Parameters.Add("@Can_Percentage10", SqlDbType.Decimal).Value = can_bo.Can_Percentage10;
                sq_com.Parameters.Add("@Can_Percentage12", SqlDbType.Decimal).Value = can_bo.Can_Percentage12;
                sq_com.Parameters.Add("@Can_Experience", SqlDbType.Decimal).Value = can_bo.Can_Experience;
                sq_com.Parameters.Add("@Can_YearsOfGapEducation", SqlDbType.Int).Value = can_bo.Can_YearsOfGapEducation;
                sq_com.Parameters.Add("@Can_YearsOfGapExperience", SqlDbType.Int).Value = can_bo.Can_YearsOfGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapExperience", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapExperience;
                sq_com.Parameters.Add("@Can_ReasonForGapEducation", SqlDbType.VarChar, 50).Value = can_bo.Can_ReasonsForGapEducation;

                //sq_com.Parameters.Add("@Can_ResumeFile",SqlDbType.VarChar,200).Value = can_bo.Can_ResumeFile;
                int x = sq_com.ExecuteNonQuery();
                sql_con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }


        }

    }

    //class2.cs file
    public class CreateTestAdminDAL
    {

        //Create sql connection
        SqlConnection con;

        // this method will generate test admin
        public string createTestAdminDAL(AddTestAdmin objectAddTestAdmin)
        {

            string msg = "";
            try
            {

                con = new SqlConnection("Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password='tcshyd'");
                con.Open();
                SqlCommand cmd;

                //select details of given employee_id from  E_Data_Employee table.
                cmd = new SqlCommand("select Emp_EmployeeID,Emp_IsHR from E_Data_Employee where Emp_EmployeeID=' " + objectAddTestAdmin.emp_id + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToInt32(dr[1]) == 1)
                    {
                        //if entered id is HR's id ,then it will return this message.
                        msg = "This is HR's EmployeeID";
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("select * from E_Data_Login where DL_LoginID='" + objectAddTestAdmin.emp_id + "'", con);
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            if (Convert.ToString(dr[3]) == "TA")
                            {
                                //if entered id is aleady created as a test Admin ,then it will return this message.
                                msg = "employee is already Test Admin";
                            }
                            else
                            {
                                dr.Close();

                                //it will assign DL_Role as a test admin if he is not test admin
                                cmd = new SqlCommand("update E_Data_Login set DL_Role='TA' where DL_LoginID='" + objectAddTestAdmin.emp_id + "' and DL_Role is null", con);

                                int lb = cmd.ExecuteNonQuery();
                                if (lb.Equals(0))
                                {
                                    //if emp_id is other than emp_id of unit head and employee, then it will display this message.
                                    msg = "Invalid Employee Id";
                                }
                                else
                                {
                                    //if test admin is created then it will return this message.
                                    msg = "Test Administrator is created";
                                }

                            }
                        }

                    }
                }
                else
                {
                    //if entered emp_id is not valid, then it will return this message.
                    msg = "Wrong Employee ID";
                }
            }
            // this will throw exception.
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }


            return msg;
        }

    }//end of class AddTestAdminDAL .

    public class DeleteTestAdminDAL
    {
        SqlConnection con;

        //reteun message whether test admin is deleted or not
        public string deleteTestAdminDAL(DeleteTestAdmin objDeleteTestAdmin)
        {
            string msg = "";
            try
            {
                con = new SqlConnection("Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password=tcshyd");
                con.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("select * from E_Data_Login where DL_LoginID='" + objDeleteTestAdmin.emp_id + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToString(dr[3]) == "")
                    {
                        //if entered emp_id is not not test admin but still your deleting it ,then it will return this message
                        msg = "This is not a Test Admin";
                    }
                    else
                    {
                        dr.Close();

                        // deleting test admin .
                        cmd = new SqlCommand("update E_Data_Login set DL_Role=null where DL_LoginID='" + objDeleteTestAdmin.emp_id + "' and DL_Role='TA'", con);

                        int n = cmd.ExecuteNonQuery();

                        if (n == 0 || n == (-1))
                        {
                            //if enterd emp_id is other than emp_id of unit head and employee, then it will show this message.
                            msg = "This is not a Test Admin";
                        }
                        else
                        {
                            //if entered employee is valid emp_id then 
                            msg = "Test Admin is deleted";
                        }
                    }

                }
                else
                {
                    msg = "wrong Employee_ID";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");//throw exception
            }

            return msg;//method exits from here.
        }

    }//end of class DeleteTestAdminDAL .

    public class DisplayTestAdminDAL
    {
        // this method will retun list
        public List<Employee> displayTestAdminDAL(DisplayTestAdmin objDisplayTestAdmin, ref string msg)
        {
            msg = "";

            //create list.
            List<Employee> listemployee = new List<Employee>();
            try
            {
                SqlConnection con = new SqlConnection("Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password=tcshyd");
                con.Open();
                SqlCommand cmd = new SqlCommand("select e.Emp_EmployeeID,Emp_EmployeeName,e.Emp_Designation,d.DL_Role from E_Data_Employee e join E_Data_Login d on( e.Emp_EmployeeID= d.DL_LoginID)where (DL_Role is null or DL_Role='TA') and Emp_EmployeeName like '%" + objDisplayTestAdmin.emp_name + "%'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Employee employee = new Employee();
                        employee.Employee_ID = Convert.ToInt32(dr[0]);
                        employee.Test_Administrator = Convert.ToString(dr[1]);
                        employee.designation = Convert.ToString(dr[2]);
                        employee.Employee_Role = Convert.ToString(dr[3]);

                        //adding element into list.
                        listemployee.Add(employee);
                    }

                }
                else
                {
                    msg = "Data Not Found";
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }

            return listemployee;//method exits from here.
        }
    }


    //executequery.cs file
    public class ExecuteQuery
    {
        SqlConnection conn;
        public ExecuteQuery()
        {

            //ConfigurationManager.
            conn = new SqlConnection("Data Source=172.25.192.72;Initial Catalog=DB01H64;User ID=PJ01H64;Password=tcshyd");
            //conn = new SqlConnection("Data Source=ADMINSTRATOR-PC\\SQLEXPRESS2;Initial Catalog=H64;Integrated Security=True");
            conn.Open();
        }
        public SqlDataReader select(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public int delete(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            int n = cmd.ExecuteNonQuery();
            return n;
        }
        public int update(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            int n = cmd.ExecuteNonQuery();
            return n;
        }
        public int insert(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            int n = cmd.ExecuteNonQuery();
            return n;
        }
    }

    //loginDAL.cs file
    public class loginDAL
    {
        public user checkLoginDAL(user objlogin)
        {
            try
            {
                string query;
                if (Convert.ToInt32(objlogin.username) < 5000)
                    query = "select DL_Password, DL_Role,Emp_EmployeeName from E_Data_Login inner join E_Data_Employee on Emp_EmployeeID=DL_LoginID where DL_LoginID=" + objlogin.username;
                else
                    query = "select DL_Password, DL_Role,PC_PlacementConsultantName from E_Data_Login inner join E_Data_Placement_Consultant on PC_PlacementConsultantID=DL_LoginID where DL_LoginID=" + objlogin.username;
                //string query = "select * from Data_Login_dumy_g7 where DL_LoginID=" + objlogin.username;
                ExecuteQuery objExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    string password = dr["DL_Password"] is DBNull ? "" : Convert.ToString(dr["DL_Password"]);
                    if (objlogin.password.Equals(password))
                    {
                        objlogin.role = dr["DL_Role"] is DBNull ? "" : Convert.ToString(dr["DL_Role"]);
                        objlogin.password = "";
                        objlogin.name = dr[2] is DBNull ? "" : Convert.ToString(dr[2]);
                        objlogin.name = objlogin.name + " (" + objlogin.username + ")";
                        return objlogin;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            return null;
        }

        public bool checkPasswordDAL(user objlogin)
        {
            try
            {
                string query = "select * from E_Data_Login where DL_LoginID=" + objlogin.username;
                //string query = "select * from Data_Login_dumy_g7 where DL_LoginID=" + objlogin.username;
                ExecuteQuery objExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    string password = dr["DL_Password"] is DBNull ? "" : Convert.ToString(dr["DL_Password"]);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }


        }

        public bool changePasswordDAL(user objlogin)
        {
            try
            {
                string query = "update E_Data_Login set DL_Password='" + objlogin.password + "' where DL_LoginID=" + objlogin.username;
                //string query = "update Data_Login_dumy_g7 set DL_Password='" + objlogin.password + "' where DL_LoginID=" + objlogin.username;
                ExecuteQuery objExecuteQuery = new ExecuteQuery();
                int n = objExecuteQuery.insert(query);
                if (n == 0 || n == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            return false;
        }

        public bool checkUserDAL(user objlogin)
        {
            try
            {
                string query = "select Emp_DOB from E_Data_Employee where Emp_EmployeeID=" + objlogin.username;
                ExecuteQuery objExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    string dob = dr["Emp_DOB"] is DBNull ? "" : Convert.ToString(dr["Emp_DOB"]);
                    return checkDOB(objlogin.dob, dob);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }

        }

        public bool checkDOB(string userdob, string dbdob)
        {

            try
            {
                string[] dividedatetime = userdob.Split(' ');
                string[] datepart = dividedatetime[0].Split('/');
                int dd = Convert.ToInt32(datepart[0]);
                int mm = Convert.ToInt32(datepart[1]);
                int yy = Convert.ToInt32(datepart[2]);

                dividedatetime = dbdob.Split(' ');
                datepart = dividedatetime[0].Split('/');
                int dd1 = Convert.ToInt32(datepart[1]);
                int mm1 = Convert.ToInt32(datepart[0]);
                int yy1 = Convert.ToInt32(datepart[2]);
                if (yy == yy1)
                    if (mm == mm1)
                        if (dd == dd1)
                            return true;
                return false;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }

        }
    }

    //testandInterviewDAL.cs
    public class TestandInterviewDAL
    {
        public List<int> getTestAdministratorIDListDAL()
        {

            List<int> listTestAdmin = new List<int>();
            try
            {
               
                string query = "  select DL_LoginID from E_Data_Login where DL_Role='TA'";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);

                if (dr.HasRows)
                {
                    while (dr.Read())
                        listTestAdmin.Add(dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return listTestAdmin;
        }
        public List<int> getVacancyIDListDAL()
        {

            List<int> listTestAdmin = new List<int>();
            try
            {
                string query = "select Vac_VacancyID from E_Data_Vacancy where Vac_VacancyID not in (select TI_VacancyID from E_Data_Test_And_Interview)";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);

                if (dr.HasRows)
                {
                    while (dr.Read())
                        listTestAdmin.Add(dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return listTestAdmin;
        }
        public List<int> getCreatedScheduleVacancyIDListDAL()
        {

            List<int> listTestAdmin = new List<int>();
            try
            {
                string query = "select Vac_VacancyID from E_Data_Vacancy where Vac_VacancyID in (select TI_VacancyID from E_Data_Test_And_Interview) and Vac_RequiredByDate > GETDATE()";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);

                if (dr.HasRows)
                {
                    while (dr.Read())
                        listTestAdmin.Add(dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return listTestAdmin;
        }
        public List<int> displayVacancyIDListDAL()
        {

            List<int> listTestAdmin = new List<int>();
            try
            {
                string query = "select Vac_VacancyID from E_Data_Vacancy";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);

                if (dr.HasRows)
                {
                    while (dr.Read())
                        listTestAdmin.Add(dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return listTestAdmin;
        }
        public string getRequiredByDateDAL(int VacancyID)
        {
            string RequiredByDate = "";
            try
            {
                string query = "select Vac_RequiredByDate from E_Data_Vacancy where Vac_VacancyID=" + VacancyID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    RequiredByDate = dr[0] is DBNull ? "" : Convert.ToString(dr[0]);
                    convertDate(ref RequiredByDate);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return RequiredByDate;
        }
        public int getRecruitmentRequestID(int VacancyID)
        {
            int RecruitmentRequestID = 0;
            try
            {
                string query = "select Vac_RecruitmentRequestID from E_Data_Vacancy where Vac_VacancyID =" + VacancyID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    RecruitmentRequestID = dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return RecruitmentRequestID;
        }
        public int createTestandInterviewDAL(ScheduleDetails testObject)
        {
            int n = 0;
            try
            {
                string query = "insert into E_Data_Test_And_Interview(TI_TestAdministratorID,TI_RecruitmentRequestID,TI_VacancyID,TI_WrittenTestDate,TI_TechnicalInterviewDate,TI_HRInterviewDate) values(" + testObject.TestAdministratorID + "," + testObject.RecruitmentRequestID + "," + testObject.VacancyID + ",'" + testObject.WrittenTestDate + "','" + testObject.TechnicalInterviewDate + "','" + testObject.HRInterviewDate + "')";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                n = objectExecuteQuery.insert(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }

            return n;

        }
        public ScheduleDetails getcreatedTestandInterviewDAL(ScheduleDetails testObject)
        {
            try
            {
                string s = "";
                string query = "select TOP(1) * from E_Data_Test_And_Interview where TI_TestAdministratorID =" + testObject.TestAdministratorID + " AND TI_RecruitmentRequestID=" + testObject.RecruitmentRequestID + " AND TI_VacancyID=" + testObject.VacancyID + " order by TI_TestID desc";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    testObject.TestID = dr["TI_TestID"] is DBNull ? 0 : Convert.ToInt32(dr["TI_TestID"]);
                    testObject.RecruitmentRequestID = dr["TI_RecruitmentRequestID"] is DBNull ? 0 : Convert.ToInt32(dr["TI_RecruitmentRequestID"]);

                    s = dr["TI_WrittenTestDate"] is DBNull ? "" : Convert.ToString(dr["TI_WrittenTestDate"]);
                    convertDate(ref s);
                    testObject.WrittenTestDate = s;

                    s = dr["TI_TechnicalInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_TechnicalInterviewDate"]);
                    convertDate(ref s);
                    testObject.TechnicalInterviewDate = s;

                    s = dr["TI_HRInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_HRInterviewDate"]);
                    convertDate(ref s);
                    testObject.HRInterviewDate = s;
                }
            }
            catch (Exception e)
            {
                testObject = null;
                MessageBox.Show(e.Message, "Error Occured");
            }
            return testObject;
        }
        public DisplayCandidateDetails displayTestsAndInterviewScheduledDAL(int VacancyID)
        {
            DisplayCandidateDetails objectCandidate_Profile = new DisplayCandidateDetails();
            try
            {
                string query = "select * from E_Data_Candidate_Profile where Can_VacancyID =" + VacancyID + "  and Can_IsDeleted=0";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objectCandidate_Profile.can_id.Add(dr["Can_CandidateID"] is DBNull ? 0 : Convert.ToInt32(dr["Can_CandidateID"]));
                        objectCandidate_Profile.can_status.Add(dr["Can_TestStatus"] is DBNull ? 0 : Convert.ToInt32(dr["Can_TestStatus"]));
                    }
                }
                query = "select * from E_Data_Test_And_Interview where TI_VacancyID =" + VacancyID;
                objectExecuteQuery = new ExecuteQuery();
                dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objectCandidate_Profile.WrittenTestDate = (dr["TI_WrittenTestDate"] is DBNull ? "" : Convert.ToString(dr["TI_WrittenTestDate"]));
                        convertDate(ref objectCandidate_Profile.WrittenTestDate);
                        objectCandidate_Profile.canEditWrittenTestDate = canUpdate(objectCandidate_Profile.WrittenTestDate);

                        objectCandidate_Profile.TechnicalInterviewDate = (dr["TI_TechnicalInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_TechnicalInterviewDate"]));
                        convertDate(ref objectCandidate_Profile.TechnicalInterviewDate);
                        objectCandidate_Profile.canTechnicalInterviewDate = canUpdate(objectCandidate_Profile.TechnicalInterviewDate);

                        objectCandidate_Profile.HRInterviewDate = (dr["TI_HRInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_HRInterviewDate"]));
                        convertDate(ref objectCandidate_Profile.HRInterviewDate);
                        objectCandidate_Profile.canEditHRInterviewDate = canUpdate(objectCandidate_Profile.HRInterviewDate);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return objectCandidate_Profile;
        }
        public DisplayCandidateDetails displayMedicalStatusDAL(int VacancyID)
        {
            DisplayCandidateDetails objectCandidate_Profile = new DisplayCandidateDetails();
            try
            {
                string query = "select * from E_Data_Candidate_Profile where Can_VacancyID =" + VacancyID + "  and Can_IsDeleted=1";
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objectCandidate_Profile.can_id.Add(dr["Can_CandidateID"] is DBNull ? 0 : Convert.ToInt32(dr["Can_CandidateID"]));
                        objectCandidate_Profile.can_status.Add(dr["Can_MedicalTestStatus"] is DBNull ? 0 : Convert.ToInt32(dr["Can_MedicalTestStatus"]));
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return objectCandidate_Profile;
        }
        public ScheduleDetails getTestsAndInterviewScheduledDAL(int VacancyID)
        {
            ScheduleDetails testObject = new ScheduleDetails();
            try
            {
                string s;
                string query = "select * from E_Data_Test_And_Interview where TI_VacancyID=" + VacancyID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    testObject.TestID = dr["TI_TestID"] is DBNull ? 0 : Convert.ToInt32(dr["TI_TestID"]);

                    s = dr["TI_WrittenTestDate"] is DBNull ? "" : Convert.ToString(dr["TI_WrittenTestDate"]);
                    convertDate(ref s);
                    testObject.WrittenTestDate = s;
                    testObject.canEditWrittenTestDate = canEdit(testObject.WrittenTestDate);

                    s = dr["TI_TechnicalInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_TechnicalInterviewDate"]);
                    convertDate(ref s);
                    testObject.TechnicalInterviewDate = s;
                    testObject.canEditTechnicalInterviewDate = canEdit(testObject.TechnicalInterviewDate);

                    s = dr["TI_HRInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_HRInterviewDate"]);
                    convertDate(ref s);
                    testObject.HRInterviewDate = s;
                    testObject.canEditHRInterviewDate = canEdit(testObject.HRInterviewDate);

                    s = getRequiredByDateDAL(VacancyID);
                    convertDate(ref s);
                    testObject.RequiredByDate = s;
                    testObject.VacancyID = VacancyID;

                    testObject.TestAdministratorID = getTestAdministratorIDDAL(VacancyID);

                }
            }
            catch (Exception e)
            {
                testObject = null;
                MessageBox.Show(e.Message, "Error Occured");
            }
            return testObject;
        }
        public int getTestAdministratorIDDAL(int VacancyID)
        {
            int TestAdministratorID = 0;
            try
            {
                string query = "select TI_TestAdministratorID from E_Data_Test_And_Interview where TI_VacancyID =" + VacancyID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    TestAdministratorID = dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return TestAdministratorID;
        }
        public ScheduleDetails editTestsAndInterviewsScheduledDAL(ScheduleDetails testObject)
        {
            int n = 0;
            try
            {
                string query = "update E_Data_Test_And_Interview set TI_WrittenTestDate='" + testObject.WrittenTestDate + "',TI_TechnicalInterviewDate='" + testObject.TechnicalInterviewDate + "',TI_HRInterviewDate='" + testObject.HRInterviewDate + "' where TI_TestID=" + testObject.TestID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                n = objectExecuteQuery.update(query);
                if (n == 0 || n == -1)
                {
                    MessageBox.Show("Updation not done");
                }
                else
                {
                    return getUpdatedTestandInterviewDAL(testObject);
                }

            }
            catch (Exception e)
            {
                n = 0;
                MessageBox.Show(e.Message, "Error Occured");
            }

            return null;

        }
        public int getTestID(int VacancyID)
        {
            int TestID = 0;
            try
            {
                string query = "select TI_TestID from E_Data_Test_And_Interview where TI_VacancyID =" + VacancyID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                        TestID = dr[0] is DBNull ? 0 : Convert.ToInt32(dr[0]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return TestID;

        }
        public ScheduleDetails getUpdatedTestandInterviewDAL(ScheduleDetails testObject)
        {
            try
            {
                string s;
                string query = "select * from E_Data_Test_And_Interview where TI_TestAdministratorID =" + testObject.TestAdministratorID + " AND TI_RecruitmentRequestID=" + testObject.RecruitmentRequestID + " AND TI_VacancyID=" + testObject.VacancyID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                {
                    dr.Read();
                    testObject.TestID = dr["TI_TestID"] is DBNull ? 0 : Convert.ToInt32(dr["TI_TestID"]);
                    testObject.RecruitmentRequestID = dr["TI_RecruitmentRequestID"] is DBNull ? 0 : Convert.ToInt32(dr["TI_RecruitmentRequestID"]);

                    s = dr["TI_WrittenTestDate"] is DBNull ? "" : Convert.ToString(dr["TI_WrittenTestDate"]);
                    convertDate(ref s);
                    testObject.WrittenTestDate = s;

                    s = dr["TI_TechnicalInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_TechnicalInterviewDate"]);
                    convertDate(ref s);
                    testObject.TechnicalInterviewDate = s;

                    s = dr["TI_HRInterviewDate"] is DBNull ? "" : Convert.ToString(dr["TI_HRInterviewDate"]);
                    convertDate(ref s);
                    testObject.HRInterviewDate = s;
                }
            }
            catch (Exception e)
            {
                testObject = null;
                MessageBox.Show(e.Message, "Error Occured");
            }
            return testObject;
        }
        public string editTestStatusOfCandidatesDAL(Test_Status objTest_Status)
        {
            int n = 0;
            string msg = "";
            string query;
            try
            {
                if (objTest_Status.TestStatus == -1)
                {
                    query = "update E_Data_Candidate_Profile set Can_TestStatus=" + objTest_Status.TestStatus + " where Can_CandidateID=" + objTest_Status.CandidateID;
                }
                else
                {
                    query = "update E_Data_Candidate_Profile set Can_TestStatus=(Can_TestStatus+" + objTest_Status.TestStatus + ") where Can_CandidateID=" + objTest_Status.CandidateID;
                }

                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                n = objectExecuteQuery.update(query);
                if (n < 1)
                    msg = "Updation is not done";
                else
                    msg = "Updation Done";
            }
            catch (Exception e)
            {
                e.ToString();
                n = 0;
            }
            return msg;
        }
        public string updateMedicalStatusDAL(UpdateMedicalStatus objUpdateMedicalStatus)
        {
            int n = 0;
            string msg = "";

            try
            {
                string query = "update E_Data_Candidate_Profile set Can_MedicalTestStatus=" + objUpdateMedicalStatus.Status + " where Can_CandidateID=" + objUpdateMedicalStatus.CandidateID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                n = objectExecuteQuery.update(query);
                if (n < 1)
                    msg = "Updation is not done";
                else
                    msg = "Updation Done";
            }
            catch (Exception e)
            {
                e.ToString();
                n = 0;
            }
            return msg;
        }
        /*
        
        
        
        
        
        
        
        public bool checkCandidateIDDAL(int CandidateID)
        {
            string query;

            try
            {
                query = "select * from Candidate_Profile_table_dumy_g7 where Can_CandidateID=" + CandidateID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                SqlDataReader dr = objectExecuteQuery.select(query);
                if (dr.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            { }
            return false;
        }
        public string editTestStatusOfCandidatesDAL(Test_Status objTest_Status)
        {
            int n = 0;
            string msg = "";

            try
            {
                string query = "update Candidate_Profile_table_dumy_g7 set Can_TestStatus=" + objTest_Status.TestStatus + " where Can_CandidateID=" + objTest_Status.CandidateID;
                ExecuteQuery objectExecuteQuery = new ExecuteQuery();
                n = objectExecuteQuery.update(query);
                if (n < 1)
                    msg = "Updation is not done";
                else
                    msg = "Updation Done";
            }
            catch (Exception e)
            {
                n = 0;
            }
            return msg;
        }
        public bool checkWithCurrentDate(ref string date)
        {
            string[] dividedatetime = date.Split(' ');
            string[] datepart = dividedatetime[0].Split('/');
            int dd = Convert.ToInt32(datepart[0]);
            int mm = Convert.ToInt32(datepart[1]);
            int yy = Convert.ToInt32(datepart[2]);

            string[] dividedatetime1 = (Convert.ToString(DateTime.Now)).Split(' ');
            string[] datepart1 = dividedatetime1[0].Split('/');
            int dd1 = Convert.ToInt32(datepart1[1]);
            int mm1 = Convert.ToInt32(datepart1[0]);
            int yy1 = Convert.ToInt32(datepart1[2]);
            if (dd == dd1)
                if (mm == mm1)
                    if (yy == yy1)
                    {
                        date = Convert.ToString(dd) + "/" + Convert.ToString(mm) + "/" + Convert.ToString(yy);
                        return true;
                    }
                    else
                        return false;
                else
                    return false;
            else return false;

        }
    */
        public void convertDate(ref string date)
        {
            string[] dividedatetime = date.Split(' ');
            string[] datepart = dividedatetime[0].Split('/');
            int dd = Convert.ToInt32(datepart[1]);
            int mm = Convert.ToInt32(datepart[0]);
            int yy = Convert.ToInt32(datepart[2]);
            date = Convert.ToString(dd) + "/" + Convert.ToString(mm) + "/" + Convert.ToString(yy);
        }
        public bool canUpdate(string date)
        {
            string[] dividedatetime = date.Split(' ');
            string[] datepart = dividedatetime[0].Split('/');
            int dd = Convert.ToInt32(datepart[0]);
            int mm = Convert.ToInt32(datepart[1]);
            int yy = Convert.ToInt32(datepart[2]);

            dividedatetime = Convert.ToString(DateTime.Now).Split(' ');
            datepart = dividedatetime[0].Split('/');
            int dd1 = Convert.ToInt32(datepart[1]);
            int mm1 = Convert.ToInt32(datepart[0]);
            int yy1 = Convert.ToInt32(datepart[2]);
            if (yy == yy1)
                if (mm == mm1)
                    if (dd == dd1)
                        return true;
            return false;
        }

        public bool canEdit(string date)
        {
            string[] dividedatetime = date.Split(' ');
            string[] datepart = dividedatetime[0].Split('/');
            int dd = Convert.ToInt32(datepart[0]);
            int mm = Convert.ToInt32(datepart[1]);
            int yy = Convert.ToInt32(datepart[2]);
            DateTime d = new DateTime(yy, mm, dd);

            int n = Convert.ToInt32(DateTime.Compare(d, DateTime.Now));
            if (n > 0)
                return true;
            else
                return false;
        }

    }





}
