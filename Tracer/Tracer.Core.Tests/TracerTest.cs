using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracer.Core.Tests
{

    [TestClass]
    public class TracerTest
    {
        [TestMethod]
        public void Trace1()
        {
            var a = new A(new CTracer());
            a.A1();
            var result = a.GetTraceResult();
            Assert.AreSame(1500, result.Threads[0].TotalWorkTime);
        }

        [TestMethod]
        public void Trace2()
        {
            var b = new B(new CTracer());
            b.B2();
            var result = b.GetTraceResult();
            Assert.AreSame(3200, result.Threads[0].TotalWorkTime);
        }

        [TestMethod]
        public void Trace3()
        {
            var b = new B(new CTracer());
            b.B3();
            var result = b.GetTraceResult();
            Assert.AreSame(3200, result.Threads[0].TotalWorkTime);
        }
    }
}
