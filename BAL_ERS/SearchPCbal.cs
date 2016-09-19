using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using DAL_ERS;
namespace BAL_ERS
{
    public class SearchPCbal
    {
        public List<PC_BO> searchbal(string pctxt)
        {
            List<PC_BO> pclist = new List<PC_BO>();
            SearchPCdal s = new SearchPCdal();
            pclist = s.searchdal(pctxt);
            return pclist;


        }
    }
}
