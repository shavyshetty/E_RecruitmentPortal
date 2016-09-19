using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_ERS;
using DAL_ERS;

namespace BAL_ERS
{
    public class LoginBAL
    {

        public LoginBO LoginCheckBAL(LoginBO lb)
        {
            LoginDAL dl = new LoginDAL();

            LoginBO lbo = dl.LoginCheckDAL(lb);
            string des = lb.Login_Designation;

            if (lbo != null)
            {
                if (lbo.Login_ID == lb.Login_ID && lbo.Login_Password.Equals(lb.Login_Password))
                {
                    LoginBO sessionlogin = new LoginBO();
                    sessionlogin.Login_ID = lbo.Login_ID;
                    sessionlogin.Login_Designation = lbo.Login_Designation;
                    sessionlogin.Login_Role = lbo.Login_Role;
                    return sessionlogin;
                }
                else
                    return null;
            }


            else
            {
                return null;
            }

        }


        public LoginBO ResetLoginBAL(LoginBO lb)
        {
            LoginDAL dl = new LoginDAL();

            LoginBO lbo = dl.LoginCheckDAL(lb);
            string des = lb.Login_Designation;

            if (lbo != null)
            {
                if (lbo.Login_ID == lb.Login_ID)
                {
                    LoginBO sessionlogin = new LoginBO();
                    sessionlogin.Login_ID = lbo.Login_ID;
                    sessionlogin.Login_Designation = lbo.Login_Designation;
                    sessionlogin.Login_Role = lb.Login_Role;
                    return sessionlogin;
                }
                else
                    return null;
            }


            else
            {
                return null;
            }

        }
        public void reset_password_bal(LoginBO lb)
        {
            LoginDAL dl = new LoginDAL();
            dl.reset_password_dal(lb);
        }

        public bool change_password_bal(LoginBO lb, string pass)
        {
            LoginDAL dl = new LoginDAL();
            string cur_db_pass = dl.chk_change_password_dal(lb);
            if (cur_db_pass.Equals(pass))
            {
                dl.change_password_dal(lb);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
