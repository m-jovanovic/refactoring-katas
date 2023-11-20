namespace CreditCalculator.Before;

public class CustomerService
{
    public bool AddCustomer(
        string firstName,
        string lastName,
        string email,
        DateTime dateOfBirth,
        int companyId)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        if (!email.Contains('@') && !email.Contains('.'))
        {
            return false;
        }

        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month ||
            now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
        {
            age--;
        }


        if (age < 21)
        {
            return false;
        }

        var companyRepository = new CompanyRepository();
        var company = companyRepository.GetById(companyId);

        var customer = new Customer
        {
            Company = company,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        if (company.Type == "VeryImportantClient")
        {
            // Skip credit check
            customer.HasCreditLimit = false;
        }
        else if (company.Type == "ImportantClient")
        {
            // Do credit check and double credit limit
            customer.HasCreditLimit = true;
            var creditService = new CustomerCreditServiceClient();

            var creditLimit = creditService.GetCreditLimit(
                customer.FirstName,
                customer.LastName,
                customer.DateOfBirth);

            creditLimit *= 2;
            customer.CreditLimit = creditLimit;
        }
        else
        {
            // Do credit check
            customer.HasCreditLimit = true;
            var creditService = new CustomerCreditServiceClient();

            var creditLimit = creditService.GetCreditLimit(
                customer.FirstName,
                customer.LastName,
                customer.DateOfBirth);

            customer.CreditLimit = creditLimit;
        }

        if (customer.HasCreditLimit && customer.CreditLimit < 500)
        {
            return false;
        }

        var customerRepository = new CustomerRepository();
        customerRepository.AddCustomer(customer);

        return true;
    }
}