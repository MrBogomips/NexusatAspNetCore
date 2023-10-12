using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Models;

public class HelloWorldRequest: IValidatableObject
{
	[Required(ErrorMessage = "Name is required")]
	[MinLength(3, ErrorMessage = "Name Too short")]
	public string Name { get; set; }
        
	[Required]
	[MinLength(3, ErrorMessage = "Surname Too short")]
	public string Surname { get; set; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (Name == "Giovanni")
		{
			yield return new ValidationResult("No Giovanni! You cannot use me", new[] { nameof(Name) });
		}
		if (Surname == "Costagliola")
		{
			yield return new ValidationResult("No Costagliola! You cannot use me", new[] { nameof(Name) });
		}

		if (Name == "Giovanni" && Surname == "Costa")
		{
			yield return new ValidationResult("This is a bad person", new[] { nameof(Name), nameof(Surname) });
		}
	}
}