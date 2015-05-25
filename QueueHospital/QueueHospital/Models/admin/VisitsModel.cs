using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class VisitsModel
	{
		public IEnumerable<Visit> Visits { get; set; }
	}
}