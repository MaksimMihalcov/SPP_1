using System;
using System.Threading;
using Tracer.Core.Interfaces;
using Tracer.Core;
using System.Reflection;

namespace Tracer.Example
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(3000);
            _bar.InnerMethod();
            _tracer.StopTrace();
            var res = _tracer.GetTraceResult();
            Console.WriteLine(res.MethodName + " " + res.WorkTime);
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(2000);
            _tracer.StopTrace();
            var res = _tracer.GetTraceResult();
            Console.WriteLine(res.MethodName + " " + res.WorkTime);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //Assembly.Load();
            var q = new Foo(new CTracer());
            q.MyMethod();
        }
    }
}
