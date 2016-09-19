using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_ERS;
using BO_ERS;

namespace BAL_ERS
{
    public class viewbal
    {
        public List<int> piddropbal()
        {
            viewdal d1 = new viewdal();
            List<int> du = new List<int>();
            du = d1.piddropdal();
            return du;
        }

        public List<Data_Vacancy> viewdisbal(string pid, string txt)
        {
            viewdal d1 = new viewdal();
            List<Data_Vacancy> du = new List<Data_Vacancy>();
            du = d1.viewdisdal(pid, txt);
            return du;
        }

    }
}
