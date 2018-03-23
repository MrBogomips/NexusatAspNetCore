using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nesxusat.AspNetCore.Models
{
        public sealed class ValidationErrorsInfo : Dictionary<string, List<string>>
        {
            private readonly NamingStrategy JsonNamingStrategy;

#if EXCLUDE
            public ValidationErrorsInfo()
            {
                var jsonContractResolver = Globals.JsonSerializerSettings.ContractResolver as DefaultContractResolver;
                if (jsonContractResolver != null)
                {
                    JsonNamingStrategy = jsonContractResolver.NamingStrategy;
                }
            }
            /// <summary>
            /// Add a violation that should refer to keys of the JSON message used for the request
            /// </summary>
            /// <param name="violation">A description of the violation</param>
            /// <param name="jsonKeys">The list of keys where this violation occurs</param>
            public void AddJsonRequestViolation(string violation, params string[] jsonKeys)
            {
                foreach (var k in jsonKeys)
                {
                    var k1 = SerializeToJsonConformantName(k);
                    this[k1].Add(violation);
                }
            }
            /// <summary>
            /// Helper method to parse a <see cref="ModelStateDictionary">ModelStateDictionary</see>.
            /// </summary>
            /// <param name="modelState"></param>
            public void ParseModelState(ModelStateDictionary modelState)
            {
                foreach (var item in modelState)
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

            private string SerializeToJsonConformantName(string input)
                => JsonNamingStrategy?.GetPropertyName(input, false) ?? input;
#endif
        }
}
