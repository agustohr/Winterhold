using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface ICustomerRepository
{
    public List<Customer> Get(); 
    public List<Customer> Get(int pageNumber, int pageSize, string? memberNumber, string? fullName, bool isExpired); 
    public int CountCustomers(string? memberNumber, string? fullName, bool isExpired);
    public Customer GetByMemberNumber(string memberNumber);
    public void Insert(Customer customer);
    public void Update(Customer customer);
    public void Delete(Customer customer);
}
