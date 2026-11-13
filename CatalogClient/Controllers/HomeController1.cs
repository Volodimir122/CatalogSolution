using CatalogClient.Models;
using CatalogClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogClient.Controllers;

public class ProductsController : Controller
{
    private readonly ApiService _api;

    public ProductsController(ApiService api)
    {
        _api = api;
    }

    // СПИСОК + ФИЛЬТРАЦИЯ
    public async Task<IActionResult> Index(string? search, int? categoryId)
    {
        var products = await _api.GetProductsAsync(search, categoryId);
        var categories = await _api.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryId);
        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;

        return View(products ?? new List<Product>());
    }

    // ДЕТАЛИ
    public async Task<IActionResult> Details(int id)
    {
        var product = await _api.GetProductAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // ДОБАВЛЕНИЕ (Форма)
    public async Task<IActionResult> Create()
    {
        var categories = await _api.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    // ДОБАВЛЕНИЕ (Сохранение)
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _api.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        await _api.CreateProductAsync(product);
        return RedirectToAction(nameof(Index));
    }

    // РЕДАКТИРОВАНИЕ (Форма)
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _api.GetProductAsync(id);
        if (product == null) return NotFound();

        var categories = await _api.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    // РЕДАКТИРОВАНИЕ (Сохранение)
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            var categories = await _api.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        await _api.UpdateProductAsync(id, product);
        return RedirectToAction(nameof(Index));
    }

    // УДАЛЕНИЕ (Форма)
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _api.GetProductAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // УДАЛЕНИЕ (Сохранение)
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _api.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }
}