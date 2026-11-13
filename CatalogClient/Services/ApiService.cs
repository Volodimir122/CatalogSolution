using CatalogClient.Models;
using System.Net.Http.Json;

namespace CatalogClient.Services;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    // Товары
    public Task<List<Product>?> GetProductsAsync(string? search = null, int? categoryId = null)
    {
        var url = "api/products?";
        if (!string.IsNullOrEmpty(search)) url += $"search={search}&";
        if (categoryId.HasValue && categoryId > 0) url += $"categoryId={categoryId}&";
        return _http.GetFromJsonAsync<List<Product>>(url);
    }

    public Task<Product?> GetProductAsync(int id) =>
        _http.GetFromJsonAsync<Product>($"api/products/{id}");

    public Task CreateProductAsync(Product product) =>
        _http.PostAsJsonAsync("api/products", product);

    public Task UpdateProductAsync(int id, Product product) =>
        _http.PutAsJsonAsync($"api/products/{id}", product);

    public Task DeleteProductAsync(int id) =>
        _http.DeleteAsync($"api/products/{id}");

    // Категории
    public Task<List<Category>?> GetCategoriesAsync() =>
        _http.GetFromJsonAsync<List<Category>>("api/categories");
}