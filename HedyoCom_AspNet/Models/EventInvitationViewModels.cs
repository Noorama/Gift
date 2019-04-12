using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HedyoCom_AspNet.Models
{
    public class GiftListViewModel
    {
        public List<InvintationGiftViewModel> GiftsWithPayments { get; set; } = new List<InvintationGiftViewModel>();
        public List<InvintationGiftViewModel> GiftsWithOwnPayments { get; set; } = new List<InvintationGiftViewModel>();

        public List<SelectListItem> AvailableAmounts { get; } = new List<SelectListItem>
        {
            new SelectListItem
            {
              Text = "0",
              Value = "0"
            },
            new SelectListItem
            {
              Text = "50",
              Value = "50"
            },
            new SelectListItem
            {
              Text = "100",
              Value = "100"
            },
            new SelectListItem
            {
              Text = "200",
              Value = "200"
            },
            new SelectListItem
            {
              Text = "400",
              Value = "400"
            },
            new SelectListItem
            {
              Text = "500",
              Value = "500"
            },
            new SelectListItem
            {
              Text = "1000",
              Value = "1000"
            }
        }; 
    }

    public class InvintationGiftViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public virtual GiftImage Image { get; set; }
        public virtual IList<GiftPayment> Payments { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SelectedAmount { get; set; }

        public InvintationGiftViewModel() { }

        public InvintationGiftViewModel(Gift gift)
        {
            Id = gift.Id;
            Name = gift.Name;
            Price = gift.Price;
            EventId = gift.EventId;
            Image = gift.Image;
            Payments = gift.Payments.ToList();
        }

        public InvintationGiftViewModel(Gift gift, decimal totalAmount) : this(gift)
        {
            TotalAmount = totalAmount;
        }
    }

    public class SocialInvitationViewModel
    {
        public Event Event { get; set; }
        public string ExactUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}