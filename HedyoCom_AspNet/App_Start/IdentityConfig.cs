using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using HedyoCom_AspNet.Models;
using MailKit.Net.Smtp;
using System.Net;
using System.Security;
using System.Security.Authentication;
using MimeKit;
using System.Text;

namespace HedyoCom_AspNet
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // настройка логина, пароля отправителя
            var from = "hedyo@yandex.ru";
            var pass = "12345Wq!";

            // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
            //SmtpClient client = new SmtpClient("smtp.yandex.ru", 465);

            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential(from, pass);
            //client.EnableSsl = true;

            //// создаем письмо: message.Destination - адрес получателя
            //var mail = new MailMessage(from, message.Destination);
            //mail.Subject = message.Subject;
            //mail.Body = message.Body;
            //mail.IsBodyHtml = true;

            //return client.SendMailAsync(mail);

            string chars = "tsifubyrgapnvmzhwejodqlkc";
            var SMTPClientPassword = new SecureString();
            SMTPClientPassword.AppendChar(chars[19]); // TODO add 2nd and 3rd level indexing for additional security
            SMTPClientPassword.AppendChar(chars[17]);
            SMTPClientPassword.AppendChar(chars[8]);
            SMTPClientPassword.AppendChar(chars[7]);
            SMTPClientPassword.AppendChar(chars[16]);
            SMTPClientPassword.AppendChar(chars[6]);
            SMTPClientPassword.AppendChar(chars[0]);
            SMTPClientPassword.AppendChar(chars[7]);
            SMTPClientPassword.AppendChar(chars[24]);
            SMTPClientPassword.AppendChar(chars[23]);
            SMTPClientPassword.AppendChar(chars[13]);
            SMTPClientPassword.AppendChar(chars[15]);
            SMTPClientPassword.AppendChar(chars[0]);
            SMTPClientPassword.AppendChar(chars[11]);
            SMTPClientPassword.AppendChar(chars[11]);
            SMTPClientPassword.AppendChar(chars[21]);

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.yandex.com", 465);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(new NetworkCredential("mozturksau@yandex.com", SMTPClientPassword));
            client.SslProtocols = SslProtocols.Default;

            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(Encoding.UTF8, "Hedyo.com", "mozturksau@yandex.com"));
            mail.To.Add(new MailboxAddress(message.Destination));
            mail.Subject = message.Subject;
            string emailBody = message.Body;

            var b = new BodyBuilder();
            b.HtmlBody = emailBody;
            mail.Body = b.ToMessageBody();

            return client.SendAsync(mail);
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
