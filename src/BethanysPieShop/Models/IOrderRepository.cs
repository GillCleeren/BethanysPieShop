namespace BethanysPieShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        void CreatePieGiftOrder(PieGiftOrder pieGiftOrder);
    }
}
