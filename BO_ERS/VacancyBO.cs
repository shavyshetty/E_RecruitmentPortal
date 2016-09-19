using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class VacancyBO
    {
        public int id { get; set; }
        public int noof_vacancies { get; set; }
        public string skills { get; set; }
        public int experience { get; set; }
        public string location { get; set; }
        public string domain { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string appststus { get; set; }
    }
}