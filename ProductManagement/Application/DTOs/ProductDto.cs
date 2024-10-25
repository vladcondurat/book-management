namespace Application.DTOs;

public class ProductDto
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public int Tva { get; set; }
    public int DiscountPercent { get; set; }
}