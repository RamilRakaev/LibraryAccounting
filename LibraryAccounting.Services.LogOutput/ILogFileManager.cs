using System;
using System.Threading.Tasks;

namespace LibraryAccounting.Services.LogOutput
{
    public interface ILogFileManager
    {
        Task<Log[]> GetLogsAsync(string date);
        Task<Log[]> GetLogsFromServiceAsync(string date, string serviceName);
        Task<string[]> GetDatesAsync();
        Task<string[]> GetServicesAsync();

        public string ErrorMessage { get; }
        public bool Successed { get; }
    }
}
