using System;
using System.IO;
using Tracer.Serialization.Abstractions.Interfaces;
using Tracer.Serialization.Abstractions.Structures;
using YamlDotNet.Serialization;

namespace Tracer.Serialization.Yaml
{
    public class YamlTraceSerializer : ITraceResultSerializer
    {
        public string Format { get { return "YAML"; } }

        public static void Serialize(TraceResult traceResult, string filePath)
        {
            var serializer = new SerializerBuilder().Build();
            File.WriteAllText(filePath, serializer.Serialize(traceResult));
        }
    }
}
