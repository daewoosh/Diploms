using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Doctor
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		[Required]
		public int SpecialityId { get; set; }

		public Speciality Speciality { get; set; }

		
		public virtual ApplicationUser User{get;set;}

		public string FullName 
		{
			get 
			{
				return String.Format( "{0} {1} {2}", LastName, FirstName, MiddleName );
			}
		}
	}
}
