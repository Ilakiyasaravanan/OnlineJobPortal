using System;
using System.Collections.Generic;
using System.Linq;
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
            DBUtills dBUtills = new DBUtills();
            return dBUtills.AccountDb.ToList();
        }
        public void Add(AccountDetails job)
        {
            DBUtills db = new DBUtills();
            db.AccountDb.Add(job);
            db.SaveChanges();
        }
        public string Check(AccountDetails log)
        {
            string role = "empty";
            using (DBUtills db = new DBUtills())
            {
                var get_user = db.AccountDb.Single(p => p.Email == log.Email
                && p.Password == log.Password);
                if (get_user != null)
                {
                    //Session["UserId"] = get_user.UserID.ToString();
                    //Session["UserName"] = get_user.UserName.ToString();
                    //return RedirectToAction("LoggedIn");
                    role = get_user.Email;
                }
            }
           return role;
            //else
            //{
            //    ModelState.AddModelError("", "UserName or Password does not match.");
            //}

            //string role = "empty";
            //foreach (AccountDetails i in details)
            //{
            //    try
            //    {
            //        if ((log.Email.Equals(i.Email) && log.Password.Equals(i.Password)))
            //        {
            //            role = i.Role;
            //        }
            //    }
            //    catch
            //    {
            //        throw new Exception("ErrorMessage");
            //    }              

            //}


        }
    }
}




