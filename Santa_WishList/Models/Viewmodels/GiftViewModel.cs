using SantasWishlist.Domain;

namespace Santa_WishList.Models.Viewmodels
{
    public class GiftViewModel
    {
        public bool IsChecked { get; set; }
        public Gift Gift { get; set; }
    }
}
