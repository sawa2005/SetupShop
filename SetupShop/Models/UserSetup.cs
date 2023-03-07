using SetupShop.Areas.Identity.Data;

namespace SetupShop.Models
{
    public class UserSetup
    {
        public string UserId { get; set; }
        public SetupShopUser User { get; set; }
        public int SetupId { get; set; }
        public Setup Setup { get; set; }
    }
}
