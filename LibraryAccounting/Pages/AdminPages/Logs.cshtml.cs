using System.Threading.Tasks;
using LibraryAccounting.Pages.ClientPages;
using LibraryAccounting.Services.LogOutput;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.AdminPages
{
    public class LogsModel : PageModel
    {
        private readonly ILogger<LogsModel> _logger;
        public readonly ILogFileManager LogManager;
        public new UserProperties User;
        public string[] Dates { get; private set; }
        public Log[] Logs { get; private set; }
        public string[] Types { get; private set; }

        public LogsModel(ILogger<LogsModel> logger, 
            ILogFileManager log,
            UserProperties userProperties)
        {
            LogManager = log;
            _logger = logger;
            User = userProperties;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.IsAuthenticated == false || User.RoleId != 3)
            {
                return RedirectToPage("/Account/Login");
            }
            Dates = await LogManager.GetDatesAsync();
            Types = await LogManager.GetServicesAsync();
            Logs = await LogManager.GetLogsAsync(Dates[0]);
            return Page();
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
