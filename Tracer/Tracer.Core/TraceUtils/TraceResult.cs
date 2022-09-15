using System.Collections.Generic;

namespace Tracer.Serialization.Abstractions.Structures
{
    public class TraceResult
    {
        public List<ThreadItem> Threads { get; set; }
    }

    public class ThreadItem
    {
        public long ThreadId { get; set; }
        public long TotalWorkTime { get; set; }
        public List<Method> Methods { get; set; }
    }

    public class Method
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public long WorkTime { get; set; }
        public List<Method> InnerMethods { get; set; }
    }
}
