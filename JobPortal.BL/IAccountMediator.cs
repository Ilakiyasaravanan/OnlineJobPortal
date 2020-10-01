using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Entity;
namespace JobPortal.BL
{
	public interface IAccountMediator
	{
		int AddAccountDetails(AccountDetails account);
		AccountDetails CheckAccountDetails(AccountDetails acc);
	    IEnumerable<AccountDetails> View();
		IEnumerable<AccountDetails> View(int id);
		AccountDetails Edit(int id);
		void Delete(int id);
		int Update(AccountDetails account);

		AccountDetails ParticularDetails(int id);
		//IEnumerable<Country> GetCountry();

		//void AddCountry(Country country);
		bool CheckAccount(string acc);
	


	}
}
