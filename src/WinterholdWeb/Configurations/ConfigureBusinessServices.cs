using WinterholdBusiness.Interfaces;
using WinterholdBusiness.Repositories;
using WinterholdWeb.Services;

namespace WinterholdWeb.Configurations;

public static class ConfigureBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services){
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<AuthorService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<CategoryService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<BookService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerService>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<LoanService>();
        return services;
    }
}
