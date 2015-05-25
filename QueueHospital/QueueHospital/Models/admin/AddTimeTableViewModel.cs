using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class AddTimeTableViewModel
	{
		public IEnumerable<Doctor> Doctors { get; set; }
		public IEnumerable<TimeTable> TimeTables { get; set; }

		public string GetTimeString( int docID, DayOfWeek dayOfWeek )
		{
			var time = TimeTables.FirstOrDefault( x => x.Doctor.Id == docID && x.WeekDay == dayOfWeek );
			if ( time != null )
				return String.Format("{0} - {1}", time.StartHour, time.EndHour);
			return String.Empty;
		}
	}
}