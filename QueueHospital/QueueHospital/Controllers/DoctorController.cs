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
	[Authorize( Roles = "Doctor" )]
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index( )
        {
			var vm = new DoctorsModel();
			using ( var context = new ApplicationDbContext() )
			{
				var user = context.Users.FirstOrDefault( x => x.UserName == User.Identity.Name );
				var doc = context.Doctors.FirstOrDefault(x=>x.User.Email == user.Email);
				var visits = context.Visits.Where( x => x.DoctorId == doc.Id ).ToList().OrderBy(visit => visit.VisitDateTime);
				vm.Doctor = doc;
				vm.Visits = visits;
			}
            return View(vm);
        }
    }
}