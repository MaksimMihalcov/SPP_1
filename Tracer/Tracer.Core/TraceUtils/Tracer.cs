using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tracer.Core.Interfaces;
using Tracer.Serialization.Abstractions.Structures;

namespace Tracer.Core
{
    public class CTracer : ITracer
    {
        private TraceResult Result { get; set; } 
        private Stack<Stopwatch> Watchs { get; set; }
        private object locker = new();

        public CTracer()
        {
            Watchs = new Stack<Stopwatch>();
            Result = new TraceResult();
            Result.Threads = new List<ThreadItem>();
        }

        public TraceResult GetTraceResult()
        {
            foreach(var thread in Result.Threads)
                foreach (var method in thread.Methods)
                    thread.TotalWorkTime += method.WorkTime;
            return Result;
        }

        public void StartTrace()
        {
            lock (locker)
            {
                var thread = Result.Threads.FirstOrDefault(x => x.ThreadId == AppDomain.GetCurrentThreadId());
                var stackTrace = new StackTrace();
                var methodInfo = stackTrace.GetFrame(1).GetMethod();
                var method = new Method()
                {
                    MethodName = methodInfo.Name,
                    ClassName = methodInfo.DeclaringType.Name,
                    InnerMethods = new List<Method>()
                };
                if (thread == null)
                {
                    thread = new ThreadItem();
                    thread.ThreadId = AppDomain.GetCurrentThreadId();
                    thread.Methods = new List<Method>();
                    thread.Methods.Add(method);
                    Result.Threads.Add(thread);
                }
                else if (Watchs.Count > 0)
                {
                    try
                    {
                        var topMetodInfo = stackTrace.GetFrame(2).GetMethod();
                        var topMethod = thread.Methods.FirstOrDefault(x => x.MethodName == topMetodInfo.Name && x.ClassName == topMetodInfo.DeclaringType.Name);
                        topMethod.InnerMethods.Add(method);

                    }
                    catch { }
                }
                else
                {
                    thread.Methods.Add(method);
                }
                var watch = new Stopwatch();
                Watchs.Push(watch);
                watch.Start();
            }
        }

        private Method GetMethod(List<Method> methods, string mname, string cname)
        {
            var method = methods.FirstOrDefault(x => x.MethodName == mname && x.ClassName == cname);
            if (method == null)
                for(int i = 0; i < methods.Count; i++)
                    return GetMethod(methods[i].InnerMethods, mname, cname);
            return method;
        }

        public void StopTrace()
        {
            lock(locker)
            {
                var stw = Watchs.Pop();
                stw.Stop();
                var thread = Result.Threads.FirstOrDefault(x => x.ThreadId == AppDomain.GetCurrentThreadId());
                var methodInfo = new StackTrace().GetFrame(1).GetMethod();
                var method = GetMethod(thread.Methods, methodInfo.Name, methodInfo.DeclaringType.Name);
                method.WorkTime = stw.ElapsedMilliseconds;
            }
        }
    }
}
