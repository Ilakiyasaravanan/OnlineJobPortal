using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using JobPortal.Entity;
namespace JobPortal.DAL
{
	class DBUtills : DbContext
	{
		public DBUtills() : base("DbConnection") { }
		public DbSet<AccountDetails> AccountDb { get; set; }
	}
}
