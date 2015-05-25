using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QueueHospital.DAL;
using QueueHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QueueHospital.Controllers
{
	[Authorize( Roles = "Admin" )]
	public class AdminController : Controller
	{

		#region ctor
		public AdminController()
			: this( new UserManager<ApplicationUser>( new UserStore<ApplicationUser>( new ApplicationDbContext() ) ) )
		{
		}

		public AdminController( UserManager<ApplicationUser> userManager )
		{
			UserManager = userManager;
		}
		#endregion

		public UserManager<ApplicationUser> UserManager { get; private set; }

		#region Index
		// GET: Admin
		public ActionResult Index()
		{
			return View();
		}
		#endregion

		#region специальности
		public ActionResult AddSpeciality()
		{
			var vm = new AddSpecialityViewModel();
			using ( var context = new ApplicationDbContext() )
			{
				vm.Specialities = context.Specialities.ToList();
			}
			return View( vm );
		}

		public ActionResult DeleteSpeciality( int Id )
		{
			using ( var context = new ApplicationDbContext() )
			{
				var spec = context.Specialities.FirstOrDefault( x => x.Id == Id );
				context.Specialities.Remove( spec );
				context.SaveChanges();
			}
			return RedirectToAction( "AddSpeciality" );
		}

		[HttpPost]
		public ActionResult AddSpeciality( string SpecialityName )
		{
			using ( var context = new ApplicationDbContext() )
			{
				context.Specialities.Add( new Speciality { Name = SpecialityName } );
				context.SaveChanges();
			}
			return RedirectToAction( "AddSpeciality" );
		}
		#endregion

		#region доктора
		public ActionResult AddDoctor()
		{
			var vm = new AddDoctorViewModel();
			using ( var context = new ApplicationDbContext() )
			{
				vm.Specialities = context.Specialities.ToList();
				vm.Doctors = context.Doctors.ToList();
			}
			return View( vm );
		}

		[HttpPost]
		public ActionResult AddDoctor( AddDoctorViewModel model )
		{
			using ( var context = new ApplicationDbContext() )
			{
				var user = context.Users.Create();
				user.UserName = model.DoctorEmail;
				user.Email = model.DoctorEmail;
				user.PasswordHash = UserManager.PasswordHasher.HashPassword( model.DoctorPassword );

				context.Doctors.Add(
					new Doctor
					{
						User = user,
						LastName = model.DoctorLastName,
						FirstName = model.DoctorFirstName,
						MiddleName = model.DoctorMiddleName,
						SpecialityId = model.Speciality
					} );
				context.SaveChanges();
				UserManager.UpdateSecurityStamp( user.Id );
				UserManager.AddToRole( user.Id, "Doctor" );
			}

			return RedirectToAction( "AddDoctor" );
		}

		public ActionResult DeleteDoctor( int Id )
		{
			using ( var context = new ApplicationDbContext() )
			{
				var doc = context.Doctors.FirstOrDefault( x => x.Id == Id );
				var user = doc.User;
				context.Users.Remove( user );
				context.Doctors.Remove( doc );
				context.SaveChanges();
			}
			return RedirectToAction( "AddDoctor" );
		}
		#endregion

		#region TimeTable

		public ActionResult AddTimeTable()
		{
			var vm = new AddTimeTableViewModel();
			using ( var context = new ApplicationDbContext() )
			{
				vm.Doctors = context.Doctors.ToList();
				vm.TimeTables = context.TimeTables.ToList();
			}
			return View( vm );
		}

		[HttpPost]
		public ActionResult AddTimeTable( int Doctor, int dayOfWeek, int StartTime, int EndTime )
		{

			using ( var context = new ApplicationDbContext() )
			{
				var doctor = context.Doctors.FirstOrDefault( x => x.Id == Doctor );
				var exisitingTimeTable = context.TimeTables.FirstOrDefault( x => x.DoctorId == Doctor && x.WeekDay == (DayOfWeek)dayOfWeek );
				if ( exisitingTimeTable != null )
					context.TimeTables.Remove( exisitingTimeTable );
				TimeTable newTimeTable = new TimeTable { Doctor = doctor, StartHour = StartTime, EndHour = EndTime, WeekDay = (DayOfWeek)dayOfWeek };
				context.TimeTables.Add( newTimeTable );
				context.SaveChanges();
			}
			return RedirectToAction( "AddTimeTable" );
		}
		#endregion

		#region посещения
		public ActionResult Visits()
		{
			var vm = new VisitsModel();
			using ( var context = new ApplicationDbContext() )
			{
				var visits = context.Visits.Include( "Doctor" ).ToList();
				vm.Visits = visits;
			}
			return View( vm );
		}

		public ActionResult DeleteVisit( int Id )
		{
			using ( var context = new ApplicationDbContext() )
			{
				var visit = context.Visits.FirstOrDefault( x => x.Id == Id );
				context.Visits.Remove( visit );
				context.SaveChanges();
			}
			return RedirectToAction( "Visits" );
		}
		#endregion
	}
}