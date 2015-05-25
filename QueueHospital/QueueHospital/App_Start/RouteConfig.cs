using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QueueHospital
{
	public class RouteConfig
	{
		public static void RegisterRoutes( RouteCollection routes )
		{
			routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );


			routes.MapRoute(
				"DoctorsList",
				"Home/Doctors/List/{SpecialityId}",
				new { controller = "Home", action = "DoctorList", SpecialityId = "" }
			);

			routes.MapRoute(
				"TimeTableList",
				"Home/TimeTables/List/{DoctorId}/{Date}",
				new { controller = "Home", action = "TimeTableList", DoctorId = "", Date="" }
				);

			routes.MapRoute(
				"SpecialitiesList",
				"Home/Specialities/List",
				new { controller = "Home", action = "SpecialitiesList" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
