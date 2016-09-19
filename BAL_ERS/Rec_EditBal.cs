using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_ERS;
using BO_ERS;

namespace BAL_ERS
{
    public class editbal
    {

        public List<int> editbaldrop()
        {
            editdal d = new editdal();
            List<int> du = new List<int>();
            du = d.editdaldrop();
            return du;
        }

        public List<Data_Vacancy> displayeditbal(string plist)
        {
            editdal d = new editdal();
            List<Data_Vacancy> du = new List<Data_Vacancy>();
            du = d.editdisplaydal(plist);
            return du;

        }
        public List<Data_Vacancy> edadddisbal(string recid)
        {
            editdal d = new editdal();
            List<Data_Vacancy> du = new List<Data_Vacancy>();
            du = d.edadddisdal(recid);
            return du;

        }



        public void editaddbal(string[] chk, string recid)
        {
            editdal d = new editdal();
            d.editadddal(chk, recid);
        }

        public void editremovebal(string[] chk, string recid)
        {
            editdal d = new editdal();
            d.editremovedal(chk, recid);
        }
    }
}
