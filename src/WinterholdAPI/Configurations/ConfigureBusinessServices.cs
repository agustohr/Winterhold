using WinterholdAPI.Authors;
using WinterholdAPI.Books;
using WinterholdAPI.Categories;
using WinterholdAPI.Customers;
using WinterholdAPI.Loans;
using WinterholdBusiness.Interfaces;
using WinterholdBusiness.Repositories;

namespace WinterholdAPI.Configurations;

public static class ConfigureBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services){
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<CategoryService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<BookService>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<AuthorService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerService>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<LoanService>();
        return services;
    }
}
