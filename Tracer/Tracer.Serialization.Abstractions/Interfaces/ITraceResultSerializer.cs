using System.IO;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Serialization.Abstractions.Interfaces
{
    public interface ITraceResultSerializer
    {
        string Format { get; }
        void Serialize(TraceResult traceResult, string filePath);
    }
}
