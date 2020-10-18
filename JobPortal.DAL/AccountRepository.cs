using System;
using System.Collections.Generic;
using System.Linq;
using JobPortal.Entity;
using JobPortal.Common;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace JobPortal.DAL
{
	[ExceptionHandler]
	public class AccountRepository : IAccountRepository
	{
		public AccountRepository() //Constructor
		{
		}

		public IEnumerable<AccountDetails> GetDetails()  //View DB Details
		{

			using (DBUtills dBUtills = new DBUtills())
			{
				IEnumerable<AccountDetails> account = null;
				account = dBUtills.AccountDb.Where(s => s.Role != "Admin").ToList();

				return account;
			}
		}
		public AccountDetails GetParticularDetails(int id)  //View particular Details
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				AccountDetails account = null;

				account = dBUtills.AccountDb.Find(id);


				return account;

			}
		}
		public void RemoveValue(int idValue)  //Delete DB Details
		{
			using (DBUtills dBUtills = new DBUtills())
			{

				AccountDetails account = dBUtills.AccountDb.Find(idValue);
				dBUtills.AccountDb.Remove(account);
				dBUtills.SaveChanges();

			}

		}
		public AccountDetails EditValue(int idValue)  //Edit DB Details
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				AccountDetails account = null;

				account = dBUtills.AccountDb.Find(idValue);

				return account;
			}
		}
		public bool AccountExists(string account)//Account id exists or not
		{
			bool isexists = false;		

			using (DBUtills db = new DBUtills())
			{
				string encryptUser = Encrypt(account);

				isexists = db.AccountDb.Any(x => x.Email == encryptUser);

			}
			return isexists;
		}
		//Encrypt password
		static string Encrypt(string clearText)
		{
			string EncryptionKey = "MAKV2SPBNI99212";
			byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					clearText = Convert.ToBase64String(ms.ToArray());
				}
			}
			return clearText;
		}
		//Decrypt password

		static string Decrypt(string cipherText)
		{
			string EncryptionKey = "MAKV2SPBNI99212";
			byte[] cipherBytes = Convert.FromBase64String(cipherText);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					cipherText = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return cipherText;

		}

		public int Add(AccountDetails job)  //Insert DB Details
		{
			string encryptPassword = Encrypt(job.Password);
			
				using (DBUtills db = new DBUtills())
				{
					job.Password = encryptPassword;
					db.AccountDb.Add(job);
					db.SaveChanges();
					return job.AccountId;
				}

				//SqlParameter firstName = new SqlParameter("@FirstName", job.FirstName);//sample for stored procedures
				//SqlParameter lastName = new SqlParameter("@LastName", job.LastName);
				//SqlParameter address = new SqlParameter("@Address", job.Address);
				//SqlParameter gender = new SqlParameter("@Gender", job.Gender);
				//SqlParameter phone = new SqlParameter("@PhoneNumber", job.PhoneNumber);
				//SqlParameter password = new SqlParameter("@Password", job.Password);
				//SqlParameter role = new SqlParameter("@Role", job.Role);
				//SqlParameter countryId = new SqlParameter("@CountryId", job.CountryId);
				//SqlParameter email = new SqlParameter("@Email", job.Email);
				//try
				//{
				//int result = db.Database.ExecuteSqlCommand("sp_InsertAccountDetails @FirstName,@LastName, @Address,@Gender,@PhoneNumber,@Password,@Role,@CountryId,@Email", firstName, lastName, address, gender, phone, password, role, countryId, email);
				//return result;
				//}
				//catch (System.Data.SqlClient.SqlException)
				//{
				//	return 0;
				//}
				//db.AccountDb.Add(job);
				//db.SaveChanges();
			
		}
		public int Update(AccountDetails job)  //Update details
		{
			using (DBUtills dBUtills = new DBUtills())
			{

				dBUtills.Entry(job).State = EntityState.Modified;
				dBUtills.SaveChanges();
				return 1;

			}
		}
		public AccountDetails Check(AccountDetails log)  //Login Check
		{
			AccountDetails account = new AccountDetails();

			using (DBUtills db = new DBUtills())
			{
				string encryptPassword= Encrypt(log.Password);				
				var getValues = db.AccountDb.SingleOrDefault(p => p.Email == log.Email && p.Password == encryptPassword);
				if (getValues != null)
				{
					string decryptPassword = Decrypt(getValues.Password);
					if (log.Password == decryptPassword && log.Email == getValues.Email)
					{
						account.Role = getValues.Role;
						account.AccountId = getValues.AccountId;
						account.Email = getValues.Email;
						return account;

					}
				}			

			}
			return account = null;
		}
	} }
		