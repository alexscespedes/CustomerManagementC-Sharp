using System;
using CustomerManagement.DependencyInjection;
using CustomerManagement.Repositories;

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

        container.RegisterTransientType<MenuManager, MenuManager>();

        return container;
    }
}
