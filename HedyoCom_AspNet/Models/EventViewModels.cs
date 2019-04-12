using HedyoCom_AspNet.Contants;
using HedyoCom_AspNet.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HedyoCom_AspNet.Models
{
    public class PortalViewModel
    {
        public IList<Event> OwnedEvents { get; set; }
        public IList<Event> AttendingWeddings { get; set; }
    }

    public class PlanEventViewModel
    {
        public Event PlanningEvent { get; set; }
        public List<CharacterViewModel> Characters { get; set; } = new List<CharacterViewModel>();
        public int EventTypeId { get; set; }
        public string EventType { get; set; }

        public PlanEventViewModel()
        {
        }

        public string GetRoleTitle(string eventType, string role)
        {
            return HedyoEventHelper.GetRoleTitle(eventType, role);
        }
    }

    public class ManageEventViewModel
    {
        public List<CharacterViewModel> Characters { get; set; } = new List<CharacterViewModel>();
        public string Place { get; set; }
        public string PlaceDescription { get; set; }
        public DateTime Date { get; set; }
        public IList<Gift> Gifts { get; set; }
        public string EventType { get; set; }
        public int EventId { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Sadece Sayıları Giriniz!")]
        public string IBAN { get; set; }

        public IList<EventInvitation> EventInvitations { get; set; }

        public ManageEventViewModel() { }

        public ManageEventViewModel(Event e)
        {
            this.SetPropertiesFromEvent(e);
        }

        public void SetPropertiesFromEvent(Event e)
        {
            this.EventId = e.Id;
            this.Place = e.Place;
            this.PlaceDescription = e.PlaceDescription;
            this.Date = e.Date;
            this.Gifts = e.Gifts;
            this.IBAN = e.IBAN;
            this.EventType = e.EventType.Name;
            this.EventInvitations = e.Invitations.ToList();
            this.Characters = e.Characters.Select(c => new CharacterViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Role = c.Role,
                Icon = c.Role != null ? EventConstants.RoleIcons[c.Role] : EventConstants.SimpleManIcon
            }).ToList();
        }

        public List<GiftPayment> GetInvatedUserPayements(IList<Gift> gifts, EventInvitation eventInvitation)
        {
            var payments = new List<GiftPayment>();
            foreach (var gift in gifts)
            {
                payments.AddRange(gift.Payments.Where(p => p.PayerEmail == eventInvitation.InviteeEmail));
            }

            return payments;
        }

        public string GetRoleTitle(string eventType, string role)
        {
            return HedyoEventHelper.GetRoleTitle(eventType, role);
        }


        /// <summary>
        /// Update event
        /// </summary>
        /// <param name="e">Event which will be updated</param>
        public void UpdateEvent(Event e)
        {
            this.UpdateCharacters(e);
            e.Place = this.Place;
            e.PlaceDescription = this.PlaceDescription;
            e.Date = this.Date;
            e.IBAN = this.IBAN;
        }

        /// <summary>
        /// Update event characters
        /// </summary>
        /// <param name="e">Event which will be updated</param>
        public void UpdateCharacters(Event e)
        {
            foreach(var character in e.Characters)
            {
                var _character = this.Characters.Find(x => x.Id == character.Id);
                if (_character == null)
                {
                    continue;
                }
                character.FirstName = _character.FirstName;
                character.LastName = _character.LastName;
            }
        }
    }

    public class CharacterViewModel
    {

        public int Id { get; set; }

        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Role { get; set; }

        public string Icon { get; set; }
    }

    public class SelectGiftsViewModel
    {
        public int EventId { get; set; }
        public List<GiftTemplate> Templates { get; set; }
        /// <summary>
        /// Keeps the indices of the selected templates from list <see cref="Templates"/>
        /// </summary>
        public List<int> SelectedTemplates { get; set; }
    }
    
    public class SelectInviteesViewModel
    {
        public int WeddingId { get; set; }
        public List<string> Emails { get; set; }
        public string InvitationNotice { get; set; }
    }
}