using DAL.Entities;
using QueueHospital.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class AddVisitViewModel
	{
		public IEnumerable<Speciality> Specialities { get; set; }

		public IEnumerable<Doctor> Doctors { get; set; }
	}
}