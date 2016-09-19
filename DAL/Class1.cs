using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using BO;
using System.Configuration;
namespace DAL
{

    //add BGC class DAL for submodule-1 crud 1 & 3
    public class AddBgcAdminDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        //displays list for bgc admins
        public List<BOClass> Add_Employee_BGC_Admin_DAL()
        {
            List<BOClass> bolist = new List<BOClass>();
            try
            {


                con.Open();
                SqlCommand cmd1 = new SqlCommand("select Emp_EmployeeID, Emp_EmployeeName from E_Data_Employee where Emp_EmployeeID in(select Emp_EmployeeID from E_Data_Employee where Emp_IsHR !=1 and Emp_UnitHeadID is not null except select BgcAdmin_EmployeeID from E_Data_BGC_Administrator)", con);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (dr.Read())
                {
                    BOClass b = new BOClass();
                    if (dr.IsDBNull(0))
                    {

                        b.BgcAdmin_EmployeeID = 0;
                    }
                    else
                    {
                        b.BgcAdmin_EmployeeID = Convert.ToInt32(dr.GetInt32(0));
                    }

                    b.Emp_EmployeeName = (dr.GetString(1));
                    bolist.Add(b);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return bolist;

        }

        //displays list of added employees as bgc admin
        public List<BOClass> Add_BGC_Admin_DAL(string empid)
        {

            List<BOClass> bolist2 = new List<BOClass>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_AddBGCAdmin_Group1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("select * from E_Data_BGC_Administrator  ", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    BOClass b = new BOClass();
                    if (dr.IsDBNull(3))
                    {

                        b.BgcAdmin_EmployeeID = 0;
                    }
                    else
                    {
                        b.BgcAdmin_EmployeeID = Convert.ToInt32(dr.GetInt32(3));
                    }

                    b.BgcAdmin_Status = (dr.GetString(1));
                    b.BgcAdmin_StatusChangeTime = (dr.GetDateTime(2));

                    b.BgcAdmin_AdministratorID = (dr.GetInt32(0));
                    bolist2.Add(b);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bolist2;

        }

        //displays final bgc admins
        public List<BOClass> View_BGC_Admin_DAL()
        {
            List<BOClass> bolist2 = new List<BOClass>();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from E_Data_BGC_Administrator   ", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    BOClass b = new BOClass();
                    if (dr.IsDBNull(3))
                    {

                        b.BgcAdmin_EmployeeID = 0;
                    }
                    else
                    {
                        b.BgcAdmin_EmployeeID = Convert.ToInt32(dr.GetInt32(3));
                    }
                    b.BgcAdmin_Status = (dr.GetString(1));
                    b.BgcAdmin_StatusChangeTime = (dr.GetDateTime(2));

                    b.BgcAdmin_AdministratorID = (dr.GetInt32(0));
                    bolist2.Add(b);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bolist2;
        }
    }


    //edit BGC status DAL for submodule-1 crud 2
    public class EditBGCStatus_DALClass
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        //view BGC Admins list passed by HR-DAL
        public List<EditBGCStatus_BOClass> getDAL()
        {
            List<EditBGCStatus_BOClass> boList = new List<EditBGCStatus_BOClass>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from E_Data_BGC_Administrator  ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EditBGCStatus_BOClass b = new EditBGCStatus_BOClass();
                    b.BgcAdmin_AdministratorID = (dr.GetInt32(0));
                    b.BgcAdmin_Status = dr.GetString(1);
                    b.BgcAdmin_StatusChangeTime = dr.GetDateTime(2);
                    boList.Add(b);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return boList;
        }

        //edit BGC admin status-admin details list -DAL
        public List<EditBGCStatus_BOClass> getEditDAL(int e_id)
        {
            List<EditBGCStatus_BOClass> boList = new List<EditBGCStatus_BOClass>();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select BgcAdmin_AdministratorID,BgcAdmin_StatusChangeTime from E_Data_BGC_Administrator   where BgcAdmin_AdministratorID=" + e_id, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EditBGCStatus_BOClass b = new EditBGCStatus_BOClass();
                    b.BgcAdmin_AdministratorID = (dr.GetInt32(0));
                    //b.BgcAdmin_Status = dr.GetString(1);
                    b.BgcAdmin_StatusChangeTime = dr.GetDateTime(1);
                    boList.Add(b);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return boList;
        }

        //edit BGC admin status-dropdown DAL
        public void updateBGCStatusDAL(int e_id, string value)
        {
            int flag = 0;
            //DateTime current = new DateTime();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_UpdateBGC_Group1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = e_id;
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = value;
            cmd.ExecuteNonQuery();
            int emp_id = 0;
            SqlCommand cmd4 = new SqlCommand("select BgcAdmin_EmployeeID from E_Data_BGC_Administrator where BgcAdmin_AdministratorID='" + e_id + "'", con);
            var dr5 = cmd4.ExecuteReader();
            while (dr5.Read())
            {
                emp_id = dr5.GetInt32(0);
            }
            String status = "BGCAdmin";
            dr5.Close();
            if (value == "Approved")
            {
                SqlCommand cmd0 = new SqlCommand("select DL_LoginID from E_Data_Login where DL_LoginID= '" + emp_id + "'", con);
                flag = cmd0.ExecuteNonQuery();
                //if (flag == 1)
                //{
                SqlCommand cmd1 = new SqlCommand("update E_Data_Login set DL_Role='" + status + "'where DL_LoginID=" + emp_id, con);
                cmd1.ExecuteNonQuery();
                //}

            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("select DL_LoginID from E_Data_Login where DL_LoginID=" + emp_id, con);

                var dr1 = cmd2.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Close();
                    if (value == "Awaiting Approval" || value == "On Leave" || value == "Rejected")
                    {
                        SqlCommand cmd3 = new SqlCommand("LoginBGCUpdates", con);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@e_id", SqlDbType.Int).Value = emp_id;
                        cmd3.ExecuteNonQuery();
                    }

                }
            }


            con.Close();

        }

    }

