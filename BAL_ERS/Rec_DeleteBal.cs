using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_ERS;
using BO_ERS;


namespace BAL_ERS
{
    public class deletebal
    {


        public List<int> reciddropbal()
        {
            deletedal d = new deletedal();
            List<int> du = new List<int>();
            du = d.reciddropdal();
            return du;
        }


        public List<Data_Vacancy> displaylistbal(string plist)
        {
            deletedal d = new deletedal();
            List<Data_Vacancy> du = new List<Data_Vacancy>();
            du = d.displaylistdal(plist);
            return du;

        }

        public void deleterecbal(string rlist)
        {
            deletedal d = new deletedal();
            d.deleterecdal(rlist);
        }
    }
}
