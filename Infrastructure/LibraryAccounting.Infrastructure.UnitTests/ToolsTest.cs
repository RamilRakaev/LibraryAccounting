using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class ToolsTest
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly RoleManager<ApplicationUserRole> _roleManager;
        private readonly IAdminTools _adminTools;
        private readonly ILibrarianTools _librarianTools;

        public ToolsTest(IAdminTools adminTools,
            ILibrarianTools librarianTools,
            IRepository<Book> bookRepository,
            RoleManager<ApplicationUserRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IRepository<Booking> bookingRepository)
        {
            _adminTools = adminTools;
            _librarianTools = librarianTools;
            _roleManager = roleManager;
            _userManager = userManager;
            _bookRepository = bookRepository;
            _bookingRepository = bookingRepository;
        }

        [TestMethod]
        public void LibrarianToolsTest()
        {
           
        }

        [TestMethod]
        public void AdminToolsTest()
        {

        }
    }
}
