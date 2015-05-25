using DAL.Entities;
using QueueHospital.DAL;
using QueueHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueHospital.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			//в зависимости от роли пользователя перенаправляем его в нужную панель
			if ( User.IsInRole( "Admin" ) )
				return RedirectToAction( "Index", "Admin" );
			else
				if ( User.IsInRole( "Doctor" ) )
					return RedirectToAction( "Index", "Doctor" );
				else
					return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult SpecialitiesList()
		{
			//список специальностей получаем асинхронно и возвращаем Json
			List<Speciality> specialiies;
			using ( var context = new ApplicationDbContext() )
			{
				specialiies = context.Specialities.ToList();
			}
			if ( HttpContext.Request.IsAjaxRequest() )
			{
				return Json( new SelectList(
							specialiies,
							"Id",
							"Name" ), JsonRequestBehavior.AllowGet
							);
			}
			return View( specialiies );
		}

		public ActionResult DoctorList( int SpecialityId )
		{
			List<Doctor> doctors;

			//отбираем докторов выбранной нами специальности
			using ( var context = new ApplicationDbContext() )
			{
				doctors = context.Doctors.Where( x => x.SpecialityId == SpecialityId ).ToList();
			}

			if ( HttpContext.Request.IsAjaxRequest() )
				return Json( new SelectList(
								doctors,
								"Id",
								"FullName" ), JsonRequestBehavior.AllowGet
							);

			return View( doctors );
		}

		/// <summary>
		/// Формируем список доступных времен для выбранного доктора и дня недели
		/// </summary>
		/// <param name="DoctorId">ИД доктора</param>
		/// <param name="dateTimeString">Выбранная в календаре дата</param>
		/// <returns>PartialView с сформированными временами</returns>
		public ActionResult TimeTableList( int DoctorId, string dateTimeString )
		{
			//преобразовали выбранную дату в рабочий формат
			var currentDate = DateTime.Parse( dateTimeString );

			List<string> times = new List<string>();
			TimeTable timeTable;
			List<Visit> doctorsVisits;
			List<string> bookedTimes = new List<string>();
			using ( var context = new ApplicationDbContext() )
			{

				timeTable = context.TimeTables.FirstOrDefault( x => x.DoctorId == DoctorId && x.WeekDay == currentDate.DayOfWeek );
				//LINQ не работает со многими свойствами и функциями DateTime, поэтому чтобы выбрать все посещения на выбранную дату
				//приходится проводить такое явное сравнение. 
				doctorsVisits = context.Visits.Where( x => x.DoctorId == DoctorId &&
					x.VisitDateTime.Year == currentDate.Year &&
					x.VisitDateTime.Month == currentDate.Month &&
					x.VisitDateTime.Day == currentDate.Day ).ToList();
			}
			foreach ( var visit in doctorsVisits )
			{
				bookedTimes.Add( visit.VisitDateTime.ToShortTimeString() );
			}
			var i = timeTable.StartHour;
			double minutes = 0;
			while ( i <= timeTable.EndHour - 1 )
			{
				if ( minutes >= 60 )
				{
					minutes = 0;
					i++;
				}
				var t = ( i + minutes / 100 ).ToString();
				string[] split = t.Split( new Char[] { ',', '.' } );
				string finalRez;
				if ( split.Length > 1 )
					finalRez = String.Format( "{0:00}:{1:00}", split[0], split[1].PadRight( 2, '0' ) );
				else
					finalRez = String.Format( "{0:00}:00", split[0] );
				times.Add( finalRez );
				minutes = minutes + 15;

			}
			var vm = new TImeTableModel();
			vm.Times = times;
			vm.BlockedTimes = bookedTimes;
			if ( HttpContext.Request.IsAjaxRequest() )
				return PartialView( "_TimeTablePartial", vm );

			return View( times );
		}

		/// <summary>
		/// Сохраняем посещение в бд
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Index( IndexViewModel model )
		{

			Visit visit = new Visit
			{
				PatientFirstName = model.PatientFirstName,
				PatientLastName = model.PatientLastName,
				PatientMiddleName = model.PatientMiddleName,
				PatientPhoneNumber = model.PatientPhoneNumber,
				Snils = model.PatientSnils
			};

			using ( var context = new ApplicationDbContext() )
			{
				var doc = context.Doctors.FirstOrDefault( x => x.Id == model.Doctors );
				visit.Doctor = doc;
				visit.VisitDateTime = GenerateDateTime( model.Date, model.Time );
				context.Visits.Add( visit );
				context.SaveChanges();
			}

			var result = new ResultModel { Visit = visit };

			return Result(result );
		}


		public ActionResult Result( ResultModel result )
		{
			return View( "Result",result );
		}
		/// <summary>
		/// генерируем DateTime на основе выбранного времени и даты
		/// </summary>
		/// <param name="date"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		private DateTime GenerateDateTime( string date, string time )
		{
			IFormatProvider culture = new System.Globalization.CultureInfo( "ru-RU", true );
			var NewDate = DateTime.Parse( date + " " + time, culture );
			return NewDate;
		}
	}
}