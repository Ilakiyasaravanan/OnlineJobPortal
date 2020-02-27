using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Entity;
namespace JobPortal.DAL
{
	public class JobRepository
	{
		public static List<JobDetails> recuriterDetails = new List<JobDetails>();
        public static List<JobDetails> candidateDetails = new List<JobDetails>();
        public JobRepository()
		{	}

        public bool Add(JobDetails job)
        {
            bool status = false;
            try
            {
                recuriterDetails.Add(job);
                return status = true; ;
            }
            catch
            {
                return status;
            }
        }
        public bool AddCandidateDetails(JobDetails job)
        {
            bool status = false;
            try
            {
                candidateDetails.Add(job);
                return status = true; ;
            }
            catch
            {
                return status;
            }
        }

    }
}
