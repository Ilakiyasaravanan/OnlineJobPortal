using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.DAL;
using JobPortal.Entity;
namespace JobPortal.BL
{
	public class JobMediator
	{
		public bool AddJobDetails(JobDetails account)
		{
			bool status = new JobRepository().Add(account);
			return status;
		}
		public bool AddDetails(JobDetails account)
		{
			bool status = new JobRepository().AddCandidateDetails(account);
			return status;
		}
	}
}
