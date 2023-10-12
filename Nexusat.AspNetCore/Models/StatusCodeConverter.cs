using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonException = System.Text.Json.JsonException;

namespace Nexusat.AspNetCore.Models;

public class StatusCodeConverter : JsonConverter<StatusCode>
{
	public override StatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Deserialize as string
		var value = reader.GetString();
		if (value is null)
		{
			throw new JsonException("Expected a string");
		}

		return new(value);
	}

	public override void Write(Utf8JsonWriter writer, StatusCode value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.Code);
	}
}