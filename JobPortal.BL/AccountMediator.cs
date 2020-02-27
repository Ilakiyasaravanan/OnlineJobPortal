using System.Collections.Generic;
using JobPortal.Entity;
using JobPortal.DAL;
namespace JobPortal.BL
{
    public class AccountMediator
    {
        public void AddAccountDetails(AccountDetails account)
        {
            new AccountRepository().Add(account);
            
        }
        public string CheckAccountDetails(AccountDetails acc)
            {
                string loginStatus = new AccountRepository().Check(acc);
                return loginStatus;
            }
           public IEnumerable<AccountDetails> View()
           {
            return new AccountRepository().GetDetails();

          }
            }
    }
