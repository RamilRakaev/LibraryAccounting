
namespace LibraryAccounting.Services.LogOutput
{
    public interface ILog
    {
        public string Date { get; set; }
        public string LogLevel { get; set; }
        public string ServiceName { get; set; }
        public string Message { get; set; }
    }
}
