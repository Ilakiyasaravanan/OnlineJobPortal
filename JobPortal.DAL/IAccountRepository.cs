using System.Collections.Generic;
using JobPortal.Entity;
namespace JobPortal.DAL
{
	public interface IAccountRepository
	{
		IEnumerable<AccountDetails> GetDetails();
		AccountDetails GetParticularDetails(int id);
		void RemoveValue(int idValue);
		AccountDetails EditValue(int idValue);
		int Add(AccountDetails job);
		int Update(AccountDetails job);
		AccountDetails Check(AccountDetails log);
		
		bool AccountExists(string account);

	}
}
