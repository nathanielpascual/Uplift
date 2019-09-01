using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
	public class WebImage
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public byte[] Image { get; set; }
	}
}
