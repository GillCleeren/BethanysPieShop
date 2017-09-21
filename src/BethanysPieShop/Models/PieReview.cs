namespace BethanysPieShop.Models
{
    public class PieReview
    {
        public int PieReviewId { get; set; }
        public Pie Pie { get; set; }
        public string Review { get; set; }
    }
}
