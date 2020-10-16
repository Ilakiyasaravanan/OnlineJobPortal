using System.Collections.Generic;
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
		
		bool CheckAccount(string acc);
	


	}
}
