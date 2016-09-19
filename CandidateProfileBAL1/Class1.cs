using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateProfileBO1;
using CandidateProfileDAL1;


namespace CandidateProfileBAL1
{
    //canduidate profile class
    public class CandidateProfileBALClass
    {

        CandidateProfileDALClass can_dal = new CandidateProfileDALClass();
        CandidateProfileBOClass can_bo;

        //populate vacancy list
        public List<int> populateVacancies(string Role, int ID, int Role_factor)
        {

            List<int> vac_list = can_dal.populateVacancies(Role, ID, Role_factor);
            return vac_list;
        }

        //display candidate details
        public List<CandidateProfileBOClass> displayCandidateProfile(string vacancy_id, int Role)
        {
            List<CandidateProfileBOClass> list_can = can_dal.displayCandidateProfile(vacancy_id, Role);
            return list_can;
        }
        //display candidate details HR
        public List<CandidateProfileBOClassHR> displayCandidateProfileHR(string vacancy_id, int Role)
        {
            List<CandidateProfileBOClassHR> list_can = can_dal.displayCandidateProfileHR(vacancy_id, Role);
            return list_can;
        }


        //edit candidate profile get candidate object
        public CandidateProfileBOClass editCandidateProfileRecord(int Can_CandidateID)
        {
            this.can_bo = can_dal.editCandidateProfileRecord(Can_CandidateID);
            return this.can_bo;
        }

        //store edited values
        public void storeEditValues(CandidateProfileBOClass can_bo)
        {
            can_dal.storeEditValues(can_bo);
            return;
        }

        //delete candidate profile
        public void deleteCandidateProifle(int Can_CandidateID)
        {
            can_dal.deleteCandidateProifle(Can_CandidateID);
            return;
        }

        public int createCandidateProfile(CandidateProfileBOClass can_bo)
        {
            int can_id = can_dal.createCandidateProfile(can_bo);
            return can_id;
        }



    }

    //class2.cs file
    public class CreateTestAdminBAL
    {
        public string createTestAdminBAL(AddTestAdmin objectAddTestAdmin)
        {
            string msg = "";
            CreateTestAdminDAL objCreateTestAdminDAL = new CreateTestAdminDAL();
            msg = objCreateTestAdminDAL.createTestAdminDAL(objectAddTestAdmin);
            return msg;
        }

    }

    public class DeleteTestAdminBAL
    {
        public string deleteTestAdminBAL(DeleteTestAdmin objDeleteTestAdmin)
        {
            string msg = "";
            DeleteTestAdminDAL objDeleteTestAdminDAL = new DeleteTestAdminDAL();
            msg = objDeleteTestAdminDAL.deleteTestAdminDAL(objDeleteTestAdmin);
            return msg;
        }

    }

    public class DisplayTestAdminBAL
    {
        public List<Employee> displayTestAdminBAL(DisplayTestAdmin objDisplayTestAdmin, ref string msg)
        {

            DisplayTestAdminDAL objDisplayTestAdminDAL = new DisplayTestAdminDAL();
            List<Employee> listemployee = objDisplayTestAdminDAL.displayTestAdminDAL(objDisplayTestAdmin, ref msg);
            return listemployee;
        }
    }


    //loginBal.cs file
    public class loginBAL
    {
        public user checkLoginBAL(user objlogin)
        {
            return new loginDAL().checkLoginDAL(objlogin);
        }
        public bool checkPasswordBAL(user objlogin)
        {
            return new loginDAL().checkPasswordDAL(objlogin);
        }
        public bool changePasswordBAL(user objlogin)
        {
            return new loginDAL().changePasswordDAL(objlogin);
        }

        public bool checkUserBAL(user objlogin)
        {
            return new loginDAL().checkUserDAL(objlogin);
        }

    }

    //TestandInterviewBAL.cs file
    public class TestandInterviewBAL
    {
        public List<int> getTestAdministratorIDListBAL()
        {
            return new TestandInterviewDAL().getTestAdministratorIDListDAL();
        }
        public List<int> getVacancyIDListBAL()
        {
            return new TestandInterviewDAL().getVacancyIDListDAL();
        }
        public List<int> getCreatedScheduleVacancyIDListBAL()
        {
            return new TestandInterviewDAL().getCreatedScheduleVacancyIDListDAL();
        }
        public List<int> displayVacancyIDListBAL()
        {
            return new TestandInterviewDAL().displayVacancyIDListDAL();
        }
        public string getRequiredByDateBAL(int VacancyID)
        {
            return new TestandInterviewDAL().getRequiredByDateDAL(VacancyID);
        }
        public ScheduleDetails createTestandInterviewBAL(ScheduleDetails testObject)
        {
            TestandInterviewDAL objectTestandInterviewDAL = new TestandInterviewDAL();
            testObject.WrittenTestDate = convertDate(testObject.WrittenTestDate);
            testObject.TechnicalInterviewDate = convertDate(testObject.TechnicalInterviewDate);
            testObject.HRInterviewDate = convertDate(testObject.HRInterviewDate);
            testObject.RecruitmentRequestID = objectTestandInterviewDAL.getRecruitmentRequestID(testObject.VacancyID);
            if (testObject.RecruitmentRequestID == -1)
            {
                return null;
            }
            else
            {
                objectTestandInterviewDAL.createTestandInterviewDAL(testObject);
                testObject = objectTestandInterviewDAL.getcreatedTestandInterviewDAL(testObject);
            }
            return testObject;
        }
        public DisplayCandidateDetails displayTestsAndInterviewScheduledBAL(int VacancyID)
        {
            return new TestandInterviewDAL().displayTestsAndInterviewScheduledDAL(VacancyID);
        }

        public DisplayCandidateDetails displayMedicalStatusBAL(int VacancyID)
        {
            return new TestandInterviewDAL().displayMedicalStatusDAL(VacancyID);
        }


        public ScheduleDetails getTestsAndInterviewScheduledBAL(int VacancyID)
        {
            return new TestandInterviewDAL().getTestsAndInterviewScheduledDAL(VacancyID);
        }
        public ScheduleDetails editTestsAndInterviewsScheduledBAL(ScheduleDetails testObject)
        {
            testObject.WrittenTestDate = convertDate(testObject.WrittenTestDate);
            testObject.TechnicalInterviewDate = convertDate(testObject.TechnicalInterviewDate);
            testObject.HRInterviewDate = convertDate(testObject.HRInterviewDate);
            TestandInterviewDAL objTestandInterviewDAL = new TestandInterviewDAL();

            testObject.TestID = objTestandInterviewDAL.getTestID(testObject.VacancyID);
            return objTestandInterviewDAL.editTestsAndInterviewsScheduledDAL(testObject);
        }
        public string convertDate(string date)
        {
            string newdate;
            string[] splitdate = date.Split('/');
            newdate = splitdate[2] + "/" + splitdate[1] + "/" + splitdate[0];
            return newdate;
        }
        public string editTestStatusOfCandidatesBAL(Test_Status objTestStatus)
        {
            TestandInterviewDAL objTestandInterviewDAL = new TestandInterviewDAL();
            return objTestandInterviewDAL.editTestStatusOfCandidatesDAL(objTestStatus);
        }

        public string updateMedicalStatusBAL(UpdateMedicalStatus objUpdateMedicalStatus)
        {
            TestandInterviewDAL objTestandInterviewDAL = new TestandInterviewDAL();
            return objTestandInterviewDAL.updateMedicalStatusDAL(objUpdateMedicalStatus);
        }


    }


}
