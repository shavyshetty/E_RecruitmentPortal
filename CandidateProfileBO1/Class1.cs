using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace CandidateProfileBO1
{
    public class Candidate_Profile
    {
        int _CandidateProfileID;
        public int CandidateProfileID
        {
            get
            {
                return _CandidateProfileID;
            }
            set
            {
                _CandidateProfileID = value;
            }
        }

        int _VacancyID;
        public int VacancyID
        {
            get
            {
                return _VacancyID;
            }
            set
            {
                _VacancyID = value;
            }
        }
        string _CandidateName;
        public string CandidateName
        {
            get
            {
                return _CandidateName;
            }
            set
            {
                _CandidateName = value;
            }
        }

        DateTime _DOB;
        public DateTime DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
            }
        }

        string _Location;
        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }
        string _Gender;
        public string Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = value;
            }
        }
        float _Percentage10;
        public float Percentage10
        {
            get
            {
                return _Percentage10;
            }
            set
            {
                _Percentage10 = value;
            }
        }

        float _Percentage12;
        public float Percentage12
        {
            get
            {
                return _Percentage12;
            }
            set
            {
                _Percentage12 = value;
            }
        }
        string _GapInEducation;
        public string GapInEducation
        {
            get
            {
                return _GapInEducation;
            }
            set
            {
                _GapInEducation = value;
            }
        }

        string _GapInExperience;
        public string GapInExperience
        {
            get
            {
                return _GapInExperience;
            }
            set
            {
                _GapInExperience = value;
            }
        }

        int _Experience;
        public int Experience
        {
            get
            {
                return _Experience;
            }
            set
            {
                _Experience = value;
            }
        }
        /*
         * 
         * 
         * 
         * Resume file
         * 
         * */

        int _TestID;
        public int TestID
        {
            get
            {
                return _TestID;
            }
            set
            {
                _TestID = value;
            }
        }

        int _TestStatus;
        public int TestStatus
        {
            get
            {
                return _TestStatus;
            }
            set
            {
                _TestStatus = value;
            }
        }

        int _MedicalTestStatus;// check datatype with db table its bit
        public int MedicalTestStatus
        {
            get
            {
                return _MedicalTestStatus;
            }
            set
            {
                _MedicalTestStatus = value;
            }
        }

        int _BGCTestID;
        public int BGCTestID
        {
            get
            {
                return _BGCTestID;
            }
            set
            {
                _BGCTestID = value;
            }
        }
        int _BCGTestStatus;// check datatype with db table its bit
        public int BCGTestStatus
        {
            get
            {
                return _BCGTestStatus;
            }
            set
            {
                _BCGTestStatus = value;
            }
        }
    }

    //class1.cs file
    public class CandidateProfileBo1Class
    {


        public int Can_VacancyID { get; set; }
        public int Can_CandidateID { get; set; }
        public string Can_CandidateName { get; set; }
        public DateTime Can_DOB { get; set; }
        public string Can_Location { get; set; }
        public string Can_Gender { get; set; }
        public float Can_Percentage10 { get; set; }
        public float Can_Percentage12 { get; set; }
        public float Can_Experience { get; set; }
        public int Can_YearsOfGapEducation { get; set; }
        public float Can_YearsOfGapExperience { get; set; }
        public string Can_ReasonsForGapEducation { get; set; }
        public string Can_ReasonsForGapExperience { get; set; }
        public string Can_ResumeFile { get; set; }
        public int Can_TestID { get; set; }
        public int Can_TestStatus { get; set; }
        public int Can_MedicalTestStatus { get; set; }
        public bool Can_Offer_Letter_Status { get; set; }
        public int Can_BGCTestID { get; set; }
        public bool Can_BGCTestStatus { get; set; }
        public int Can_IsDeleted { get; set; }
        public string WrittenTestDate { get; set; }
        public string HRInterviewDate { get; set; }
        public string TechnicalInterviewDate { get; set; }
    }

    //class2.cs file
    public class Employee
    {
        // public string location { get; set; }
        // public int experience { get; set; }
        public int Employee_ID { get; set; }
        public string Test_Administrator { get; set; }
        public string designation { get; set; }
        public string Employee_Role { get; set; }
    }

    public class AddTestAdmin
    {
        public int emp_id;
    }
    public class DeleteTestAdmin
    {
        public int emp_id;
    }
    public class DisplayTestAdmin
    {
        public int emp_id;
        public string emp_name;
    }

    //class3.cs file
    [DataContract]
    public class CandidateProfileBOClass
    {
        [DataMember]
        [DisplayName("Vacancy ID")]
        public int Can_VacancyID { get; set; }

        [DisplayName("Candidate ID")]
        public int Can_CandidateID { get; set; }

        [DataMember]
        [DisplayName("Candidate Name")]
        public string Can_CandidateName { get; set; }

        [DataMember]
        [DisplayName("DOB (yyyy/mm/dd)")]
        public string Can_DOB { get; set; }

        [DataMember]
        [DisplayName("Location")]
        public string Can_Location { get; set; }

        [DataMember]
        [Display(Name = "Gender")]
        public string Can_Gender { get; set; }

        [DataMember]
        [DisplayName("Percentage 10")]
        public float Can_Percentage10 { get; set; }

        [DataMember]
        [DisplayName("Percentage 12")]
        public float Can_Percentage12 { get; set; }

        [DataMember]
        [DisplayName("Experience")]
        public float Can_Experience { get; set; }

        [DataMember]
        [DisplayName("Gap in Education")]
        public int Can_YearsOfGapEducation { get; set; }

        [DataMember]
        [DisplayName("Gap in Experience")]
        public float Can_YearsOfGapExperience { get; set; }

        [DataMember]
        [DisplayName("Reason for Gap in Education")]
        public string Can_ReasonsForGapEducation { get; set; }

        [DataMember]
        [DisplayName("Reason for Gap in Experience")]
        public string Can_ReasonsForGapExperience { get; set; }

        [DisplayName("Resume")]
        public HttpPostedFileBase Can_ResumeFile { get; set; }

        [DataMember]
        [DisplayName("Resume")]
        public string Can_ResumeFileName { get; set; }

        [Display(Name = "Test ID")]
        public int Can_TestID { get; set; }

    }
    public class CandidateProfileBOClassHR : CandidateProfileBOClass
    {
        [Display(Name = "Test Status")]
        public int Can_TestStatus { get; set; }

        [Display(Name = "Medical Test Status")]
        public int Can_MedicalTestStatus { get; set; }

        [Display(Name = "Offer Letter Status")]
        public string Can_Offer_Letter_Status { get; set; }

        [Display(Name = "BGC ID")]
        public int Can_BGCTestID { get; set; }

        [Display(Name = "BGC Test Status")]
        public bool Can_BGCTestStatus { get; set; }

        [Display(Name = "Written Test Date")]
        public string WrittenTestDate { get; set; }

        [Display(Name = "Technical Test Date")]
        public string TechnicalInterviewDate { get; set; }

        [Display(Name = "HR Test Date")]
        public string HRInterviewDate { get; set; }
    }


    //displaycandidatedetails.cs file
    public class DisplayCandidateDetails
    {
        public List<int> VacancyIDList = new List<int>();
        public List<int> can_id = new List<int>();
        public List<int> can_status = new List<int>();
        public string WrittenTestDate;
        public bool canEditWrittenTestDate = false;
        public string TechnicalInterviewDate;
        public bool canTechnicalInterviewDate = false;
        public string HRInterviewDate;
        public bool canEditHRInterviewDate = false;
        public string status;
    }

    //scheduledetails.cs file
    public class ScheduleDetails
    {
        public int TestID { get; set; }
        public int TestAdministratorID { get; set; }
        public int RecruitmentRequestID { get; set; }
        public int VacancyID { get; set; }
        public string WrittenTestDate { get; set; }
        public string TechnicalInterviewDate { get; set; }
        public string HRInterviewDate { get; set; }
        public string RequiredByDate { get; set; }
        public List<int> listVacancyID = new List<int>();
        public List<int> listTestAdminID = new List<int>();
        public bool flag = false;
        public bool canEditWrittenTestDate = false;
        public bool canEditTechnicalInterviewDate = false;
        public bool canEditHRInterviewDate = false;
    }

    //test details.cs file
    public class Test_Details
    {
        public int TestID { get; set; }
        public int TestAdministratorID { get; set; }
        public int RecruitmentRequestID { get; set; }
        public int VacancyID { get; set; }
        public string WrittenTestDate { get; set; }
        public string TechnicalInterviewDate { get; set; }
        public string HRInterviewDate { get; set; }
        public string RequiredByDate { get; set; }
    }

    //teststatus.cs file
    public class Test_Status
    {
        public int CandidateID;
        public int TestStatus;
    }

    //updateMedical Status.cs file
    public class UpdateMedicalStatus
    {
        public int CandidateID;
        public int Status;
    }

    //user.cs file
    public class user
    {
        [Required()]
        [RegularExpression(@"\d+", ErrorMessage = "Enter Digits Only")]
        public string username { get; set; }
        [Required()]
        public string password { get; set; }
        public string role { get; set; }
        public string dob { get; set; }
        public string name { get; set; }
    }

}
