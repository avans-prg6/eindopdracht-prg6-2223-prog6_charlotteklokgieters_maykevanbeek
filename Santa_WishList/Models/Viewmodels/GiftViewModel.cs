using SantasWishlist.Domain;
using System.ComponentModel.DataAnnotations;

namespace Santa_WishList.Models.Viewmodels
{
    public class GiftViewModel
    {
        public bool IsChecked { get; set; }

        public string Gift { get; set; }
    }
}
