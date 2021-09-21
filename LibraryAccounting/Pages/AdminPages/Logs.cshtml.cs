using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryAccounting.Pages.ClientPages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NLog;

namespace LibraryAccounting.Pages.AdminPages
{
    public class LogsModel : PageModel
    {

        public LogsModel(ILogger<LogsModel> logger)
        {

        }

        public async Task OnGet()
        {

        }

        public async Task OnPost(int id)
        {
            
        }
    }
}
