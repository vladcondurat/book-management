using MediatR;

namespace Application.Use_Clases.Commands;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public int Tva { get; set; }
    public int DiscountPercent { get; set; }
}