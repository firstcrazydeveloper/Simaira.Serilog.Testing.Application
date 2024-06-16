using Serilog.Events;

namespace Serilog.WebAPI.Testing.Application
{
    public class Serilog
    {
        public string? Name { get; set; }

        public IEnumerable<string> Using { get; set; }

        public LogEventLevel MinimumLevel { get; set; }
        public IEnumerable<WriteTo> WriteTo { get; set; }

        public IEnumerable<string> Enrich { get; set; }

        public Properties? Properties { get; set; }
    }

    public class Properties
    {
        public string? Application { get; set; }
    }

    public class WriteTo
    {
        public string? Name { get; set; }
        public LogArgs Args { get; set; }
    }

    public class LogArgs
    {
        public string? Path { get; set; }
    }
}
