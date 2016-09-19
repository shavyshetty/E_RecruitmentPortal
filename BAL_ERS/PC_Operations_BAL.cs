using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_ERS;
using BO_ERS;

namespace BAL_ERS
{
    public class PC_Operations_BAL
    {
        PC_Operations_DAL pod = new PC_Operations_DAL();

        public void add_PC_bal(PC_BO b)
        {
            pod.add_pc_dal(b);
        }
        public List<PC_BO> view_pc_Bal()
        {

            List<PC_BO> bal = new List<PC_BO>();
            bal = pod.dal_pc_view();
            return bal;
        }
        public void edit_pc_bal(int id,PC_BO b1)
        {
            pod.edit_pc_DAL(id,b1);
        }
        public void del_pc_bal(int id)
        {
            pod.del_pc_dal(id);
        }
        //public void editshowTO_pc_bal(int id)
        //{
        //    pod.editshow_pc_dal1(id);
        //}
        //public List<PC_BO> editshow_pc_bal()
        //{
        //    List<PC_BO> list = new List<PC_BO>();
        //    list = pod.editshow_pc_dal();
        //    return list;
        //}

        
    }
}
