using System.Collections.Generic;

namespace BethanysPieShop.Auth
{
    public static class BethanysPieShopClaimTypes
    {
        public static List<string> ClaimsList { get; set; } = new List<string> { "Delete Pie", "Add Pie", "Age for ordering" };
    }
}
