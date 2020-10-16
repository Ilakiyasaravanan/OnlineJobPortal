using System.Collections.Generic;
using JobPortal.Entity;
using JobPortal.DAL;
namespace JobPortal.BL
{
	public class AccountMediator:IAccountMediator
	{
		readonly IAccountRepository accountRepository;
		public AccountMediator() //Parameterless contructor
		{
			accountRepository = new AccountRepository();
		}
		public int AddAccountDetails(AccountDetails account)  //Insert details
		{
			int result= accountRepository.Add(account);
			return result;
		}
		public AccountDetails CheckAccountDetails(AccountDetails acc)  //Login details
		{
			AccountDetails account = accountRepository.Check(acc);
			return account;
		
		}
		public bool CheckAccount(string acc)  //check userid exists
		{
			bool account = accountRepository.AccountExists(acc);
			return account;

		}
		public IEnumerable<AccountDetails> View()  //Get details
		{
			return accountRepository.GetDetails();
		}
		public IEnumerable<AccountDetails> View(int id)  //Get particular detail of user
		{
			return accountRepository.GetDetails();
		}
		public AccountDetails Edit(int id)  //Edit details
		{
			return accountRepository.EditValue(id);
		}
		public void Delete(int id)  //Delete details
		{
			accountRepository.RemoveValue(id);
		}
		public int Update(AccountDetails account) //Update details
		{
			int result= accountRepository.Update(account);
			return result;
		}
		public AccountDetails ParticularDetails(int id)//Get Particular details
		{
			return accountRepository.GetParticularDetails(id);
		}
		
	}
}
