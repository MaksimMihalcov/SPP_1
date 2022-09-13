using System.Runtime.CompilerServices;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Core.Interfaces
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}
