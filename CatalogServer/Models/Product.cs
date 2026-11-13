using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogServer.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название товара обязательно")]
    [StringLength(200)]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть положительной")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Display(Name = "Категория")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}