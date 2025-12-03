using System;
using CustomerManagement.DependencyInjection;
using CustomerManagement.Repositories;
using CustomerManagement.UI.Controllers;
using CustomerManagement.UI.Menus;
using CustomerManagement.UI.Services;

namespace CustomerManagement.Configuration;

public static class ServiceConfiguration
{
    public static IServiceContainer ConfigureServices()
    {
        var container = new SimpleServiceContainer();

        var dataContext = new DataContext();
        container.RegisterSingletonInstance(dataContext);

        container.RegisterSingletonType<ICustomerRepository, CustomerRepository>();
        container.RegisterSingletonType<IProductRepository, ProductRepository>();
        container.RegisterSingletonType<IOrderRepository, OrderRepository>();

        container.RegisterSingletonType<ICustomerService, CustomerService>();
        container.RegisterSingletonType<IProductService, ProductService>();
        container.RegisterSingletonType<IOrderService, OrderService>();

        container.RegisterSingletonType<InputValidator, InputValidator>();
        container.RegisterSingletonType<DisplayHelper, DisplayHelper>();
        container.RegisterSingletonType<JsonDataRepository, JsonDataRepository>();

        container.RegisterSingletonType<IConsoleService, ConsoleService>();
        container.RegisterSingletonType<IInputReader, InputReader>();

        container.RegisterSingletonType<CustomerController, CustomerController>();
        container.RegisterSingletonType<ProductController, ProductController>();
        container.RegisterSingletonType<OrderController, OrderController>();
        container.RegisterSingletonType<ReportController, ReportController>();

        container.RegisterSingletonType<CustomerMenu, CustomerMenu>();
        container.RegisterSingletonType<ProductMenu, ProductMenu>();
        container.RegisterSingletonType<OrderMenu, OrderMenu>();
        container.RegisterSingletonType<ReportMenu, ReportMenu>();
        container.RegisterSingletonType<MainMenu, MainMenu>();

        return container;
    }
}
