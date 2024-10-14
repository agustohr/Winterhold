using WinterholdBusiness.Interfaces;

namespace WinterholdAPI.Customers;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public CustomerDto GetByMemberNumber(string memberNumber){
        var model = _repository.GetByMemberNumber(memberNumber);

        return new CustomerDto(){
            MembershipNumber = model.MembershipNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate.ToString("dd/MM/yyyy"),
            Gender = model.Gender,
            Phone = model.Phone,
            Address = model.Address
        };
    }

    public void Delete(string memberNumber){
        var model = _repository.GetByMemberNumber(memberNumber);
        _repository.Delete(model);
    }

    public void ExtendExpiredDate(string memberNumber){
        var model = _repository.GetByMemberNumber(memberNumber);
        model.MembershipExpireDate = model.MembershipExpireDate.AddYears(2);
        _repository.Update(model);
    }
}
