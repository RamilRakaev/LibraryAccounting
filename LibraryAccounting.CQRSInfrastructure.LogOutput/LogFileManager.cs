using LibraryAccounting.Services.LogOutput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.LogOutput
{
    public class LogFileManager : ILogFileManager
    {
        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\logs";
        private readonly string _logOutput = "nlog-all";
        private readonly string _fileExtension = ".log";
        private readonly string[] _logFiles;

        public string ErrorMessage { get; private set; }
        public bool Successed { get; private set; } = true;

        public LogFileManager(string path, string logOutput, string fileExtension)
        {
            _path = path;
            _logOutput = logOutput;
            _fileExtension = fileExtension;
        }

        public LogFileManager()
        {
            _logFiles = Directory.GetFiles(_path);
        }

        private List<Log> GetLogsAsList(string date)
        {
            var file = _logFiles.FirstOrDefault(f => f.Contains(date));
            List<Log> logs = new List<Log>();
            try
            {
                if (file != null)
                {
                    var logsFromFile = File
                        .ReadAllText(file)
                        .Split(date, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var log in logsFromFile)
                    {
                        var message = log.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        logs.Add(new FileLog()
                        {
                            Date = date,
                            LogLevel = message[^3],
                            ServiceName = message[^2],
                            Message = message[^1]
                        });
                    }
                }
                Successed = true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                Successed = false;
            }
            return logs;
        }

        private string[] GetDates()
        {
            List<string> dates = new List<string>();
            try
            {
                foreach (var log in _logFiles.Where(l => l.Contains(_logOutput)))
                {
                    dates.Add(log[
                        (log.IndexOf(_logOutput) + _logOutput.Length + 1)..log.IndexOf(_fileExtension)]);
                }
                Successed = true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                Successed = false;
            }
            return dates.ToArray();
        }

        public async Task<Log[]> GetLogsAsync(string date)
        {
            return await Task
                .FromResult(GetLogsAsList(date)
                .ToArray());
        }

        public async Task<Log[]> GetLogsFromServiceAsync(string date, string serviceName)
        {
            return await Task
                .FromResult(GetLogsAsList(date)
                .Where(l => l.ServiceName == serviceName)
                .ToArray());
        }

        public async Task<string[]> GetDatesAsync()
        {
            return await Task.FromResult(GetDates());
        }
    }
}
