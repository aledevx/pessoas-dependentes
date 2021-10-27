using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pessoa_dependentes_serverside.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace pessoa_dependentes_serverside.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SendMail();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secured()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (username == "alexandre" && password == "xande123")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, "Alexandre Oliveira"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Error. Username or Password is invalid!";
            return View("login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SendMail()
        {
            TwilioClient.Init("test", "SK0b10bdbbed041df566ecddb693ff189a");

            var incomingPhoneNumber = IncomingPhoneNumberResource.Create(
                phoneNumber: new Twilio.Types.PhoneNumber("5569992251883")
            );


        }
        // protected void Button1_Click(object sender, EventArgs e)

        // {

        //     String CelDestinatario = "99323-3480";

        //     string toPhoneNumber = "+5569" + CelDestinatario;

        //     string login = "alexandre.oliveira@faculdadeporto.com.br";

        //     string password = "xande123";

        //     string compression = "Alerta de notebook";

        //     string body = "Existem notebooks com atraso";


        //     MailMessage mail = new MailMessage()
        //     {
        //         From = new MailAddress(login, "Super Xandão")
        //     };

        //     mail.To.Add(new MailAddress(toPhoneNumber + "@sms.ipipi.com"));
        //     mail.Subject = compression;
        //     mail.Body = body;
        //     mail.IsBodyHtml = true;
        //     mail.Priority = MailPriority.High;

        //     //outras opções
        //     //mail.Attachments.Add(new Attachment(arquivo));
        //     //

        //     using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
        //     {
        //         smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
        //         smtp.EnableSsl = true;
        //         await smtp.SendMailAsync(mail);

        //         // MailMessage mail = new MailMessage();

        //         // mail.To = toPhoneNumber + "@sms.ipipi.com";

        //         // mail.From = login + "@ipipi.com";

        //         // mail.Subject = compression;

        //         // mail.Body = body;

        //         // mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");

        //         // mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", login);

        //         // mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);

        //         // SmtpMail.SmtpServer = "ipipi.com";

        //         // SmtpMail.Send(mail);

        //     }
    }
}
