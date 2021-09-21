using LibraryAccounting.Services.LogOutput;


namespace LibraryAccounting.CQRSInfrastructure.LogOutput
{
    public class Log : ILog
    {
        public string Date { get; set; }
        public string LogLevel { get; set; }
        public string ServiceName { get; set; }
        public string Message { get; set; }
    }
}
