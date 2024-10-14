using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels;
using static WinterholdWeb.Constants;

namespace WinterholdWeb.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public CustomerIndexViewModel Get(int pageNumber, string memberNumber, string fullName, bool isExpired){
        var model = _repository.Get(pageNumber, PAGE_SIZE, memberNumber, fullName, isExpired)
        .Select(customer=>new CustomerViewModel(){
            MembershipNumber = customer.MembershipNumber,
            FullName = customer.FirstName + " " + customer.LastName,
            MembershipExpireDate = customer.MembershipExpireDate.ToString("dd/MM/yyyy")
        });
        return new CustomerIndexViewModel(){
            Customers = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountCustomers(memberNumber, fullName, isExpired)
            },
            MemberNumber = memberNumber,
            FullName = fullName,
            IsExpired = isExpired
        };
    }

    public CustomerUpsertViewModel GetByMemberNumber(string memberNumber){
        var model = _repository.GetByMemberNumber(memberNumber);

        return new CustomerUpsertViewModel(){
            MembershipNumber = model.MembershipNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Gender = model.Gender,
            Phone = model.Phone,
            Address = model.Address
        };
    }

    public void Insert(CustomerUpsertViewModel viewModel){
        var model = new Customer(){
            MembershipNumber = viewModel.MembershipNumber,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate?? new DateTime(),
            Gender = viewModel.Gender,
            Phone = viewModel.Phone,
            Address = viewModel.Address,
            MembershipExpireDate = DateTime.Now.AddYears(2)
        };
        _repository.Insert(model);
    }

    public void Update(string memberNumber, CustomerUpsertViewModel viewModel){
        var model = _repository.GetByMemberNumber(memberNumber);
        model.MembershipNumber = memberNumber;
        model.FirstName = viewModel.FirstName;
        model.LastName = viewModel.LastName;
        model.BirthDate = viewModel.BirthDate??new DateTime();
        model.Gender = viewModel.Gender;
        model.Phone = viewModel.Phone;
        model.Address = viewModel.Address;

        _repository.Update(model);
    }

}
