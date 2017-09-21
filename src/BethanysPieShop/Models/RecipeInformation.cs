namespace BethanysPieShop.Models
{
    public class RecipeInformation
    {
        public int RecipeInformationId { get; set; }
        public string PreparationDirections { get; set; }
        public int Duration { get; set; }
        public Pie Pie { get; set; }
        public int PieId { get; set; }

    }
}
