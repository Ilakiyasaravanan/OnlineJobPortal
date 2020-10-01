using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		//IEnumerable<Country> GetCountry();
		//void AddCountry(Country country);
		//void RemoveCountry(int idValue);
		//Locations EditCountry(int idValue); 
		//void UpdateCountry(Country country);
		bool AccountExists(string account);

	}
}
