using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Visit
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		public int DoctorId { get; set; }

		public Doctor Doctor { get; set; }

		[Required]
		public string PatientLastName { get; set; }

		[Required]
		public string PatientFirstName { get; set; }

		public string PatientMiddleName { get; set; }

		[Required]
		public string PatientPhoneNumber { get; set; }

		[Required]
		public string Snils { get; set; }

		[Required]
		public DateTime VisitDateTime { get; set; }
	}
}
