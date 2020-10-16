using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobPortal.Common
{
	public class ExceptionHandlerAttribute : FilterAttribute,IExceptionFilter
	{
		public  void OnException(ExceptionContext filterContext)
		{

			if (!filterContext.ExceptionHandled)
			{
				try
				{

					ExceptionLogger(filterContext);

				}
				catch (FileNotFoundException filexception)  //If file not exits
				{
					FileStream f = new FileStream("C:\\Users\\ILAKIYA\\OneDrive\\Desktop\\OnlineJobPortal\\ExceptionLogger\\Writer.txt", FileMode.Create);
					ExceptionLogger(filterContext);
				}

				}
		}
	  void ExceptionLogger(ExceptionContext filterContext)
		{
			StreamWriter objectStream = new StreamWriter(@"C:\Users\ILAKIYA\OneDrive\Desktop\OnlineJobPortal\ExceptionLogger\Writer.txt", true); //Read file path
			objectStream.WriteLine();
			objectStream.WriteLine("Log Written Date:" + " " + DateTime.Now.ToString()); //Writing input in file
			objectStream.WriteLine("Message:" + filterContext.Exception.Message); //Writing input in file
			objectStream.WriteLine("StakeTrace:" + filterContext.Exception.StackTrace); //Writing input in file
			objectStream.WriteLine("ControllerName:" + filterContext.RouteData.Values["controller"].ToString());//Logging controller name
			objectStream.Close();   //For closing file
		} 
}
}
