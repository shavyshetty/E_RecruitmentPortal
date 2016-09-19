using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace BAL
{
    //add BGC class BAL for submodule-1 crud 1 & 3
    public class AddBgcAdminBAL   // 
    {
        //displays list for bgc admins
        public List<BOClass> Add_Employee_BGC_Admin_BAL()
        {
            AddBgcAdminDAL dl = new AddBgcAdminDAL();
            BOClass b = new BOClass();
            List<BOClass> bolist2 = new List<BOClass>();
            bolist2 = dl.Add_Employee_BGC_Admin_DAL();
            return bolist2;
        }

        //displays list of added employees as bgc admin
        public List<BOClass> Add_BGC_Admin_BAL(string empid)
        {
            AddBgcAdminDAL dl = new AddBgcAdminDAL();
            BOClass b = new BOClass();
            List<BOClass> bolist2 = new List<BOClass>();
            bolist2 = dl.Add_BGC_Admin_DAL(empid);
            return bolist2;
        }

        //displays final bgc admins
        public List<BOClass> View_BGC_Admin_BAL()
        {
            AddBgcAdminDAL dl = new AddBgcAdminDAL();
            BOClass b = new BOClass();
            List<BOClass> bolist2 = new List<BOClass>();
            bolist2 = dl.View_BGC_Admin_DAL();
            return bolist2;
        }
    }

    //edit BGC status BAL for submodule-1 crud 2
    public class EditBGCStatus_BALClass
    {
        //view BGC Admins list passed by HR-BAL        
        public List<EditBGCStatus_BOClass> getBAL()
        {
            EditBGCStatus_DALClass dal = new EditBGCStatus_DALClass();
            List<EditBGCStatus_BOClass> bList = new List<EditBGCStatus_BOClass>();
            bList = dal.getDAL();
            return bList;
        }

        //edit BGC admin status-admin details list -BAL
        public List<EditBGCStatus_BOClass> getEditBAL(int e_id)
        {
            EditBGCStatus_DALClass dal = new EditBGCStatus_DALClass();
            List<EditBGCStatus_BOClass> bList = new List<EditBGCStatus_BOClass>();
            bList = dal.getEditDAL(e_id);
            return bList;
        }

        //edit BGC admin status-dropdown BAL
        public void updateBGCStatusBAL(int empid, string value)
        {
            EditBGCStatus_DALClass d = new EditBGCStatus_DALClass();
            d.updateBGCStatusDAL(empid, value);
        }
    }

    //class for BGC Updates- submodule 2 crud 1,2,3
    public class BgcScheduleBAL
    {
        public int balcrud1(bgcschedule bgc)
        {
            DateTime today = DateTime.Today;
            int flag = -1;

            try
            {
                if (Convert.ToDateTime(bgc.frmdate) >= today)
                {
                    if (Convert.ToDateTime(bgc.todate) >= Convert.ToDateTime(bgc.frmdate))
                    {
                        flag = 1;
                    }
                }
            }
            catch (Exception e)
            {
                flag = -2;
            }
            if (flag == 1)
            {
                BgcScheduleDAL dc = new BgcScheduleDAL();
                int i = dc.dalcrud1(bgc);
                if (i >= 1)
                {
                    flag = i;
                }
                else if (i == 0)
                {
                    flag = 0;
                }
                else if (i == -1)
                {
                    flag = -1;
                }
                else if (i == -2)
                {
                    flag = -2;
                }

            }
            return flag;

        }

        public List<int> balgetdl_list()
        {
            BgcScheduleDAL dc = new BgcScheduleDAL();
            List<int> bgc;
            bgc = dc.dalgetdl_list();
            return bgc;

        }

        public List<display> balcrud3(string dl, string status)
        {
            BgcScheduleDAL dc = new BgcScheduleDAL();
            List<display> li;
            li = dc.dalcrud3(dl, status);
            return li;
        }

        public int balcrud2_setStatus(setbgcdetails cd, string remarks)
        {
            int teststatus = 7;
            if (cd.bgcteststatus == 0 && remarks.Equals(""))
            {
                teststatus = 5;
            }
            else if (cd.bgcteststatus == 1)
            {
                teststatus = 6;
            }
            BgcScheduleDAL dc = new BgcScheduleDAL();
            int i = dc.dalcrud2_setStatus(cd, teststatus, remarks);
            return i;
        }

        public List<candidate_display> balcrud2_getbgclist(int bgcadminid)
        {
            BgcScheduleDAL dc = new BgcScheduleDAL();
            List<candidate_display> li = dc.dalcrud2_getbgclist(bgcadminid);
            return li;
        }

        public candidate_display balcrud2_getcanlist(int candidateid)
        {
            BgcScheduleDAL dc = new BgcScheduleDAL();
            candidate_display cd = dc.dalcrud2_getcanlist(candidateid);
            return cd;
        }
    }

    //class for employee details - submodule 3 crud 1,2,3
    public class EmployeeDetailsBAL
    {
        //public BOList getBAL()
        //{

        //    EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
        //   BOList bo = dal.getDAL();
        //   return bo;

        //}
        public List<BOCandidateOfferStatus> getCandidateOfferStatusBAL()
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            List<BOCandidateOfferStatus> li = dal.getBOCandidateOfferStatusDAL();
            return li;
        }
        public CheckLogin LoginBAL(string username, string password)
        {
            string designation;
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            CheckLogin ck = dal.LoginDAL(username, password);
            return ck;
        }
        public bool checkOfferBAL(string chkCandidateStatus)
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            bool check = dl.checkOfferDAL(chkCandidateStatus);
            return check;
        }
        public string respondOfferBAL(string username, string password)
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            string status = dl.respondOfferDAL(username, password);
            return status;

        }
        public int AcceptOfferCanBAL(string username)
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            int check = dal.AcceptOfferCanDAL(username);
            return check;
        }
        public int RejectOfferCanBAL(string username)
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            int check = dal.RejectOfferCanDAL(username);
            return check;
        }
        public List<SetJobStatus> CheckCanOfferBAL()
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            List<SetJobStatus> li = dal.CheckCanOfferDAL();
            return li;
        }
        public List<int> displayBAL()
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            // BOClass b = new BOClass();
            List<int> bolist = new List<int>();
            bolist = dl.displayDAL();
            return bolist;

        }
        public List<int> displayEmpBAL()
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            // BOClass b = new BOClass();
            List<int> bolist = new List<int>();
            bolist = dl.displayEmpDAL();
            return bolist;

        }
        public bool insertCandidateBAl(int canID, int unit_id, int project_id, string ishr, DateTime doj, string gender, string designation, int Emp_CTC, string isNew)
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            bool check = dal.InsertCandidateDAL(canID, unit_id, project_id, ishr, doj, gender, designation, Emp_CTC, isNew);
            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<int> displayUnitBAL()
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();

            List<int> bolist = new List<int>();
            bolist = dl.displayUnitDAL();
            return bolist;

        }
        public List<int> displayProjectBAL()
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();

            List<int> bolist = new List<int>();
            bolist = dl.displayProjectDAL();
            return bolist;

        }
        public bool editEmployeeBAL(int can_id, int unit_id, string designation, int Emp_CTC)
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            bool check = dl.editEmployee(can_id, unit_id, designation, Emp_CTC);
            if (check)
            {
                return true;
            }
            else
            {
                return false;


            }
        }
        public List<DisplayEmployee> displayEmplistBal()
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            List<DisplayEmployee> li = dl.displayEmployeeList();
            return li;
        }
        public bool editCandidateStatusBAL(string int_status, string bgc_status, int can_id)
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            bool check = dal.editCandidateStatusDAL(int_status, bgc_status, can_id);
            return check;
        }

        public List<GenerateCandidateID> generateCandidateIdBAL(string username)
        {
            EmployeeDetailsDAL dl = new EmployeeDetailsDAL();
            List<GenerateCandidateID> li = dl.generateCandidateIdDAL(username);
            return li;

        }
        public List<EmployeeProfile> getProfileBAL(string username)
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            List<EmployeeProfile> li = dal.getProfileDAl(username);
            return li;
        }
        public string getUsernameBAL(string username, string designation)
        {
            EmployeeDetailsDAL bal = new EmployeeDetailsDAL();
            string name = bal.getUsername(username, designation);
            return name;
        }
        public int ChangePassBAL(string pass, string username)
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            int i = dal.changePassDAL(pass, username);
            return i;
        }
        public List<BOCandidateOfferStatus> waitingApprovalCandidate()
        {
            EmployeeDetailsDAL dal = new EmployeeDetailsDAL();
            List<BOCandidateOfferStatus> li = dal.waitingApprovalCandidate();
            return li;
        }


    }

}

