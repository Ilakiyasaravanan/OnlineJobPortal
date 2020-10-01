using System.Web;
using System.Web.Mvc;
using JobPortal.Common;
namespace OnlineJobPortal
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new ExceptionHandlerAttribute());
		}
	}
}
