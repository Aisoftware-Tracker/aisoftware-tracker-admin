using Aisoftware.Tracker.Admin.Domain.Services;
using Aisoftware.Tracker.UseCases.Sessions.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Controllers;
public class AccountController : Controller
{
    private readonly ISessionUseCase _useCase;
    private readonly ITokenService _token;
    private string _cookieValue;
    private readonly ILogger _logger;
    private readonly ILogUtil _logUtil;
    private RouteData _context;

    public AccountController(ITokenService token, ISessionUseCase useCase, ILogger<AccountController> logger, ILogUtil logUtil)
    {
        _token = token;
        _useCase = useCase;
        _logger = logger;
        _logUtil = logUtil;
    }

    [AllowAnonymous]
    public IActionResult Login() => View();

    [AllowAnonymous]
    public async Task<ActionResult> Validate(Login login)
    {
        _context = this.ControllerContext.RouteData;

        try
        {
            var response = await _useCase.Create(login, _cookieValue);

            if (response is null)
                return NotFound(new { message = "Login ou senha inválido" });

            string cookieValue = _useCase.GetCookieValue();

            var token = _token.GenerateToken(response, cookieValue);

            if (token is not null)
                HttpContext.Session.SetString("Token", token);


            this.SetSessions(response);
            string message = "Login Realizado com Sucesso!";

            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), $"{message} -> {login.Email}"));
            return Json(new { status = true, message = message });

        }
        catch (Exception e)
        {
            string message = e.Message == "Unauthorized" ?
            "Login ou senha inválido" :
            "Erro inesperado!\n Tente novamente em instantes";

            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e, $"{message} -> {login.Email}"));
            return Json(new { status = false, message = message });
        }

    }

    [Authorize]
    public ActionResult Logout()
    {
        _context = this.ControllerContext.RouteData;

        _cookieValue = HttpContext.Session.GetString(CookieName.JSESSIONID);

        HttpContext.Session.Clear();
        _useCase.Delete(_cookieValue);

        _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

        return RedirectToAction(ActionName.LOGIN, ControllerName.ACCOUNT);
    }

    public ActionResult Unauthenticated() => View();

    public ActionResult Forbidden() => View();

    private void SetSessions(Session session)
    {
        _cookieValue = _useCase.GetCookieValue();

        HttpContext.Session.SetString(CookieName.JSESSIONID, _cookieValue);
        HttpContext.Session.SetInt32(SessionKey.USER_ID, session.Id);
        HttpContext.Session.SetString(SessionKey.USER_NAME, session.Name);
        HttpContext.Session.SetString(SessionKey.USER_EMAIL, session.Email);
        HttpContext.Session.SetString(SessionKey.USER_DEVICE_READ_ONLY, session.DeviceReadonly.ToString());
        HttpContext.Session.SetString(SessionKey.USER_PHOTO,
            string.IsNullOrEmpty(session.Photo) ?
            "/dist/img/user-default.png" : session.Photo
        );
    }
}
