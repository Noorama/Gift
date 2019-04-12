using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HedyoCom_AspNet.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Data.Entity;
using HedyoCom_AspNet.Contants;
using HedyoCom_AspNet.DAL;

namespace HedyoCom_AspNet.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private static readonly ILog log = LogManager.GetLogger(typeof(EventController));

        private static readonly SecureString SMTPClientPassword;
        private static readonly string SMTPServerAddress = "smtp.yandex.com";
        private static readonly int SMTPServerPort = 465;
        private static readonly string SenderEmail = "mozturksau@yandex.com";

        static EventController()
        {
            //0123456789012345678901
            string chars = "tsifubyrgapnvmzhwejodqlkc";
            SMTPClientPassword = new SecureString();
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
            // oegrwytrckmhtnnq
        }

        private static String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public ActionResult Index()
        {
            List<Event> ownedEvents;
            using (var ctx = new ApplicationDbContext())
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                    .FindById(User.Identity.GetUserId());
                if (user == null)
                    return RedirectToAction("Login", "Account");
                ownedEvents = ctx.Events
                    .Include(x => x.Characters)
                    .Where(w => w.Owner.Id == user.Id).ToList();
            }

            var inzarWedding = new Event()
            {
                Characters = new List<Character>
                {
                    new Character { FirstName = "İnzar" },
                    new Character { FirstName = "Hande" },

                },
                Date = new DateTime(2017, 10, 8, 19, 0, 0),
                Place = "Çiçek Düğün Salonu Nizip/Gaziantep"
            };
            var bekirWedding = new Event()
            {
                Characters = new List<Character>
                {
                    new Character { FirstName = "Bekir" },
                    new Character { FirstName = "Kübra" },

                },
                Date = new DateTime(2016, 10, 15, 11, 0, 0),
                Place = "Diltaş Sosyal Tesisleri Meram/Konya"
            };
            var model = new PortalViewModel()
            {
                OwnedEvents = ownedEvents, //new List<Wedding> { /*bekirWedding*/ },
                AttendingWeddings = new List<Event> {bekirWedding, inzarWedding}
            };
            return View(model);
        }

        public ActionResult Plan(string eventType = "")
        {
            eventType = eventType.ToLower();
            var eve = this.unitOfWork.EventTypeRepository.GetByName(eventType);

            if (eve == null)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = new PlanEventViewModel
            {
                EventTypeId = eve.Id,
                EventType = eventType
            };

            switch (eventType)
            {
                case "wedding":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.ManRole, Icon = EventConstants.ManIcon },
                        new CharacterViewModel { Role = EventConstants.WomanRole, Icon = EventConstants.WomanIcon},
                    };
                    break;

                case "birthday":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.BirthdayRole, Icon = EventConstants.BirthdayIcon },
                    };
                    break;

                case "babyborn":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.SimpleManRole, Icon = EventConstants.SimpleManIcon },
                        new CharacterViewModel { Role = EventConstants.SimpleWomanRole, Icon = EventConstants.SimpleWomanIcon},
                        new CharacterViewModel { Role = EventConstants.BabyRole, Icon = EventConstants.BabyIcon },
                    };
                    break;

                case "new_house_keeping":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.SimpleManRole, Icon = EventConstants.SimpleManIcon },
                    };
                    break;

                case "new_work_open":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.SimpleManRole, Icon = EventConstants.SimpleManIcon },
                    };
                    break;

                case "pipi_cuting":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.BabyRole, Icon = EventConstants.BabyIcon },
                    };
                    break;

                case "custom":
                    model.Characters = new List<CharacterViewModel>
                    {
                        new CharacterViewModel { Role = EventConstants.ManRole, Icon = EventConstants.ManIcon },
                    };
                    break;
                default:
                    return Content("Some Error Ocures on Character Selecting");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Plan(PlanEventViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                // TODO error page
                return Content("Model state not valid " + string.Join(", ",
                                   ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage)));
            }

            var eventType = this.unitOfWork.EventTypeRepository.GetByID(model.EventTypeId);
            if (eventType == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var appEvent = new Event();
           
            appEvent.Date = model.PlanningEvent.Date;
            appEvent.Place = model.PlanningEvent.Place;
            appEvent.PlaceDescription = model.PlanningEvent.PlaceDescription;
            appEvent.IBAN = model.PlanningEvent.IBAN;
            appEvent.OwnerId = User.Identity.GetUserId();
            appEvent.EventTypeId = eventType.Id;
            appEvent.Characters = model.Characters.Select(x => new Character
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Role = x.Role
            }).ToList();
            
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Events.Add(appEvent);
                ctx.SaveChanges();

                var ticket = new FormsAuthenticationTicket(1, "hcpd"+appEvent.Id, DateTime.Now, DateTime.MaxValue, true, "");
                appEvent.SocialAccessToken = FormsAuthentication.Encrypt(ticket); // TODO get something cryptographic here (is formsauthantication enough?)
                using (MD5 m = new MD5CryptoServiceProvider()) // TODO dont use md5, this is not guaranteed to be unique. use bit.ly api to minify the actual formsauthenticationticket
                {
                    appEvent.SocialAccessToken = Convert.ToBase64String(m.ComputeHash(UTF8Encoding.UTF8.GetBytes(appEvent.SocialAccessToken)));
                }
                ctx.SaveChanges();
            }
            return RedirectToAction(nameof(SelectGifts), new {EventId = appEvent.Id});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SelectGifts(SelectGiftsViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var evnt = ctx.Events.Find(model.EventId);

                if (evnt == null || evnt.OwnerId != User.Identity.GetUserId())
                    return Content(
                        "This is not your wedding dude! Or it was removed, I can't be sure."); // TODO error page


                if (model.Templates == null)
                {
                    model = new SelectGiftsViewModel()
                    {
                        //Templates = GiftTemplate.Templates.ToList(),
                        Templates = ctx.GiftTemplates.Include(x => x.GiftImage).ToList(),
                        SelectedTemplates = new List<int>()
                    };
                }
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName(nameof(SelectGifts))]
        public ActionResult SelectGiftsPost(SelectGiftsViewModel model)
        {
            if (model.SelectedTemplates == null || model.SelectedTemplates.Count == 0)
            {
                model.SelectedTemplates = new List<int>();
                ModelState.AddModelError("general", "En az bir hediye seçmeniz gerek.");
                return View(nameof(SelectGifts), model);
            }

            using (var ctx = new ApplicationDbContext())
            {
                var appEvent = ctx.Events.Find(model.EventId);

                if (appEvent == null || appEvent.OwnerId != User.Identity.GetUserId())
                    return Content(
                        "This is not your wedding dude! Or it was removed, I can't be sure."); // TODO error page

                appEvent.Gifts = appEvent.Gifts ?? new List<Gift>();
                foreach (var t in model.SelectedTemplates.Where(i => i >= 0 && i < model.Templates.Count)
                    .Select(i => model.Templates[i]))
                {
                    var image = ctx.GiftImages.Find(t.GiftImage.Id);
                    if (image == null)
                        continue;

                    var gift = new Gift
                    {
                        Image = image,
                        Name = t.Name,
                        Price = t.Price,
                        Event = appEvent
                    };
                    appEvent.Gifts.Add(gift);
                }
                ctx.SaveChanges();

                return RedirectToAction(nameof(SelectInvitees), new {WeddingId = appEvent.Id});
            }
        }

        public ActionResult SelectInvitees(SelectInviteesViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var appEvent = ctx.Events.Find(model.WeddingId);

                if (appEvent == null || appEvent.OwnerId != User.Identity.GetUserId())
                    return Content(
                        "This is not your wedding dude! Or it was removed, I can't be sure."); // TODO error page
            }
            model.Emails = new List<string>();
            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(SelectInvitees))]
        [ValidateInput(false)]
        public async Task<ActionResult> SelectInviteesPost(SelectInviteesViewModel model)
        {
            var messagesToSend = new List<MimeMessage>();
            using (var ctx = new ApplicationDbContext())
            {
                var appEvent = ctx.Events.Find(model.WeddingId);

                if (appEvent == null || appEvent.OwnerId != User.Identity.GetUserId())
                    return Content("This is not your wedding dude! Or it was removed, I can't be sure."); // TODO error page

                appEvent.Invitations = appEvent.Invitations ?? new List<EventInvitation>();
                string emailSubject = string.Join(", ", appEvent.Characters.Select(x => x.FirstName)) + " - Etkinliğine Davetiyeniz";

                var uniqueNew = model.Emails.Where(e =>string.IsNullOrWhiteSpace(e) == false && e.Contains('@') && appEvent.Invitations.All(i => i.InviteeEmail != e)).ToList();
                foreach (var a in uniqueNew)
                {
                    var fish = new EventInvitation();
                    fish.InviteeEmail = a;
                    var ticket = new FormsAuthenticationTicket(1, a, DateTime.Now, DateTime.MaxValue, true, "");
                    fish.Payload = FormsAuthentication.Encrypt(ticket); // TODO get something cryptographic here (is formsauthantication enough?)
                    fish.EverDisplayed = false;
                    appEvent.Invitations.Add(fish);


                    var mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress(Encoding.UTF8, "Hedyo.com" , SenderEmail));
                    mail.To.Add(new MailboxAddress(a));
                    mail.Subject = emailSubject;
                    var requestUrl = Request?.Url ?? new Uri("http://www.hedyo.com");
                    var emailBody = string.Format($@"<div style=""display:block; margin:auto; padding:10px; width:500px; height:auto; background-image: url('~/Content/images/Photos/Baloon_Image.png'); background-color:#FFFFFF; border-radius:10px; text-align:center"">
  <div style=""padding:15px 25px 25px 25px"">
    <label style=""font-family: 'Tahoma'; font-size:2em; color:#4085bf;""> {string.Join(" &amp; ", appEvent.Characters.Select(x => x.FirstName))} </label>
  </div>
  <div style=""margin:auto; padding:15px 15px 15px 15px; height:auto; background-color:#FFF; border-radius:10px; font-family:'Trebuchet MS'"">
    {model.InvitationNotice}
  </div>
  <div style=""margin:5px auto 15px auto; padding:5px 5px 5px 5px; width:300px; height:auto; background-color:#F0F0F0; border-radius:10px; font-family:'Tahoma'; font-size:0.8em"">
    {appEvent.Place}, {appEvent.Date:dd/MM/yyyy dddd}, {appEvent.Date:t}
    <br>
    <br>
    Adress Tanımı: {appEvent.PlaceDescription}
  </div>
  <div style=""display:block; margin:10px;"">
    <a href=""{requestUrl.GetLeftPart(UriPartial.Authority)}/EventInvitation/Index?token={fish.Payload}"" style=""background-color:#A9BCF5;border:1px solid #A9BCF5;border-radius:10px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:16px;line-height:44px;text-align:center;text-decoration:none;width:150px;-webkit-text-size-adjust:none;mso-hide:all;"">Daveti Kabul Et</a>
  </div>
</div>");

                    var b = new BodyBuilder();
                    b.HtmlBody = emailBody;
                    mail.Body = b.ToMessageBody();
                    messagesToSend.Add(mail);
                }

                ctx.SaveChanges();
                await SendEmails(messagesToSend);
                return RedirectToAction(nameof(PlanSuccess), new {weddingId = appEvent.Id});
            }
        }

        private async Task SendEmails(List<MimeMessage> mails)
        {
            var client = new SmtpClient();
            client.Connect(SMTPServerAddress,SMTPServerPort);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(new NetworkCredential(SenderEmail, SMTPClientPassword));
            client.SslProtocols = SslProtocols.Default;
            
            var tasks = new List<Task>();
            try
            {
                foreach (var m in mails)
                {
                    tasks.Add(client.SendAsync(m));
                }

                await Task.WhenAll(tasks.ToArray());
            }
            catch (Exception e)
            {
                log.Error("There was an error sending email.", e);
            }
            client.Dispose();
        }

        public ActionResult PlanSuccess(int weddingId)
        {
            var appEvent = default(Event);
            using (var ctx = new ApplicationDbContext())
            {
                appEvent = ctx.Events.Find(weddingId);

                if (appEvent == null || appEvent.OwnerId != User.Identity.GetUserId())
                    return Content("This is not your wedding dude! Or it was removed, I can't be sure."); // TODO error page

                string SocialImage = "https://www.hedyo.com:34443/Content/images/hedyo_logo_small.png";
                //Uri requestUrl = Request?.Url ?? new Uri("https://www.hedyo.com:34443/");
                Uri requestUrl = Request?.Url ?? new Uri("https://localhost:44371/");
                var socialShareUrl = requestUrl.GetLeftPart(UriPartial.Authority) + "/EventInvitation/SocialInvitation?token=" + appEvent.SocialAccessToken;
                ViewBag.TwitterShareUrl = $"http://twitter.com/share?text={HttpUtility.HtmlEncode($"{string.Join(" &amp; ", appEvent.Characters.Select(x => x.FirstName))} Bizim bir etkinliğimiz var! Görmek için linke tıkla!")}&url={socialShareUrl}&hashtags=event,hedyo";
                ViewBag.FacebookShareUrl = $"http://www.facebook.com/sharer/sharer.php?s=100&p[title]="+ "Etkinliğimize Katılın!" + "&u="+ socialShareUrl + "&p[summary]="+ $"{string.Join(" &amp; ", appEvent.Characters.Select(x => x.FirstName))} Bizim bir etkinliğimiz var! Görmek için linke tıkla!" + "&p[images][0]="+SocialImage;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Manage(int id)
        {
            var appEvent = this.unitOfWork.EventRepository.GetEventForManageById(id);

            if (appEvent == null || appEvent.OwnerId != User.Identity.GetUserId())
                return Content(
                    "This is not your wedding dude! Or it was removed, I can't be sure."); // TODO error page
            var model = new ManageEventViewModel(appEvent);

            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(ManageEventViewModel model)
        {
            try
            {
                var eve = this.unitOfWork.EventRepository.GetEventForManageById(model.EventId);

                if (eve == null || eve.OwnerId != User.Identity.GetUserId())
                {
                    return RedirectToAction(nameof(Index));
                }

                model.UpdateEvent(eve);
                this.unitOfWork.EventRepository.Update(eve);
                this.unitOfWork.Save();
                model.SetPropertiesFromEvent(eve);
                TempData["EventManageSuccess"] = "Bilgiler başarı ile güncellenmiştir!";
            }
            catch
            {
                TempData["EventManageError"] = "Bir hata oluşmuştur! Lütfen sonra tekrar deneyiniz.";

            }
         
 
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult Davetlilerim()
        {
            List<Event> ownedEvents;
            using (var ctx = new ApplicationDbContext())
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                    .FindById(User.Identity.GetUserId());
                if (user == null)
                    return RedirectToAction("Login", "Account");
                ownedEvents = ctx.Events
                    .Include(x => x.Characters)
                    .Where(w => w.Owner.Id == user.Id).ToList();
            }

            var inzarWedding = new Event()
            {
                Characters = new List<Character>
                {
                    new Character { FirstName = "İnzar" },
                    new Character { FirstName = "Hande" },

                },
                Date = new DateTime(2017, 10, 8, 19, 0, 0),
                Place = "Çiçek Düğün Salonu Nizip/Gaziantep"
            };
            var bekirWedding = new Event()
            {
                Characters = new List<Character>
                {
                    new Character { FirstName = "Bekir" },
                    new Character { FirstName = "Kübra" },

                },
                Date = new DateTime(2016, 10, 15, 11, 0, 0),
                Place = "Diltaş Sosyal Tesisleri Meram/Konya"
            };
            var model = new PortalViewModel()
            {
                OwnedEvents = ownedEvents, //new List<Wedding> { /*bekirWedding*/ },
                AttendingWeddings = new List<Event> { bekirWedding, inzarWedding }
            };

            return View(model);
        }



    }
}