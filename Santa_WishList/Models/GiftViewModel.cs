using SantasWishlist.Domain;

namespace Santa_WishList.Models
{
    public class GiftViewModel
    {
        public bool IsChecked { get; set; }
        public Gift Gift { get; set; }
    }
}
