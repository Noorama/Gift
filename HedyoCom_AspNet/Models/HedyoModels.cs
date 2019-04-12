using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HedyoCom_AspNet.Models
{
    /// <summary>
    /// Represents a event.
    /// </summary>
    public class Event
    {
        public Event()
        {
            this.Characters = new List<Character>();
        }

        public int Id { get; set; }
        /// <summary>
        /// Creator of this <see cref="Event"/>.
        /// </summary>
        public virtual ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }

        /// <summary>
        /// Where is this <see cref="Event"/> going to take place.
        /// </summary>
        public string Place { get; set; }
        public string PlaceDescription { get; set; }
        /// <summary>
        /// When is this <see cref="Event"/> going to take place.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Represents the <see cref="Gift"/>s associated with this <see cref="Event"/>.
        /// </summary>
        public virtual IList<Gift> Gifts { get; set; }
        public string IBAN { get; set; }
        public virtual IList<EventInvitation> Invitations { get; set; }
        public virtual IList<Character> Characters { get; set; }
        public string SocialAccessToken { get; set; }

        /// <summary>
        /// Can invitees still make payments and change their Going/NotGoing/Maybe status?
        /// </summary>
        public bool IsClosedForInvitees { get; set; }

        [ForeignKey(nameof(EventType))]
        public int? EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }
    }

    public class EventInvitation
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        /// <summary>
        /// <see cref="Models.Event"/> associated with this invitation.
        /// </summary>
        public virtual Event Event { get; set; }
        /// <summary>
        /// Email address of the invitee.
        /// </summary>
        public string InviteeEmail { get; set; }
        /// <summary>
        /// Payload that was sent to this user via email. Requests carrying a valid payload are able to make payments.
        /// </summary>
        public string Payload { get; set; }
        /// <summary>
        /// Did invitee ever clicked the link in the email?
        /// </summary>
        public bool EverDisplayed { get; set; }

        /// <summary>
        /// Represents the answer we have received for the event invitation that was sent before.
        /// </summary>
        public enum InvitationResponse
        {
            NotResponded=0,
            Going=1,
            NotGoing=2,
            Maybe=3
        }
        /// <summary>
        /// Gets and Sets the Invitation Status declared by the invitee. 
        /// <remarks>
        /// This property writes and reads from <see cref="bf_InvitationStatus"/>
        /// and doesn't keep its own data. Hence, it is not mapped to database.
        /// </remarks>
        /// </summary>
        [NotMapped]
        public InvitationResponse InvitationStatus
        {
            get { return (InvitationResponse) bf_InvitationStatus; }
            set { bf_InvitationStatus = (int) value; }
        }
        /// <summary>
        /// Backing field for <see cref="InvitationStatus"/>. This property is mapped to the database.
        /// <remarks>Do not directly write constant values into this field since their meaining (defined in <see cref="InvitationResponse"/>) might be changed.</remarks>
        /// </summary>
        [Column("InvitationStatus")]
        public int bf_InvitationStatus { get; set; } = (int) InvitationResponse.NotResponded;
    }

    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Price of the <see cref="Gift"/> in TL determined by the <see cref="Models.Event.Owner"/>.
        /// <remarks>Total <see cref="Payments"/> made to this gift cannot exceed <see cref="Price"/>.</remarks>
        /// </summary>
        public decimal Price { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        /// <summary>
        /// Holds to what <see cref="Models.Event"/> this gift belongs to.
        /// </summary>
        public virtual Event Event { get; set; }
        /// <summary>
        /// Represents the <see cref="GiftImage"/> displayed with this <see cref="Gift"/>.
        /// </summary>
        public virtual GiftImage Image { get; set; }
        /// <summary>
        /// Holds the <see cref="GiftPayment"/>s made to this gift. 
        /// <remarks>Total value of the payments cannot exceed <see cref="Price"/>.</remarks>
        /// </summary>
        public virtual IList<GiftPayment> Payments { get; set; }
    }

    /// <summary>
    /// Represents an image to be displayed with a <see cref="Gift" />.
    /// </summary>
    public class GiftImage
    {
        public int Id { get; set; }
        public string Filename { get; set; }
    }

    /// <summary>
    /// Represents a payment made to a <see cref="Gift" />.
    /// </summary>
    public class GiftPayment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(PayerUser))]
        public string PayerUserId { get; set; }
        /// <summary>
        /// Holds the info of which user made the payment. 
        /// If payment was not done with a registered account, use <see cref="PayerEmail"/> to access the email address of the payer.
        /// </summary>
        public ApplicationUser PayerUser { get; set; }
        
        /// <summary>
        /// Holds the info of who made the payment.
        /// If payment was done by a registered user, this field is empty. Then payer info can be accessed through <see cref="PayerUser"/> field.
        /// </summary>
        public string PayerEmail { get; set; }

        [ForeignKey(nameof(Gift))]
        public int GiftId { get; set; }
        /// <summary>
        /// Holds on which product the payment was made.
        /// </summary>
        public virtual Gift Gift { get; set; }

        /// <summary>
        /// Holds how much TL was paid.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Holds bank related payment info.
        /// </summary>
        public string ReceiptInfo { get; set; }

        /// <summary>
        /// Holds when was the payment made.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Was the payment successful or was it rejected for some reason.
        /// </summary>
        public bool WasSuccessful { get; set; }

        /// <summary>
        /// Null if the payment was successful. Returns the reason for the error otherwise.
        /// </summary>
        public string Error { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Role { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }

    public class EventType
    {
        public int Id { get; set; }

        [MaxLength(64)]
        [Required]
        public string Name { get; set; }

        public virtual IList<Event> Events { get; set; }
    }
}