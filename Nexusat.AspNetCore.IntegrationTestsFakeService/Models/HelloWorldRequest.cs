using System;
using System.ComponentModel.DataAnnotations;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Models
{
    public class HelloWorldRequest
    {
		[Required(ErrorMessage = "Name is required")]
		[MinLength(3, ErrorMessage = "Name Too short")]
		public string Name { get; set; }
        
		[Required]
		[MinLength(3, ErrorMessage = "Surname Too short")]
		public string Surname { get; set; }
    }
}
