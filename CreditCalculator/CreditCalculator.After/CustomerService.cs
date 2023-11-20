namespace CreditCalculator.After;

public class CustomerService
{
    private readonly CompanyRepository _companyRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly CreditLimitCalculator _creditLimitCalculator;

    public CustomerService(
        CompanyRepository companyRepository,
        CustomerRepository customerRepository,
        CreditLimitCalculator creditLimitCalculator)
    {
        _companyRepository = companyRepository;
        _customerRepository = customerRepository;
        _creditLimitCalculator = creditLimitCalculator;
    }

    public bool AddCustomer(
        string firstName,
        string lastName,
        string email,
        DateTime dateOfBirth,
        int companyId)
    {
        if (!IsValid(firstName, lastName, email, dateOfBirth))
        {
            return false;
        }

        var company = _companyRepository.GetById(companyId);

        var customer = new Customer
        {
            Company = company,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        (customer.HasCreditLimit, customer.CreditLimit) =
            _creditLimitCalculator.Calculate(customer, company);

        if (customer is { HasCreditLimit: true, CreditLimit: < 500 })
        {
            return false;
        }

        _customerRepository.AddCustomer(customer);

        return true;
    }

    private static bool IsValid(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        const int minimumAge = 21;

        return !string.IsNullOrEmpty(firstName) &&
               !string.IsNullOrEmpty(lastName) &&
               (email.Contains('@') || email.Contains('.')) &&
               CalculateAge(dateOfBirth, DateTime.Now) >= minimumAge;
    }

    private static int CalculateAge(DateTime dateOfBirth, DateTime now)
    {
        var age = now.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > now.AddYears(-age))
        {
            age -= 1;
        }

        return age;
    }
}