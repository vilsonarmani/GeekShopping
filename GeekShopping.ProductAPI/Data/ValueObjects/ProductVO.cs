using System.ComponentModel.DataAnnotations;

namespace GeekShopping.ProductAPI.Data.ValueObjects;

public class ProductVO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageURL { get; set; }

}

