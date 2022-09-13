namespace Tracer.Serialization.Abstractions.Structures
{
    public class TraceResult
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public long WorkTime { get; set; }
    }
}
