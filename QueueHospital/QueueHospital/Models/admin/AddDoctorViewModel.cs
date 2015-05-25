using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class AddDoctorViewModel
	{
		public IEnumerable<Speciality> Specialities { get; set; }

		public IEnumerable<Doctor> Doctors { get; set; }

		public string DoctorEmail { get; set; }
		public string DoctorPassword { get; set; }

		public string DoctorLastName { get; set; }
		public string DoctorFirstName { get; set; }
		public string DoctorMiddleName { get; set; }
		public int Speciality { get; set; }
	}
}