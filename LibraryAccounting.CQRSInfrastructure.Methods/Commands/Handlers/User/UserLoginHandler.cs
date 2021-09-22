using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.Mailing;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, string>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserLoginHandler> _logger;
        private readonly IMessageSending _emailService;

        public UserLoginHandler(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<UserLoginHandler> logger,
            IMessageSending emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                        await _userManager.AddClaimAsync(user, new Claim("roleId", user.RoleId.ToString()));
                        await _userManager.UpdateAsync(user);
                        _logger.LogInformation("Succeeded login");
                        return "Вход успешно осуществлён";
                    }
                    else
                    {
                        _logger.LogInformation("Password is not correct");
                        return "Неверный пароль";
                    }
                }
                else
                {
                    await request.Page.SendMessage(user, _userManager, _emailService);
                    _logger.LogInformation("A letter has been sent to the user's mail");
                    return "Вы не подтвердили свой email. " +
                            "Проверьте свою почту и перейдите по ссылке, чтобы подтвердить почту";
                }
            }
            else
            {
                _logger.LogInformation("This postal address is not registered");
                return "Данный почтовый адрес не зарегистрирован";
            }
        }
    }
}

