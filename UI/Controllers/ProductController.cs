using System;
using CustomerManagement.UI.Models;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Controllers;

public class ProductController
{
    private readonly IProductService _productService;
    private readonly IConsoleService _consoleService;
    private readonly IInputReader _inputReader;
    private readonly DisplayHelper _displayHelper;

    public ProductController(IProductService productService, IConsoleService consoleService, IInputReader inputReader, DisplayHelper displayHelper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        _displayHelper = displayHelper ?? throw new ArgumentNullException(nameof(displayHelper));
    }

    public OperationResult AddProduct()
    {
        string name = _inputReader.ReadString("Enter product name: ");
        decimal? price = _inputReader.ReadDecimal("Enter price: ");
        int? stockQuantity = _inputReader.ReadInt("Enter stock quantity: ");

        if (price == null)
        {
            return OperationResult.FailureResult("Invalid price format.");
        }

        if (stockQuantity == null)
        {
            return OperationResult.FailureResult("Invalid stock quantity format.");
        }

        bool success = _productService.CreateProduct(name, price.Value, stockQuantity.Value);
        return success
            ? OperationResult.SuccessResult("Product created successfully!")
            : OperationResult.FailureResult("Failed to create product. Check your input.");
    }

    public OperationResult ViewAllProducts()
    {
        var products = _productService.GetAllProducts();
        _displayHelper.PrintProduct(products);
        return OperationResult.SuccessResult();
    }

    public OperationResult FindProductById()
    {
        int? productId = _inputReader.ReadInt("Enter Product ID: ");
        if (productId == null)
        {
            return OperationResult.FailureResult("Invalid product ID.");
        }

        var product = _productService.GetProductById(productId.Value);
        if (product == null)
        {
            return OperationResult.FailureResult("Product not found");
        }

        _consoleService.WriteLine($"ID: {product.ProductId} | Name: {product.Name} | Price: {product.Price:C} | Stock: {product.StockQuantity}");
        return OperationResult.SuccessResult();
    }

    public OperationResult UpdateProduct()
    {
        int? productId = _inputReader.ReadInt("Enter Product ID: ");
        if (productId == null)
        {
            return OperationResult.FailureResult("Invalid product ID.");
        }

        var existingProduct = _productService.GetProductById(productId.Value);
        if (existingProduct == null)
        {
            return OperationResult.FailureResult("product not found.");
        }

        _consoleService.DisplayInfo($"Current: {existingProduct.Name} | {existingProduct.Price} | Stock: {existingProduct.StockQuantity}");

        string newName = _inputReader.ReadString("Enter new name: ");
        decimal? newPrice = _inputReader.ReadDecimal("Enter new price: ");
        int? newStockQuantity = _inputReader.ReadInt("Enter new stock quantity: ");


        if (newPrice == null)
        {
            return OperationResult.FailureResult("Invalid price format.");
        }

        if (newStockQuantity == null)
        {
            return OperationResult.FailureResult("Invalid stock quantity format.");
        }

        bool success = _productService.UpdateProduct(productId.Value, newName, newPrice.Value, newStockQuantity.Value);
        return success
            ? OperationResult.SuccessResult("Product updated successfully!")
            : OperationResult.FailureResult("Failed to update product. ");
    }

    public OperationResult DeleteProduct()
    {
        int? productId = _inputReader.ReadInt("Enter product ID: ");
        if (productId == null)
        {
            return OperationResult.FailureResult("Invalid product ID.");
        }

        var product = _productService.GetProductById(productId.Value);
        if (product == null)
        {
            return OperationResult.FailureResult("product not found.");
        }

        _consoleService.DisplayInfo($"Product: [{product.ProductId}] | {product.Name} | {product.Price} | {product.StockQuantity}");

        if (!_consoleService.GetConfirmation("Are you sure you want to delete this product?"))
        {
            return OperationResult.FailureResult("Delete operation cancelled.");
        }

        bool success = _productService.DeleteProduct(productId.Value);
        return success
            ? OperationResult.SuccessResult("Product deleted successfully!")
            : OperationResult.FailureResult("Failed to delete product. ");
    }

    public OperationResult SearchProduct()
    {
        string name = _inputReader.ReadString("Enter product name: ");
        var products = _productService.SearchProductsByName(name);
        _displayHelper.PrintProduct(products);
        return OperationResult.SuccessResult();
    }
}
