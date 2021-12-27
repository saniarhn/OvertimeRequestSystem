
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.JSON
{


    public  class OvertimeDetaillToStringConverter : JsonConverter<List<OvertimeDetail>>
    {
        public override List<OvertimeDetail> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            // Let's check we are dealing with a proper array format([]) adhering to the JSON spec.
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                // Proper array, we can deserialize from this token onwards.
                return JsonSerializer.Deserialize<List<OvertimeDetail>>(ref reader, options);

            }

            // If we reached here, it means we are dealing with the JSON array in non proper array form
            // ie: using an object structure with "$type" and "$values" format like below 
            // We will go through each token and when we get the array type inside (for $values),
            // We will deserialize that token. We exit when we reaches the next end object.

            List<OvertimeDetail> list = null;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    list = JsonSerializer.Deserialize<List<OvertimeDetail>>(ref reader, options);
                }
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    // finished processing the array and reached the outer closing bracket token of wrapper object.
                    break;
                }
            }

            return list;
            /*     if (reader.TokenType != JsonTokenType.StartObject)
                 {
                     throw new JsonException("JSON payload expected to start with StartObject token.");
                 }

                 List<OvertimeDetail> list = null;
                 var startDepth = reader.CurrentDepth;

                 while (reader.Read())
                 {
                     if (reader.TokenType == JsonTokenType.EndObject && reader.CurrentDepth == startDepth)
                         return list;
                     if (reader.TokenType == JsonTokenType.StartArray)
                     {
                         if (list != null)
                             throw new JsonException("Multiple lists encountered.");
                         var eodPostions = JsonSerializer.Deserialize<OvertimeDetail[]>(ref reader, options);
                         (list = new List<OvertimeDetail>(eodPostions.Length)).AddRange(eodPostions);
                     }
                 }
                 throw new JsonException(); // Truncated file or internal error
            */
        }
        public override void Write(Utf8JsonWriter writer, List<OvertimeDetail> value, JsonSerializerOptions options)
        {
            // Nothing special to do in write operation. So use default serialize method.
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
