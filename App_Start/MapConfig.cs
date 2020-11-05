using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Entity;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.App_Start
{
	public class MapConfig
	{
		public static void RegisterMaps()
		{
			AutoMapper.Mapper.Initialize(config =>
			{
				config.CreateMap<AccountViewModel, AccountDetails>();
				config.CreateMap<RecruiterJobViewModel, RecruiterJobDetails>();
				config.CreateMap<SearcherJobViewModel, SearcherJobDetails>();
				config.CreateMap<AccountDetails, AccountViewModel>();
				config.CreateMap<CgpaViewModel, Cgpas>();
				config.CreateMap<LoginViewModel, AccountDetails>();
				config.CreateMap<JobTypeViewModel,JobTypes>();
				config.CreateMap<JobTypes, JobTypeViewModel>();
				config.CreateMap<LocationViewModel, Locations>();
				config.CreateMap<Locations, LocationViewModel>();
				config.CreateMap<WorkExperienceViewModel, WorkExperiences>();
				
				config.CreateMap<RecruiterProfileViewModel, RecruiterProfile>();
				config.CreateMap<SearcherSkillsViewModel, SearcherSkillSets>();
				config.CreateMap<RecruiterProfile, RecruiterProfileViewModel>();
				
				});
		}
	}
}