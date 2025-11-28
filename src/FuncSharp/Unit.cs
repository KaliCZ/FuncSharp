using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FuncSharp;

/// <summary>
/// The Unit type (product of zero types). It has only one instance.
/// </summary>
[JsonConverter(typeof(UnitJsonConverter))]
public sealed class Unit
{
    private Unit()
    {
    }

    /// <summary>
    /// The only instance of the Unit type.
    /// </summary>
    public static Unit Value { get; } = new Unit();

    public override int GetHashCode()
    {
        return 42;
    }
    public override bool Equals(object? obj)
    {
        return this == obj;
    }
    public override string ToString()
    {
        return "()";
    }
}

public sealed class UnitJsonConverter : JsonConverter<Unit>
{
    public override Unit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Read();
        return Unit.Value;
    }

    public override void Write(Utf8JsonWriter writer, Unit value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteEndObject();
    }
}