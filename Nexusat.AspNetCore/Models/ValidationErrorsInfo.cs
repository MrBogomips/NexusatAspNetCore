using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
        public sealed class ValidationErrorsInfo : Dictionary<string, List<string>>
        {
		private NamingStrategy JsonNamingStrategy { get; }      
		private ModelStateDictionary ModelState { get; }

		public const string DefaultPropertyName = "Error";

		public ValidationErrorsInfo(ActionContext actionContext, ModelStateDictionary modelState) {
			if (actionContext == null) throw new ArgumentNullException(nameof(actionContext));
			ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));

			var jsonOptionsAccessor = actionContext.HttpContext.RequestServices.GetRequiredService<IOptions<MvcJsonOptions>>();         
			         
			var contractResolver = jsonOptionsAccessor.Value.SerializerSettings.ContractResolver as DefaultContractResolver;
			if (contractResolver != null) {
				JsonNamingStrategy = contractResolver.NamingStrategy;
			}
			ParseModelState();
        }

		public ValidationErrorsInfo(NamingStrategy namingStrategy, ModelStateDictionary modelState) {
			JsonNamingStrategy = namingStrategy ?? throw new ArgumentException(nameof(namingStrategy));
			ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));
			ParseModelState();
		}

		private string SerializeToJsonConformantName(string input)
		{
			string _input = input;
			if (string.IsNullOrWhiteSpace(_input)) _input = DefaultPropertyName;
			return JsonNamingStrategy?.GetPropertyName(_input, false) ?? _input;
		}
		private void ParseModelState()
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
}