    //class for BGC Updates- submodule 2 crud 1,2,3
    public class BgcScheduleDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        public int dalcrud1(bgcschedule bgc)
        {

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("updatecandidatebgc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", bgc.frmdate);
                cmd.Parameters.AddWithValue("@todate", bgc.todate);
                cmd.Parameters.AddWithValue("@adminid", bgc.adminid);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -2;

            }

        }

        public List<int> dalgetdl_list()
        {
            List<int> bgc = new List<int>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select BgcAdmin_employeeID from E_Data_BGC_Administrator    where BgcAdmin_Status = 'Approved' and BgcAdmin_employeeID is not null ", con);
                SqlDataReader dr = cmd.ExecuteReader();

                //add elements to list

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        bgc.Add(dr.GetInt32(0));
                    }
                }
                else bgc.Add(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bgc;
        }

        public List<display> dalcrud3(string dl, string status)
        {
            int i = 5;
            List<display> li = new List<display>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {
                connection.Open();
                if (dl.Equals("Test Status"))
                {
                    if (status.Equals("Pending"))
                    {
                        i = 5;
                    }
                    else if (status.Equals("Approved"))
                    {
                        i = 6;
                    }
                    else if (status.Equals("Rejected"))
                    {
                        i = 7;
                    }
                    SqlCommand cmd1 = new SqlCommand("select can_candidateID, can_vacancyID, Can_TestStatus from E_Data_Candidate_Profile    where Can_VacancyID is not null and Can_TestStatus= " + i, connection);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        display ds = new display();
                        ds.candidateid = dr.GetInt32(0);
                        ds.vacancyid = dr.GetInt32(1);
                        int j = dr.GetInt32(2);
                        if (j == 5)
                        {
                            ds.status = "BGC Test Pending";
                        }
                        else if (j == 6)
                        {
                            ds.status = "BGC Test Cleared";
                        }
                        else if (j == 7)
                        {
                            ds.status = "BGC Test Failed";
                        }
                        else ds.status = "NA";
                        li.Add(ds);
                    }
                }
                else if (dl.Equals("BGC Status"))
                {
                    SqlCommand cmd1;
                    if (status.Equals("Pending"))
                    {
                        i = 0;
                    }
                    else if (status.Equals("Approved"))
                    {
                        i = 1;
                    }
                    else if (status.Equals("Rejected"))
                    {
                        i = 2;
                    }
                    if (i == 2)
                    {
                        cmd1 = new SqlCommand("select can_candidateID,can_vacancyID, Can_BCGTestStatus, Can_TestStatus  from E_Data_Candidate_Profile    where Can_TestStatus>4 and can_vacancyID is not null and Can_BCGTestStatus=0 and Can_Remarks is not null", connection);
                    }
                    else
                    {
                        cmd1 = new SqlCommand("select can_candidateID,can_vacancyID, Can_BCGTestStatus, Can_TestStatus  from E_Data_Candidate_Profile    where Can_TestStatus>4 and can_vacancyID is not null and Can_BCGTestStatus=" + i, connection);
                    }
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        int j;
                        display ds = new display();
                        ds.candidateid = dr.GetInt32(0);
                        ds.vacancyid = dr.GetInt32(1);
                        bool j1 = dr.GetBoolean(2);
                        if (j1 == true)
                        {
                            j = 1;
                        }
                        else j = 0;
                        int k = dr.GetInt32(3);
                        if (k == 6)
                        {
                            ds.status = "BGC Test Cleared";
                        }
                        else if (k == 5 && j == 0)
                        {
                            ds.status = "BGC Test Pending";

                        }
                        else if (k == 7 && j == 0)
                        {
                            ds.status = "BGC Test Failed";
                        }
                        else ds.status = "Status Unknown";

                        li.Add(ds);
                    }
                }
                else if (dl.Equals("Vacancy ID"))
                {
                    SqlCommand cmd1 = new SqlCommand("select can_candidateID, can_vacancyID from E_Data_Candidate_Profile    where Can_VacancyID is  not null  order by (Can_VacancyID)", connection);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        display ds = new display();
                        ds.candidateid = dr.GetInt32(0);
                        ds.vacancyid = dr.GetInt32(1);
                        ds.status = "NA";
                        li.Add(ds);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return li;
        }

        public int dalcrud2_setStatus(setbgcdetails cd, int teststatus, string remarks)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

            con.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_Candidate_Profile    set Can_BCGTestStatus=" + cd.bgcteststatus + ", Can_TestStatus=" + teststatus + ", Can_Remarks='" + remarks + "' where Can_CandidateID=" + cd.candidateid, con);
            int i = cmd.ExecuteNonQuery();


            return i;
        }

        public List<candidate_display> dalcrud2_getbgclist(int bgcadminid)
        {
            List<candidate_display> li = new List<candidate_display>();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Can_CandidateID,Can_CandidateName,Can_VacancyID,Can_TestID,Can_BGCTestID,Can_BCGTestStatus from E_Data_Candidate_Profile    where  Can_BGCTestID in (select BgcCheck_BGCTestID from E_Data_Candidate_BGC_Check where BgcCheck_AdministratorID in (select BgcAdmin_AdministratorID from E_Data_BGC_Administrator where BgcAdmin_EmployeeID=" + bgcadminid + "))", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    candidate_display cd = new candidate_display();
                    cd.candidateid = dr.GetInt32(0);
                    if (dr.IsDBNull(1))
                    {
                        cd.candidatename = "No value found";
                    }
                    else cd.candidatename = dr.GetString(1);

                    if (dr.IsDBNull(2))
                    {
                        cd.vacancyid = -1;
                    }
                    else cd.vacancyid = dr.GetInt32(2);

                    if (dr.IsDBNull(3))
                    {
                        cd.testid = -1;
                    }
                    else cd.testid = dr.GetInt32(3);

                    if (dr.IsDBNull(4))
                    {
                        cd.bgctestid = -1;
                    }
                    else cd.bgctestid = dr.GetInt32(4);

                    cd.bgcteststatus = (bool)dr[5];

                    li.Add(cd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return li;
        }

        public candidate_display dalcrud2_getcanlist(int candidateid)
        {
            candidate_display cd = new candidate_display();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Can_CandidateID,Can_CandidateName,Can_VacancyID,Can_TestID,Can_BGCTestID,Can_BCGTestStatus from E_Data_Candidate_Profile    where Can_CandidateID=" + candidateid, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cd.candidateid = dr.GetInt32(0);
                    if (dr.IsDBNull(1))
                    {
                        cd.candidatename = "No value found";
                    }
                    else cd.candidatename = dr.GetString(1);

                    if (dr.IsDBNull(2))
                    {
                        cd.vacancyid = -1;
                    }
                    else cd.vacancyid = dr.GetInt32(2);

                    if (dr.IsDBNull(3))
                    {
                        cd.testid = -1;
                    }
                    else cd.testid = dr.GetInt32(3);

                    if (dr.IsDBNull(4))
                    {
                        cd.bgctestid = -1;
                    }
                    else cd.bgctestid = dr.GetInt32(4);
                    cd.bgcteststatus = (bool)dr[5];

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return cd;
        }
    }

    //class for employee details - submodule 3 crud 1,2,3
    public class EmployeeDetailsDAL
    {
        //public BOList getDAL()
        //{
        //    BOList bl = new BOList();
        //    bl.age = 12;
        //    bl.name = 13;
        //    bl.gender = 1;
        //    return bl;
        //}
        // string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        SqlConnection conOpen = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        public List<BOCandidateOfferStatus> getBOCandidateOfferStatusDAL()
        {
            List<BOCandidateOfferStatus> candidatestatus = new List<BOCandidateOfferStatus>();
            try
            {
                conOpen.Open();

                SqlCommand cmd = new SqlCommand("select Can_CandidateID,Can_TestStatus,Can_BCGTestStatus from E_Data_Candidate_Profile where Can_BCGTestStatus=1 and Can_TestStatus=6  and Offer_Letter_Status is null", conOpen);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BOCandidateOfferStatus bo = new BOCandidateOfferStatus();
                    bo.candidateid = dr.GetInt32(0);

                    bo.interviewstatus = Convert.ToString(dr.GetInt32(1));
                    if (bo.interviewstatus.Equals("6"))
                    {
                        bo.interviewstatus = "Approved";
                    }

                    bo.bgcstatus = Convert.ToString(dr.GetBoolean(2));
                    if (bo.bgcstatus.Equals("1"))
                    {
                        bo.bgcstatus = "Cleared";
                    }
                    candidatestatus.Add(bo);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return candidatestatus;
        }
        public CheckLogin LoginDAL(string username, string password)
        {
            CheckLogin cl = new CheckLogin();
            try
            {
                conOpen.Open();
                SqlCommand cmd = new SqlCommand("select DL_LoginID,DL_Password, DL_Designation,DL_Role from E_Data_Login   where DL_LoginID='" + username + "' ", conOpen);
                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr.IsDBNull(0))
                        {

                            cl.username = null;

                        }
                        else
                        {
                            cl.username = Convert.ToString(dr.GetInt32(0));


                        }
                        if (dr.IsDBNull(1))
                        {

                            cl.password = null;

                        }
                        else
                        {
                            cl.password = dr.GetString(1);


                        }

                        if (dr.IsDBNull(2))
                        {

                            cl.designation = null;

                        }
                        else
                        {
                            cl.designation = dr.GetString(2);


                        }
                        if (dr.IsDBNull(3))
                        {
                            cl.role = null;
                        }
                        else
                        {
                            cl.role = dr.GetString(3);
                        }


                    }
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return cl;

        }
        public bool checkOfferDAL(string chkCandidateStatus)
        {

            conOpen.Open();
            SqlCommand cmd = new SqlCommand("update E_Data_Candidate_Profile set Offer_Letter_Status='Awaiting Approal' where Can_BCGTestStatus=1 and Can_TestStatus=6 and Can_CandidateID='" + chkCandidateStatus + "'", conOpen);
            int i = cmd.ExecuteNonQuery();
            if (i == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string respondOfferDAL(string username, string password)
        {

            SetJobStatus ss = new SetJobStatus();
            try
            {
                conOpen.Open();

                SqlCommand cmd1 = new SqlCommand("select DL_Designation,DL_Role from E_Data_Login   where DL_LoginID='" + username + "' and DL_Password='" + password + "' ", conOpen);
                var dr = cmd1.ExecuteReader();
                CheckLogin cl = new CheckLogin();
                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                    {

                        cl.designation = null;

                    }
                    else
                    {
                        cl.designation = dr.GetString(0);


                    }
                    if (dr.IsDBNull(1))
                    {
                        cl.role = null;
                    }
                    else
                    {
                        cl.role = dr.GetString(1);
                    }


                }
                if (dr.HasRows)
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("select Offer_Letter_Status from  E_Data_Candidate_Profile    where Can_CandidateID='" + username + "' ", conOpen);
                    var dr1 = cmd.ExecuteReader();

                    while (dr1.Read())
                    {

                        if (dr1.IsDBNull(0))
                        {

                            ss.offerstatus = null;
                        }
                        else
                        {
                            ss.offerstatus = dr1.GetString(0);
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ss.offerstatus;

        }
        public int AcceptOfferCanDAL(string username)
        {
            conOpen.Open();
            SqlCommand cmd1 = new SqlCommand("select Offer_Letter_Status from E_Data_Candidate_Profile where Can_CandidateID='" + username + "' and Can_IsDeleted=0 and Can_BCGTestStatus=1 and Can_TestStatus=6 ", conOpen);
            var dr = cmd1.ExecuteReader();
            string job_status = "";
            while (dr.Read())
            {
                if (dr.IsDBNull(0))
                {

                    job_status = null;
                }
                else
                {
                    job_status = dr.GetString(0);
                }
            }
            dr.Close();
            if (job_status.Equals("Awaiting Approal"))
            {
                SqlCommand cmd = new SqlCommand("update E_Data_Candidate_Profile    set Offer_Letter_Status='Accept' where Can_CandidateID='" + username + "' and Can_IsDeleted=0 and Can_BCGTestStatus=1", conOpen);
                int i = cmd.ExecuteNonQuery();
                if (i == -1)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else if (job_status.Equals("Reject"))
            {
                return 4;
            }

            return 3;


        }
        public int RejectOfferCanDAL(string username)
        {
            conOpen.Open();
            SqlCommand cmd1 = new SqlCommand("select Offer_Letter_Status from E_Data_Candidate_Profile where Can_CandidateID='" + username + "' and Can_IsDeleted=0 and Can_BCGTestStatus=1", conOpen);
            var dr = cmd1.ExecuteReader();
            string job_status = "";
            while (dr.Read())
            {
                if (dr.IsDBNull(0))
                {

                    job_status = null;
                }
                else
                {
                    job_status = dr.GetString(0);
                }
            }
            if (job_status.Equals("Awaiting Approal"))
            {
                dr.Close();
                SqlCommand cmd = new SqlCommand("update E_Data_Candidate_Profile    set Offer_Letter_Status='Reject',Can_IsDeleted=1 where Can_CandidateID='" + username + "'", conOpen);
                int i = cmd.ExecuteNonQuery();
                if (i == -1)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }

            }
            else if (job_status.Equals("Reject"))
            {
                return 4;
            }



            return 3;
        }
        public List<SetJobStatus> CheckCanOfferDAL()
        {

            List<SetJobStatus> li = new List<SetJobStatus>();
            try
            {
                conOpen.Open();

                SqlCommand cmd = new SqlCommand("select Can_CandidateID,Can_TestStatus,Can_BCGTestStatus,Offer_Letter_Status from  E_Data_Candidate_Profile    where Offer_Letter_Status='Accept' and Can_IsDeleted=0 and Can_BCGTestStatus=1    ", conOpen);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SetJobStatus ss = new SetJobStatus();
                    ss.candidateid = dr.GetInt32(0);

                    ss.interviewstatus = Convert.ToString(dr.GetInt32(1));
                    if (ss.interviewstatus.Equals("6"))
                    {
                        ss.interviewstatus = "Approved";
                    }
                    ss.bgcstatus = Convert.ToString(dr.GetBoolean(2));
                    if (ss.bgcstatus.Equals("true"))
                    {
                        ss.bgcstatus = "Cleared";
                    }
                    ss.offerstatus = dr.GetString(3);
                    li.Add(ss);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            return li;
        }
        public List<int> displayDAL()
        {
            List<int> bolist = new List<int>();
            // string constring = "Data Source=172.25.192.72;Initial Catalog=DB01H45;User ID=PJ01H45;Password=tcshyd";
            //SqlConnection connection = new SqlConnection(constring);
            try
            {
                conOpen.Open();
                SqlCommand cmd1 = new SqlCommand("select Can_CandidateID from E_Data_Candidate_Profile    where  Offer_Letter_Status='Accept'", conOpen);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    int bo;
                    bo = (dr.GetInt32(0));
                    bolist.Add(bo);
                }
                dr.Close();
                conOpen.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bolist;

        }
        public List<int> displayEmpDAL()
        {
            List<int> bolist = new List<int>();
            // string constring = "Data Source=172.25.192.72;Initial Catalog=DB01H45;User ID=PJ01H45;Password=tcshyd";
            //SqlConnection connection = new SqlConnection(constring);
            try
            {
                conOpen.Open();
                SqlCommand cmd1 = new SqlCommand("select Emp_EmployeeID from E_Data_Employee ", conOpen);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    int bo;
                    bo = (dr.GetInt32(0));
                    bolist.Add(bo);
                }
                dr.Close();
                conOpen.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bolist;

        }
        public bool InsertCandidateDAL(int canID, int unit_id, int project_id, string ishr, DateTime doj, string gender, string designation, int Emp_CTC, string isNew)
        {
            int ishr_int;
            int isNew_int;
            conOpen.Open();
            if (ishr.Equals("isHR"))
            {
                ishr_int = 1;
            }
            else
            {
                ishr_int = 0;
            }
            if (isNew.Equals("isNew"))
            {
                isNew_int = 1;
            }
            else
            {
                isNew_int = 0;
            }


            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertcandidate";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = canID;
            cmd.Parameters.Add("@unitid", SqlDbType.Int).Value = unit_id;
            cmd.Parameters.Add("@projectid", SqlDbType.Int).Value = project_id;
            cmd.Parameters.Add("@ishr", SqlDbType.Bit).Value = ishr_int;
            cmd.Parameters.Add("@doj", SqlDbType.DateTime).Value = doj;
            cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender;
            cmd.Parameters.Add("@ctc", SqlDbType.Int).Value = Emp_CTC;
            cmd.Parameters.Add("@designation", SqlDbType.VarChar).Value = designation;
            cmd.Parameters.Add("@isnew", SqlDbType.Bit).Value = isNew_int;

            cmd.Connection = conOpen;


            int i = cmd.ExecuteNonQuery();
            if (i == -1)
            {
                conOpen.Close();
                return false;
            }
            else
            {
                conOpen.Close();
                return true;
            }

        }
        public List<int> displayUnitDAL()
        {

            List<int> bolist = new List<int>();
            try
            {
                conOpen.Open();
                SqlCommand cmd1 = new SqlCommand("select Emp_EmployeeID from E_Data_Employee where Emp_UnitHeadID is null and Emp_IsHR !=1", conOpen);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        int bo;
                        bo = (dr.GetInt32(0));
                        bolist.Add(bo);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bolist;


        }
        public List<int> displayProjectDAL()
        {

            List<int> bolist = new List<int>();
            try
            {
                conOpen.Open();
                SqlCommand cmd1 = new SqlCommand("select Proj_ProjectID from  E_Data_Project ", conOpen);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        int bo;
                        bo = (dr.GetInt32(0));
                        bolist.Add(bo);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bolist;


        }
        public bool editEmployee(int can_id, int unit_id, string designation, int Emp_CTC)
        {
            conOpen.Open();
            List<int> bolist = new List<int>();


            SqlCommand cmd1 = new SqlCommand("update E_Data_Employee  set Emp_Designation='" + designation + "',Emp_CTC= '" + Emp_CTC + "', Emp_UnitHeadID='" + unit_id + "' where Emp_EmployeeID='" + can_id + "'", conOpen);
            int i = cmd1.ExecuteNonQuery();
            if (i == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<DisplayEmployee> displayEmployeeList()
        {


            List<DisplayEmployee> empdisplay = new List<DisplayEmployee>();
            try
            {
                conOpen.Open();
                SqlCommand cmd = new SqlCommand("select Emp_EmployeeID,Emp_EmployeeName,Emp_UnitHeadID,Emp_ProjectID,Emp_IsHR,Emp_DOJ,Emp_gender,Emp_CTC,Emp_Designation,Emp_IsNew  from E_Data_Employee ", conOpen);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DisplayEmployee de = new DisplayEmployee();
                    de.Emp_id = Convert.ToInt32(dr.GetInt32(0));
                    de.Emp_Name = dr.GetString(1);
                    if (dr.IsDBNull(2))
                    {

                        de.Emp_UnitHeadID = 0;
                    }
                    else
                    {
                        de.Emp_UnitHeadID = Convert.ToInt32(dr.GetInt32(2));
                    }


                    de.Emp_ProjectID = Convert.ToInt32(dr.GetInt32(3));
                    de.isHR = Convert.ToBoolean(dr.GetBoolean(4));

                    de.Emp_DOJ = Convert.ToDateTime(dr.GetDateTime(5));


                    if (dr.IsDBNull(6))
                    {

                        de.gender = null;
                    }
                    else
                    {
                        de.gender = Convert.ToString(dr.GetString(6));
                    }

                    de.Emp_CTC = Convert.ToInt64(dr.GetDecimal(7));

                    if (dr.IsDBNull(8))
                    {

                        de.designation = null;
                    }
                    else
                    {
                        de.designation = dr.GetString(8);
                    }

                    if (dr.IsDBNull(9))
                    {

                        de.isNew = Convert.ToBoolean(0);
                    }
                    else
                    {
                        de.isNew = Convert.ToBoolean(dr.GetBoolean(9));
                    }



                    empdisplay.Add(de);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return empdisplay;
        }
        public bool editCandidateStatusDAL(string int_status, string bgc_status, int can_id)
        {
            conOpen.Open();
            if (int_status.Equals("Approved"))
            {
                int_status = "6";
            }
            else
            {
                int_status = "7";
            }
            if (bgc_status.Equals("Rejected"))
            {
                bgc_status = "0";
            }
            else
            {
                bgc_status = "1";
            }
            SqlCommand cmd = new SqlCommand("update E_Data_Candidate_Profile set Can_TestStatus='" + int_status + "',Can_BCGTestStatus='" + bgc_status + "' where Can_CandidateId='" + can_id + "'", conOpen);
            int i = cmd.ExecuteNonQuery();
            return true;


        }
        public List<GenerateCandidateID> generateCandidateIdDAL(string username)
        {
            conOpen.Open();
            int id = 0;
            string designation = "";
            int user = Convert.ToInt32(username);
            bool ishr = false;
            List<GenerateCandidateID> li = new List<GenerateCandidateID>();
            SqlCommand cmd = new SqlCommand("select Emp_EmployeeID,Emp_Designation,Emp_IsHR from E_Data_Employee where Can_CandidateID='" + user + "'", conOpen);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = dr.GetInt32(0);
                designation = dr.GetString(1);
                ishr = dr.GetBoolean(2);

            }
            dr.Close();
            if (ishr)
            {
                SqlCommand cmd1 = new SqlCommand("insert into E_Data_Login(DL_LoginID,DL_Designation,DL_Role) values('" + id + "','" + designation + "','" + designation + "')", conOpen);
                int i = cmd1.ExecuteNonQuery();
                if (i == 1)
                {
                    SqlCommand cmd2 = new SqlCommand("select DL_LoginID,DL_Password from E_Data_Login where DL_LoginID='" + id + "'", conOpen);
                    var dr1 = cmd2.ExecuteReader();
                    while (dr1.Read())
                    {
                        GenerateCandidateID gc = new GenerateCandidateID();
                        gc.eid = dr1.GetInt32(0);
                        gc.password = dr1.GetString(1);
                        li.Add(gc);

                    }
                    if (dr1.HasRows)
                    {
                        dr1.Close();
                        SqlCommand cmd3 = new SqlCommand("delete from E_Data_Login where DL_LoginID='" + username + "'", conOpen);
                        cmd3.ExecuteNonQuery();



                    }

                }
                else
                {
                    GenerateCandidateID gc = new GenerateCandidateID();
                    gc.eid = 0;
                    gc.password = null;
                    li.Add(gc);
                }

            }
            else
            {
                SqlCommand cmd1 = new SqlCommand("insert into E_Data_Login(DL_LoginID,DL_Password,DL_Designation) values('" + id + "',1234,'" + designation + "')", conOpen);
                int i = cmd1.ExecuteNonQuery();
                if (i == 1)
                {
                    SqlCommand cmd2 = new SqlCommand("select DL_LoginID,DL_Password from E_Data_Login where DL_LoginID='" + id + "'", conOpen);
                    var dr1 = cmd2.ExecuteReader();
                    while (dr1.Read())
                    {
                        GenerateCandidateID gc = new GenerateCandidateID();
                        gc.eid = dr1.GetInt32(0);
                        gc.password = dr1.GetString(1);
                        li.Add(gc);

                    }
                    if (dr1.HasRows)
                    {
                        dr1.Close();
                        SqlCommand cmd3 = new SqlCommand("delete from E_Data_Login where DL_LoginID='" + username + "'", conOpen);
                        cmd3.ExecuteNonQuery();



                    }

                }
                else
                {
                    GenerateCandidateID gc = new GenerateCandidateID();
                    gc.eid = 0;
                    gc.password = null;
                    li.Add(gc);
                }

            }
            return li;



        }
        public List<EmployeeProfile> getProfileDAl(string username)
        {

            List<EmployeeProfile> li = new List<EmployeeProfile>();
            try
            {
                conOpen.Open();
                SqlCommand cmd = new SqlCommand("select Emp_EmployeeID,Emp_EmployeeName,Emp_UnitHeadID,Emp_ProjectID,Emp_DOB,Emp_DOJ,Emp_gender,Emp_CTC,Emp_Designation  from E_Data_Employee where Emp_EmployeeID='" + username + "'", conOpen);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmployeeProfile de = new EmployeeProfile();
                    de.Emp_id = Convert.ToInt32(dr.GetInt32(0));
                    de.Emp_Name = dr.GetString(1);
                    if (dr.IsDBNull(2))
                    {

                        de.Emp_UnitHeadID = 0;
                    }
                    else
                    {
                        de.Emp_UnitHeadID = Convert.ToInt32(dr.GetInt32(2));
                    }


                    de.Emp_ProjectID = Convert.ToInt32(dr.GetInt32(3));

                    de.Emp_DOB = Convert.ToDateTime(dr.GetDateTime(4));
                    de.Emp_DOJ = Convert.ToDateTime(dr.GetDateTime(5));


                    if (dr.IsDBNull(6))
                    {

                        de.gender = null;
                    }
                    else
                    {
                        de.gender = Convert.ToString(dr.GetString(6));
                    }

                    de.Emp_CTC = Convert.ToInt64(dr.GetDecimal(7));

                    if (dr.IsDBNull(8))
                    {

                        de.designation = null;
                    }
                    else
                    {
                        de.designation = dr.GetString(8);
                    }




                    li.Add(de);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return li;

        }
        public string getUsername(string username, string designation)
        {
            SqlCommand cmd2 = null;
            string name = "";
            conOpen.Open();
            if (designation == null)
            {

                cmd2 = new SqlCommand("select Can_CandidateName from E_Data_Candidate_Profile where Can_CandidateName='" + username + "'", conOpen);

            }

            else if (designation.Equals("HR") || designation.Equals("UH") || designation.Equals("BGCAdmin") || designation.Equals("ASE"))
            {
                cmd2 = new SqlCommand("select Emp_EmployeeName from E_Data_Employee where Emp_EmployeeID='" + username + "'", conOpen);

            }

            var dr = cmd2.ExecuteReader();

            while (dr.Read())
            {
                name = dr.GetString(0);
            }
            dr.Close();
            return name;
        }
        public int changePassDAL(string pass, string username)
        {
            conOpen.Open();
            int id = Convert.ToInt32(username);
            SqlCommand cmd1 = new SqlCommand("update E_Data_Login set DL_Password='" + pass + "'where DL_LoginID='" + id + "'", conOpen);
            int i = cmd1.ExecuteNonQuery();
            return i;

        }
        public List<BOCandidateOfferStatus> waitingApprovalCandidate()
        {
            conOpen.Open();
            List<BOCandidateOfferStatus> candidatestatus = new List<BOCandidateOfferStatus>();
            SqlCommand cmd = new SqlCommand("select Can_CandidateID,Can_TestStatus,Can_BCGTestStatus,Offer_Letter_Status from E_Data_Candidate_Profile where Can_BCGTestStatus=1 and Can_TestStatus=6  and Offer_Letter_Status='Awaiting Approal'", conOpen);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                BOCandidateOfferStatus bo = new BOCandidateOfferStatus();
                bo.candidateid = dr.GetInt32(0);

                bo.interviewstatus = Convert.ToString(dr.GetInt32(1));
                if (bo.interviewstatus.Equals("6"))
                {
                    bo.interviewstatus = "Approved";
                }

                bo.bgcstatus = Convert.ToString(dr.GetBoolean(2));
                if (bo.bgcstatus.Equals("1"))
                {
                    bo.bgcstatus = "Cleared";
                }
                candidatestatus.Add(bo);
            }

            return candidatestatus;
        }
    }

}





