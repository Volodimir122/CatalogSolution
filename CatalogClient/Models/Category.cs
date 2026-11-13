using System.ComponentModel.DataAnnotations;

namespace CatalogClient.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название категории обязательно")]
    [StringLength(100)]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}