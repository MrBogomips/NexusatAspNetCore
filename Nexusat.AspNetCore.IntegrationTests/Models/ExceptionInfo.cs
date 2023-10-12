using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexusat.AspNetCore.IntegrationTests.Models;

public class ExceptionInfo
{
	public string Type { get; set; }
	public string Message { get; set; }
	public IEnumerable<string> StackTrace { get; set; }

	public override bool Equals(object obj) => Equals(obj as ExceptionInfo);

	public bool Equals(ExceptionInfo obj) {
		return 
			obj != null &&
			Type == obj.Type &&
			Message == obj.Message &&
			Enumerable.SequenceEqual(StackTrace, obj.StackTrace);
	}

	public override int GetHashCode()
	{
		throw new NotImplementedException();
	}
}