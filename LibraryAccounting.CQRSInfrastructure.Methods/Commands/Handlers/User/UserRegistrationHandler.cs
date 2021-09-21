﻿using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.Mailing;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers.User
{
    public class UserRegistrationHandler : IRequestHandler<UserRegistrationCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRegistrationHandler> _logger;
        private readonly IMessageSending _emailService;

        public UserRegistrationHandler(ILogger<UserRegistrationHandler> logger,
            UserManager<ApplicationUser> userManager,
            IMessageSending emailService)
        {
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<string> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                RoleId = request.RoleId
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Sending message for user");
                await request.Page.SendMessage(user, _userManager, _emailService);
                return "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме";
            }
            else
            {
                string errorMessages = "";
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"{error.Description}");
                    errorMessages += error.Description + "\n";
                }
                return errorMessages;
            }
        }
    }
}
