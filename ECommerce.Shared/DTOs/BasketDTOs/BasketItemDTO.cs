using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.DTOs.BasketDTOs
{
    public record BasketItemDTO(
        int Id,
        string Name , 
        string PictureUrl,
        [Range(1,500)]
        decimal Price,
        [Range(1,100)]
        int Quantity)
    {
    }
}