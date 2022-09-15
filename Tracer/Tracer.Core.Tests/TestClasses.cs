using System.Threading;
using System.Threading.Tasks;
using Tracer.Core.Interfaces;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Core.Tests
{
    public class A
    {
        public ITracer Tracer { get; set; }
        public A(ITracer _tracer)
        {
            Tracer = _tracer;
        }
        public void A1()
        {
            Tracer.StartTrace();
            Thread.Sleep(1500);
            Tracer.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            return Tracer.GetTraceResult();
        }
    }

    public class B
    {
        public ITracer Tracer { get; set; }
        public B(ITracer _tracer)
        {
            Tracer = _tracer;
        }
        public async Task B1()
        {
            Thread.Sleep(1200);
        }

        public async void B2()
        {
            Tracer.StartTrace();
            Thread.Sleep(2000);
            await B1();
            Tracer.StopTrace();
        }

        public async void B3()
        {
            Tracer.StartTrace();
            Thread.Sleep(2000);
            B1();
            Tracer.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            return Tracer.GetTraceResult();
        }
    }
}
