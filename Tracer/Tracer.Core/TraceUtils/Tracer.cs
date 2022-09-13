using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tracer.Core.Interfaces;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Core
{
    public class CTracer : ITracer
    {
        private TraceResult Result { get; set; } 
        private Stopwatch Watch { get; set; }

        public CTracer()
        {
            Watch = new Stopwatch();
            Result = new TraceResult();
        }

        public TraceResult GetTraceResult()
        {
            Result.WorkTime = Watch.ElapsedMilliseconds;
            var method = new StackTrace().GetFrame(1).GetMethod();
            Result.MethodName = method.Name;
            Result.ClassName = method.DeclaringType.Name;
            return Result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void StartTrace()
        {
            Watch.Reset();
            Watch.Start();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void StopTrace()
        {
            Watch.Stop();
        }
    }
}
