using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryAccounting.Services.LogOutput;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.AdminPages
{
    public class LogsModel : PageModel
    {
        private readonly ILogger<LogsModel> _logger;
        public readonly ILogFileManager LogManager;
        public string[] Dates { get; private set; }
        public Log[] Logs { get; private set; }
        public string[] Types { get; private set; }

        public LogsModel(ILogger<LogsModel> logger, ILogFileManager log)
        {
            LogManager = log;
            _logger = logger;
        }

        public async Task OnGet()
        {
            Dates = await LogManager.GetDatesAsync();
            Types = await LogManager.GetServicesAsync();
            Logs = await LogManager.GetLogsAsync(Dates[0]);
        }

        public async Task OnPost(string date, string service)
        {
            Dates = await LogManager.GetDatesAsync();
            Types = await LogManager.GetServicesAsync();
            if (service == string.Empty)
            {
                Logs = await LogManager.GetLogsAsync(date);

            }
            else
            {
                Logs = await LogManager.GetLogsFromServiceAsync(date, service);
            }

            if (LogManager.Successed == false)
            {
                _logger.LogError(LogManager.ErrorMessage);
            }
        }
    }
}
