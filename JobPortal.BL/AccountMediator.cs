using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Entity;
using JobPortal.DAL;
namespace JobPortal.BL
{
    public class AccountMediator
    {
         public AccountMediator()
            {
            }
            public bool AddAccountDetails(AccountDetails account)
            {
                bool status = new AccountRepository().Add(account);
                return status;
            }
            public bool CheckAccountDetails(AccountDetails acc)
            {
                bool loginStatus = new AccountRepository().Check(acc);
                return loginStatus;
            }

        }
    }
