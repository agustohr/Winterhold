using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly WinterholdContext _dbContext;

    public CustomerRepository(WinterholdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Customer> Get()
    {
        return _dbContext.Customers
        .Where(customer=>customer.MembershipExpireDate > DateTime.Now)
        .ToList();
    }

    public List<Customer> Get(int pageNumber, int pageSize, string? memberNumber, string? fullName, bool isExpired)
    {
        return _dbContext.Customers
        .Where(customer=>
            customer.MembershipNumber.ToLower().Contains(memberNumber??"".ToLower()) &&
            (customer.FirstName + " " + customer.LastName).ToLower().Contains(fullName??"".ToLower()) && 
            (isExpired ? (customer.MembershipExpireDate < DateTime.Now) : true)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int CountCustomers(string? memberNumber, string? fullName, bool isExpired)
    {
        return _dbContext.Customers
        .Where(customer=>
            customer.MembershipNumber.ToLower().Contains(memberNumber??"".ToLower()) &&
            (customer.FirstName + " " + customer.LastName).ToLower().Contains(fullName??"".ToLower()) && 
            (isExpired ? (customer.MembershipExpireDate < DateTime.Now) : true)
        )
        .Count();
    }

    public Customer GetByMemberNumber(string memberNumber)
    {
        return _dbContext.Customers.Find(memberNumber) ?? throw new NullReferenceException($"Customer with member number {memberNumber} is not found");
    }

    public void Insert(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();
    }

    public void Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        _dbContext.SaveChanges();
    }

    public void Delete(Customer customer)
    {
        _dbContext.Customers.Remove(customer);
        _dbContext.SaveChanges();
    }
}
