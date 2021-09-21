using System;
using System.Threading.Tasks;

namespace LibraryAccounting.Services.LogOutput
{
    public interface ILogFileManager<Log> where Log : ILog
    {
        Task<Log[]> GetLogsAsync(string date);
        Task<Log[]> GetLogsFromServiseAsync(string date, string serviceName);
        Task<string[]> GetDatesAsync();
    }
}
