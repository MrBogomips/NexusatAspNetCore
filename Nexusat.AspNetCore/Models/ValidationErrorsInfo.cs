using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Nexusat.AspNetCore.Models;

public sealed class ValidationErrorsInfo : Dictionary<string, List<string>>
{
	JsonNamingPolicy JsonNamingPolicy { get; }      
	ModelStateDictionary ModelState { get; }

	public const string DefaultPropertyName = "Error";

	public ValidationErrorsInfo(ActionContext actionContext, ModelStateDictionary modelState) {
		if (actionContext == null) throw new ArgumentNullException(nameof(actionContext));
		ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));

		var jsonOptionsAccessor = actionContext.HttpContext.RequestServices.GetRequiredService<IOptions<JsonSerializerOptions>>();
		if (jsonOptionsAccessor != null) {
            JsonNamingPolicy = jsonOptionsAccessor.Value.PropertyNamingPolicy;
		}
		ParseModelState();
	}

	public ValidationErrorsInfo(JsonNamingPolicy namingStrategy, ModelStateDictionary modelState) {
        JsonNamingPolicy = namingStrategy ?? throw new ArgumentException(nameof(namingStrategy));
		ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));
		ParseModelState();
	}

	string SerializeToJsonConformantName(string input)
	{
		string _input = input;
		if (string.IsNullOrWhiteSpace(_input)) _input = DefaultPropertyName;
		return JsonNamingPolicy?.ConvertName(_input) ?? _input;
	}
	void ParseModelState()
	{
		foreach (var item in ModelState)
		{
			var k = SerializeToJsonConformantName(item.Key);
			var errors = item.Value.Errors.Select(e => e.ErrorMessage);
			if (!this.ContainsKey(k))
			{
				Add(k, new List<string>());
			}
			this[k].AddRange(errors);
		}
	}
}