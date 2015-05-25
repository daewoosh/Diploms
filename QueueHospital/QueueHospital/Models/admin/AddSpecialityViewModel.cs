using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class AddSpecialityViewModel
	{
		public IEnumerable<Speciality> Specialities {get; set;}
	}
}