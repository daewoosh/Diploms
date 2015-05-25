using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueHospital.Models
{
	public class TImeTableModel
	{
		public List<string> Times { get; set; }
		public List<string> BlockedTimes { get; set; }
	}
}