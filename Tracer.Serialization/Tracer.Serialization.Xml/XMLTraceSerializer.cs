using System;
using System.IO;
using System.Xml.Serialization;
using Tracer.Serialization.Abstractions.Interfaces;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Serialization.Xml
{
    public class XmlTraceSerializer : ITraceResultSerializer
    {
        public string Format { get { return "XML"; } }
        private static XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));

        public static void Serialize(TraceResult traceResult, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,traceResult);
            }
        }
    }
}