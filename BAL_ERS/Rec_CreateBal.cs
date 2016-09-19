using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using DAL_ERS;

namespace BAL_ERS
{
    public class createbal
    {
        createdal d1 = new createdal();
        public List<Data_Vacancy> displayvacbal(string loc)
        {
            List<Data_Vacancy> du = new List<Data_Vacancy>();
            du = d1.displayvacdal(loc);
            return du;
        }

        public List<int> pciddropbal(string loc)
        {
            List<int> du = new List<int>();
            du = d1.pciddropdal(loc);
            return du;
        }

        public List<string> locdropbal()
        {
            List<string> du = new List<string>();
            du = d1.locdropdal();
            return du;
        }

        public void insertbal(string[] items, string plist)
        {
            d1.insertdal(items, plist);
        }
    }
}
