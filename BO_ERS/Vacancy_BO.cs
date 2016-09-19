using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BO_ERS
{

    public class VacancyRequestBO
    {


        //[Required(ErrorMessage = "required")] 
        [Range(1,100,ErrorMessage="Position must be a number between 1 and 100")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Position must be  Numbers only.")]
        //[Range(typeof(int), "1", "100", ErrorMessage = "Position must be a number between 1 and 100")]
           

        public int NoOfPosition { get; set; }


        [Required(ErrorMessage = "Enter Skills")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Skills should be alphabetic")]
        public string skills { get; set; }

        [Range(0, 100, ErrorMessage = "Experience should be Positive")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "Enter Location")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Location should be alphabetic")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Enter Domain")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Domain should be alphabetic")]
        public string Domain { get; set; }

        public DateTime RequireByDate { get; set; }

    }

    public class DeleteVacancyBO
    {
        public int Vac_VacancyID { get; set; }
        public int Vac_NoOfPosition { get; set; }
        public string Vac_skill { get; set; }
        public int Vac_Experience { get; set; }
        public string Vac_location { get; set; }
        public string Vac_BusinessDomain { get; set; }
        public string Vac_RequiredByDate { get; set; }
        public string Vac_Status { get; set; }
        public int Vac_RecruitmentRequestID { get; set; }
        public string Vac_ApprovalStatus { get; set; }
        public int Vac_isdeleted { get; set; }
    }

    public class DisplayBO
    {
        public int Vac_VacancyID { get; set; }
        public int NoOfPosition { get; set; }

        public string skills { get; set; }

        public int Experience { get; set; }

        public string Location { get; set; }

        public string Domain { get; set; }

        public DateTime RequireByDate { get; set; }


    }
    public class ApproveVacancyBO
    {
        public int Vac_VacancyID { get; set; }
        public int Vac_NoOfPosition { get; set; }
        public string Vac_skill { get; set; }
        public int Vac_Experience { get; set; }
        public string Vac_location { get; set; }
        public string Vac_BusinessDomain { get; set; }
        public string Vac_RequiredByDate { get; set; }
        public string Vac_Status { get; set; }
        public int Vac_RecruitmentRequestID { get; set; }
        public string Vac_ApprovalStatus { get; set; }
        public int Vac_isdeleted { get; set; }
    }
    public class Data_Vacancy
    {
        public bool ischecked { get; set; }
        public int Vac_VacancyID { get; set; }
        public int Vac_NoOfPositions { get; set; }
        public string Vac_Skills { get; set; }
        public int Vac_Experience { get; set; }
        public string Vac_Location { get; set; }
        public string Vac_Business_Domain { get; set; }
        [DataType(DataType.Date)]
        public DateTime Vac_RequiredByDate { get; set; }
        public string Vac_Status { get; set; }
        public int Vac_RecruitmentRequestID { get; set; }
        public string Vac_ApprovalStatus { get; set; }
        public int Vac_IsDeleted { get; set; }
    }
    public class Data_Recruitment
    {

        public int Rec_RecruitmentRequestID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Rec_RequesteddDate { get; set; }
        public int Rec_PlacementConsultantID { get; set; }
        public int Vac_IsDeleted { get; set; }
    }

    public class Data_Placement_Consultant
    {


        public int PC_PlacementConsultantID { get; set; }
        public string PC_PlacementConsultantName { get; set; }
        public string PC_Password { get; set; }
        public string PC_Location { get; set; }
        public string PC_Email { get; set; }
        public int PC_IsDeleted { get; set; }
    }

}

