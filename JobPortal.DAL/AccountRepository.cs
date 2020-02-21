using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Entity;
using JobPortal.Common;
namespace JobPortal.DAL
{
    public class AccountRepository
    {
        public static List<AccountDetails> details = new List<AccountDetails>();
        static AccountRepository()
        {

            details.Add(new AccountDetails { FirstName = "Ilakiya", LastName = "Saravanan", Address = "Salem", Gender = "Female", PhoneNumber = 9443322727, Password = "ilakiya", ConfirmPassword = "ilakiya", Role = "Recruiter", country = (Country)Enum.Parse(typeof(Country), "India"), Email = "ilakya@gmail.com" });
        }
        public AccountRepository()
        {
        }
        public IEnumerable<AccountDetails> GetDetails()
        {
            return details;
        }
        public bool Add(AccountDetails job)
        {
            bool registerStatus = false;
            try
            {
                details.Add(job);
                return registerStatus = true; ;
            }
            catch
            {
                return registerStatus;

            }


        }
        public bool Check(AccountDetails log)
        {
            bool status = false;
            foreach (AccountDetails i in details)
            {
                try
                {
                    if ((log.Email.Equals(i.Email) && log.Password.Equals(i.Password)))

                        status = true;
                }
                catch
                {
                    return status = false;
                }
               
               
            }

            return status;
        }
    }


}

