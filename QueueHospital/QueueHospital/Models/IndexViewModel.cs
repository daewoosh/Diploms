using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class IndexViewModel
	{
		[Required]
		[Display( Name = "Фамилия пациента" )]
		public string PatientLastName { get; set; }
		[Required]
		[Display( Name = "Имя пациента" )]
		public string PatientFirstName { get; set; }

		public string PatientMiddleName { get; set; }

		[Required]
		[Display( Name = "СНИЛС пациента" )]
		public string PatientSnils { get; set; }

		[Required]
		[Display( Name = "Телефонный номер пациента")]
		public string PatientPhoneNumber { get; set; }
		public string Time { get; set; }
		public int Doctors { get; set; }
		public string Date { get; set; }
	}
}