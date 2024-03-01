using ExchangeService.Application.Domains.Abstractions.Entities.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExchangeService.Application.Domains.Converters
{
    public class OperationTypeConverter : JsonConverter<OperationType>
    {
        public override OperationType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    int value = 0;
                    if (int.TryParse(reader.GetString(), out value))
                    {
                        if (Enum.IsDefined(typeof(OperationType), value))
                        {
                            return (OperationType)value;
                        }
                    }
                    break;

                case JsonTokenType.Number:
                    if (Enum.IsDefined(typeof(OperationType), reader.GetInt32()))
                    {
                        return (OperationType)reader.GetInt32();
                    }
                    break;

                default:
                    throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, OperationType value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)value);
        }
    }
}
