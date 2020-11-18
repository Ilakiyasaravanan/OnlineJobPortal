using JobPortal.BL;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc3;

namespace OnlineJobPortal.App_Start
{
	public class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			container.RegisterType<IJobMediator, JobMediator>();
			container.RegisterType<IAccountMediator, AccountMediator>();
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}