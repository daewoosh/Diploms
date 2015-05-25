using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class TimeTable
	{
		[Key]
		[Required]
		public int Id {get;set;}

		[Required]
		public int DoctorId { get; set; }

		public Doctor Doctor { get; set; }

		[Required]
		public int StartHour { get; set; }

		[Required]
		public int EndHour { get; set; }

		[Required]
		public DayOfWeek WeekDay { get; set; }

	}
}
