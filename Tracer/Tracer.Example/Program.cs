using System;
using System.Threading;
using Tracer.Core.Interfaces;
using Tracer.Core;
using System.Reflection;
using Tracer.Serialization.Abstractions.Structures;

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
            Thread.Sleep(1270);
            _bar.InnerMethod();
            _tracer.StopTrace();
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
        }
    }

    public class Program
    {
        public static void SaveResult(string type, string path, TraceResult result)
        {
            var serAsm = Assembly.LoadFrom($@"D:\THIS.PROJECT\SPP .NET\Laba1\Tracer.Serialization\Tracer.Serialization.{type}\bin\Debug\net5.0\Tracer.Serialization.{type}.dll");
            var serType = serAsm.GetType($"Tracer.Serialization.{type}.{type}TraceSerializer");
            var serMet = serType.GetMethod("Serialize");
            var constructor = serType.GetConstructor(Type.EmptyTypes);
            var serObject = constructor.Invoke(Array.Empty<object>());
            serMet.Invoke(serObject, new object[] { result, path });
        }

        static void Main(string[] args)
        {
            var tracer = new CTracer();
            var q = new Foo(tracer);
            q.MyMethod();
            var result = tracer.GetTraceResult();
            SaveResult("Json", @"D:\json.txt", result);
            SaveResult("Xml", @"D:\xml.txt", result);
            SaveResult("Yaml", @"D:\yaml.txt", result);
        }
    }
}
