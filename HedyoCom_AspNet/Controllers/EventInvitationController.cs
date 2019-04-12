using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HedyoCom_AspNet.Models;
using InvitationResponse = HedyoCom_AspNet.Models.EventInvitation.InvitationResponse;

namespace HedyoCom_AspNet.Controllers
{
    public class EventInvitationController : Controller
    {
        public ActionResult Index(string token)
        {
            EventInvitation invitation;
            using (var ctx = new ApplicationDbContext())
            {
                invitation = ctx.EventInvitations
                    .Include(x => x.Event.Characters)
                    .FirstOrDefault(w=> w.Payload == token);
                
                if (invitation == null)
                    return View("InvalidInvitation"); // TODO view

                if (invitation.Event.IsClosedForInvitees)
                    return View("ExpiredInvitation"); // TODO view

                // Wedding was already loaded when invitation.Wedding was called. Lets do it again in case someone deletes the lines above.
                ctx.Entry(invitation).Reference(i => i.Event).Load();
            }
            return View(invitation);
        }

        [HttpPost]
        public async Task<JsonResult> InvitationResponseSet(int id, string payload, int response)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var invitation = ctx.EventInvitations.Find(id);
                if (invitation == null || invitation.Payload != payload)
                    return Json(new {result = "fail", reason= "invalid invitation"});
                if (invitation.Event.IsClosedForInvitees)
                    return Json(new { result = "fail", reason="expired invitation" });

                // not responded or the response is not valid
                if (response == (int)InvitationResponse.NotResponded ||
                    Enum.IsDefined(typeof(InvitationResponse), response) == false)
                {
                    return Json(new { result = "fail", reason = "invalid response" });
                }

                invitation.InvitationStatus = (InvitationResponse) response;
                await ctx.SaveChangesAsync();
                return Json(new { result = "success", response });
            }
        }

        public async Task<ActionResult> ListGifts(int id, string payload)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var invitation = ctx.EventInvitations.Find(id);

                if (invitation == null || invitation.Payload != payload)
                    return View("InvalidInvitation"); // TODO

                if (invitation.Event.IsClosedForInvitees)
                    return View("ExpiredInvitation"); // TODO

                var model = new GiftListViewModel();

                model.GiftsWithPayments = invitation.Event.Gifts // summed valid payments
                    .Select(g => new InvintationGiftViewModel(g, g.Payments.Where(p => p.WasSuccessful).Sum(p => p.Amount)))
                    .ToList();

                model.GiftsWithOwnPayments = invitation.Event.Gifts
                    .Where(g => g.Payments.Any(p => p.WasSuccessful && p.PayerEmail == invitation.InviteeEmail)) // he has a payment for that gift
                    .Select(g => new InvintationGiftViewModel(g, g.Payments.Where(p => p.WasSuccessful && invitation.InviteeEmail == p.PayerEmail) // find the total payment
                            .Sum(p => p.Amount)))
                    .ToList();

                return View(model);
            }
        }

        public ActionResult SocialInvitation(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return View("InvalidInvitation"); // TODO view

            var model = new SocialInvitationViewModel();
            using (var ctx = new ApplicationDbContext())
            {
                var appEvent = ctx.Events.FirstOrDefault(w => w.SocialAccessToken == token);
                if(appEvent == null)
                    return View("InvalidInvitation"); // TODO view
                if(appEvent.IsClosedForInvitees)
                    return View("ExpiredInvitation"); // TODO view

                model.Event = appEvent;
            }

            
            var requestUrl = Request?.Url ?? new Uri("http://www.hedyo.com");
            model.ExactUrl = requestUrl.GetLeftPart(UriPartial.Authority) + "/EventInvitation/SocialInvitation?token=" + token;
            model.ImageUrl = requestUrl.GetLeftPart(UriPartial.Authority) + "/Content/Images/Photos/stock-photo-wedding-365221256.jpg";
            return View(model);
        }

        [HttpPost]
        public ActionResult Payment(GiftListViewModel model, int id, string payload)
        {
            EventInvitation invitation;
            using (var ctx = new ApplicationDbContext())
            {
                invitation = ctx.EventInvitations
                    .Include(x => x.Event.Characters)
                    .Where(w => w.Id == id)
                    .FirstOrDefault();

                if (invitation == null)
                    return View("InvalidInvitation"); // TODO view

                if (invitation.Event.IsClosedForInvitees)
                    return View("ExpiredInvitation"); // TODO view

                // Event was already loaded when invitation.Event was called. Lets do it again in case someone deletes the lines above.
                ctx.Entry(invitation).Reference(i => i.Event).Load();

                // create payments list based on all gifts wich has SelectedAmount greater then 0
                var giftPayments = model.GiftsWithPayments.Aggregate(
                    new List<GiftPayment>(),
                    (payments, gift) => {
                        if (gift.SelectedAmount > 0)
                        {
                            payments.Add(new GiftPayment()
                            {
                                Amount = gift.SelectedAmount,
                                Date = DateTime.Now,
                                GiftId = gift.Id,
                                PayerEmail = invitation.InviteeEmail,
                                WasSuccessful = true,
                            });
                        }
                        return payments;
                    }
                );

                ctx.GiftPayments.AddRange(giftPayments);
                ctx.SaveChanges();
            }
            return View(invitation);
        }

    }
}