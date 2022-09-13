using System.IO;
using System.Text.Json;
using Tracer.Serialization.Abstractions.Interfaces;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Serialization.Json
{
    public class JsonTraceSerializer : ITraceResultSerializer
    {
        public string Format { get { return "JSON"; } }

        public void Serialize(TraceResult traceResult, string filePath)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(traceResult));
        }
    }
}
